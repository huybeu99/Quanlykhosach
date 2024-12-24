$(document).ready(function () {
    loadBooks();
    $('#author').on('change',function (){
        if($(this).val()==='add_new_author'){
            $(this).val('');
            $('#addAuthorModal').modal('show');
        }
    })
});
function openModaladdbook() {
    $('#addBookModal').modal('show');
    loaddropdownadd();
}
function openupdateBook(bookID) {
    $.get('api/book/allbooks', function (data) {
       var book=data.find(function (item) {
           return item.book_ID== bookID;
       })
if (book) {
    $('#editBookName').val(book.book_Name);
    $('#editBookDescription').val(book.book_Description);
    $('#editBookYear').val(book.book_Year);
    $('#editBookQuantity').val(book.book_Quantity);
     loaddropdown(book);
    $('#updateBookModal .modal-footer').html(`
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" id="update" class="btn btn-primary btn-sm" data-book="${bookID}">Save</button>
    `);
    $('#update').on('click', function() {
        var bookIdUpdate = $(this).data('book');
        updateBook(bookIdUpdate);
    });
    $('#updateBookModal').modal('show');
}
else {
    alert('Không tìm thấy sách với ID ' + bookId);
}
    }).fail(function () {
        alert('Có lỗi sảy ra khi tải dữ liệu')
    })
}
function loadBooks() {
    $.ajax({
        url: '/api/book/allbooks',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data);
            if (data && data.length > 0) {
                displayBooks(data);
            } else {
                console.warn('No books found');
                $('#booksList').html('<p>Không có sách nào để hiển thị.</p>');
            }
        },
        error: function (xhr, status, error) {
            console.error('Chi tiết lỗi:', status, error);
            $('#booksList').html(`<p>Lỗi tải sách: ${error}</p>`);
        }
    });
}
function displayBooks(books) {
    var html = '';
    books.forEach(function (book) {
        html += `
            <div class="col-md-4 mb-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">${book.book_Name || 'Tên sách không xác định'}</h5>
                        <p class="card-text">${book.book_Description || 'Không có mô tả'}</p>
                        <p class="card-text">
                            <strong>Nhà xuất bản:</strong> ${book.publisher_Name || 'Chưa rõ'}
                        </p>
                        <p class="card-text">
                            <strong>Tác giả:</strong> ${book.author[0].author_Name || 'Chưa rõ'}
                        </p>
                        <p class="card-text">
                            <strong>Thể loại:</strong> ${book.category[0].category_Name || 'Chưa rõ'}
                        </p>
                        <p class="card-text">
                            <strong>Năm xuất bản:</strong> ${book.book_Year || 'Chưa rõ'}
                        </p>
                        <p class="card-text">
                            <strong>Số lượng:</strong> ${book.book_Quantity !== undefined ? book.book_Quantity : 0}
                        </p>
                         <p class="card-text">
                            <strong>Số lượng:</strong> ${book.wareHouse_Name || 'Chưa rõ'}
                        </p>
                        <div class="mt-2">
                            <button id="detail" class="btn btn-primary btn-sm" onclick="getBookById(${book.book_ID})">Chi tiết</button>
                            <button class="btn btn-danger btn-sm" onclick="deleteBook(${book.book_ID})">Xóa</button>
                        </div>
                    </div>
                </div>
            </div>
        `;
    });

    // Cập nhật nội dung của phần tử #booksList với chuỗi HTML đã tạo
    $('#booksList').html(html);
}
function getBookById(id) {
    if (!id) {
        alert('Book ID is required');
        return;
    }
    $.ajax({
        url: `/api/book/${id}`,  
        method: 'GET',
        dataType: 'json',
        success: function (book) {
            $('#detailBookName').val(book.book_Name);
            $('#detailBookDescription').val(book.book_Description || '');
            $('#detailBookQuantity').val(book.book_Quantity);
            $('#detailBookYear').val(book.book_Year);
            $('#detailBookPublisher').val(book.publisher_Name);
            $('#detailPublisherPhone').val(book.publisher_Phone);
            $('#detailBookWarehouse').val(book.wareHouse_Name);
            $('#detailWareHouseAddress').val(book.wareHouse_Address);
            $('#detailBookAuthor').val(book.author[0].author_Name);
            $('#detailBookCategory').val(book.category[0].category_Name);
            // Open the detail modal
            $('#detailBookModal .modal-footer').html(`
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" id="openupdate" class="btn btn-primary btn-sm" data-book-id="${book.book_ID}">Update</button>
    `);
            $('#openupdate').on('click', function() {
                var bookId = $(this).data('book-id');
                openupdateBook(bookId);
                console.log(bookId);
            });
            $('#detailBookModal').modal('show');
        },
        error: function (xhr) {
            // Handle error
            if (xhr.status === 404) {
                alert('Book not found');
            } else {
                alert('Error retrieving book details: ' + xhr.responseText);
            }
        }
    });
}
function updateBook(id) {
    var bookId = id;
    var bookName = $('#editBookName').val();
    var bookDescription = $('#editBookDescription').val();
    var bookQuantity = $('#editBookQuantity').val();
    var bookYear = $('#editBookYear').val();
    var publisherId = $('#updateBookModal .publisher').val();
    var wareHouseId = $('#updateBookModal .wareHouse').val();
    var author = $('#updateBookModal .author').val();
    var category = $('#updateBookModal .category').val();
    var bookImage = null;
    var publisherText = $(' #updateBookModal .publisher option:selected').text();
    var publisherName= publisherText.split('(')[0].trim();
    var publisherPhone= publisherText.match(/\(([^)]+)\)/)[1];
    var warehouseText = $('#updateBookModal .wareHouse option:selected').text();
    var warehouseName= warehouseText.split('(')[0].trim();
    var warehouseAddress= warehouseText.match(/\(([^)]+)\)/)[1];
   

    // Ensure we have arrays even if single selection
    if (!Array.isArray(author)) author = [author];
    if (!Array.isArray(category)) category = [category];
    
    var authors = author.map(function(authorId) {
        var selectedOption = $('.author option[value="' + authorId + '"]');
        if (!selectedOption.length) {
            console.error('Could not find author option for ID:', authorId);
            return null;
        }
        return {
            Author_ID: parseInt(authorId),
            Author_Name: selectedOption.text()
        };
    });
    
    var categories = category.map(function(categoryId) {
        var selectedOption = $('.category option[value="' + categoryId + '"]');
        if (!selectedOption.length) {
            console.error('Could not find category option for ID:', categoryId);
            return null;
        }
        return {
            Category_ID: parseInt(categoryId),
            Category_Name: selectedOption.text()
        };
    });

    // Validate that we have at least one author and category
    if (authors.length === 0 || categories.length === 0) {
        alert("Please select at least one author and one category");
        return;
    }    
    // Xác thực trường dữ liệu
    if (!bookName || !bookDescription || !bookQuantity || !bookYear) {
        alert("Vui lòng điền đầy đủ thông tin");
        return;
    }
    var bookData = {
        Book_ID:bookId,
        Book_Name: bookName,
        Book_Description: bookDescription,
        Book_Quantity: parseInt(bookQuantity),
        Book_Year: parseInt(bookYear),
        Publisher_ID: parseInt(publisherId),
        Publisher_Name: publisherName,
        Publisher_Phone: publisherPhone,
        WareHouse_ID: parseInt(wareHouseId),
        WareHouse_Name:warehouseName,
        WareHouse_Address: warehouseAddress,
        Author: authors,
        Category: categories,
        BookImage: bookImage,
    };
    console.log('Sending book data:', bookData);
    console.log('Author Name:', authors[0].Author_Name);
    console.log('Category Name:', categories[0].Category_Name);
    $.ajax({
        url: `/api/book/${bookId}`,
        method: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(bookData),
        success: function (response) {
            // Close the modal
            $('#updateBookModal').modal('hide');
            $('#detailBookModal').modal('hide');

            // Reload the books list
            loadBooks();

            // Optional: Show success message
            alert('Book updated successfully');
        },
        error: function (xhr) {
            // Handle error
            alert('Error updating book: ' + xhr.responseText);
        }
    });
}
function deleteBook(id) {
    // Confirm deletion
    if (!confirm('Are you sure you want to delete this book?')) {
        return;
    }

    // Send AJAX request to delete the book
    $.ajax({
        url: `/api/book/${id}`,
        method: 'DELETE',
        success: function (response) {
            // Remove the book from the list
            loadBooks();

            // Optional: Show success message
            alert('Book deleted successfully');
        },
        error: function (xhr) {
            // Handle error
            if (xhr.status === 404) {
                alert('Book not found');
            } else {
                alert('Error deleting book: ' + xhr.responseText);
            }
        }
    });
}
function loaddropdown(book) {
    console.log('Loading dropdowns for bookID:', book.book_ID);
    // Publisher dropdown
    $.ajax({
        url: 'api/publisher/allpublisher',
        method: 'GET',
        success: function (publishers) {
            var publisherSelect = $('#updateBookModal .publisher');
            publisherSelect.empty();
            publishers.forEach(function (publisher) {
                publisherSelect.append(`
                    <option value="${publisher.publisher_ID}" 
                            ${publisher.publisher_ID === book.publisher_ID ? 'selected' : ''}>
                                   ${publisher.publisher_Name} (${publisher.publisher_Phone})
                    </option>
                `);
            });
        }
    });

    // Warehouse dropdown
    $.ajax({
        url: 'api/wareHouse/allwarehouse',
        method: 'GET',
        success: function (warehouses) {
            var warehousesSelect = $('#updateBookModal .wareHouse');
            warehousesSelect.empty();
            warehouses.forEach(function (warehouse) {
                warehousesSelect.append(`
                    <option value="${warehouse.wareHouse_ID}"
                            ${warehouse.wareHouse_ID === book.wareHouse_ID ? 'selected' : ''}>
                        ${warehouse.wareHouse_Name} (${warehouse.wareHouse_Address})
                    </option>
                `);
            });
        }
    });

    // Author dropdown
    $.ajax({
        url: 'api/author/allauthor',
        method: 'GET',
        success: function (authors) {
            var authorSelect = $('#updateBookModal .author');
            authorSelect.empty();
            authors.forEach(function (author) {
                const isSelected = book.author.some(bookAuthor =>
                    bookAuthor.author_ID === author.author_ID
                );
                authorSelect.append(`
                    <option value="${author.author_ID}"
                            ${isSelected ? 'selected' : ''}>
                        ${author.author_Name}
                    </option>
                `);
            });
        }
    });

    // Category dropdown
    $.ajax({
        url: 'api/category/allcategory',
        method: 'GET',
        success: function (categories) {
            var categorySelect = $('#updateBookModal .category');
            categorySelect.empty();
            categories.forEach(function (category) {
                const isSelected = book.category.some(bookCategory =>
                    bookCategory.category_ID === category.category_ID
                );
                categorySelect.append(`
                    <option value="${category.category_ID}"
                            ${isSelected ? 'selected' : ''}>
                        ${category.category_Name}
                    </option>
                `);
            });
        }
    });
}
function addbook() {
    const bookName = $('#bookName').val().trim();
    const bookDescription = $('#bookDescription').val().trim();
    const bookYear = parseInt($('#bookYear').val());
    const bookQuantity = parseInt($('#bookQuantity').val());
    const publisherId = $('#publisher').val();
    const warehouseId = $('#warehouse').val();
    const authorIds = $('#author').val();
    const categoryIds = $('#category').val();

    // Kiểm tra dữ liệu cơ bản
    if (!bookName || !bookDescription || isNaN(bookYear) || isNaN(bookQuantity)) {
        alert("Vui lòng điền đầy đủ thông tin và giá trị hợp lệ");
        return;
    }

    // Lấy thông tin nhà xuất bản từ option được chọn
    const publisherText = $('#publisher option:selected').text();
    const publisherName = publisherText.split('(')[0].trim();
    const publisherPhone = publisherText.match(/\(([^)]+)\)/)[1];

    // Lấy thông tin kho từ option được chọn
    const warehouseText = $('#warehouse option:selected').text();
    const warehouseName = warehouseText.split('(')[0].trim();
    const warehouseAddress = warehouseText.match(/\(([^)]+)\)/)[1];

    // Chuyển đổi lựa chọn tác giả thành định dạng mảng
    const authors = (Array.isArray(authorIds) ? authorIds : [authorIds]).map(id => ({
        Author_ID: parseInt(id),
        Author_Name: $(`#author option[value="${id}"]`).text()
    }));

    // Chuyển đổi lựa chọn thể loại thành định dạng mảng
    const categories = (Array.isArray(categoryIds) ? categoryIds : [categoryIds]).map(id => ({
        Category_ID: parseInt(id),
        Category_Name: $(`#category option[value="${id}"]`).text()
    }));

    // Lấy ID sách tiếp theo có sẵn
    $.get('/api/book/allbooks')
        .done(function(books) {
            const maxBookId = books.length > 0
                ? Math.max(...books.map(book => book.book_ID)) //Sử dụng spread operator (...) để "mở rộng" mảng thành các đối số riêng biệt mà Math.max có thể nhận được
                : 0;

            const bookData = {
                Book_ID: maxBookId + 1,
                Book_Name: bookName,
                Book_Description: bookDescription,
                Book_Year: bookYear,
                Book_Quantity: bookQuantity,
                Publisher_ID: parseInt(publisherId),
                Publisher_Name: publisherName,
                Publisher_Phone: publisherPhone,
                WareHouse_ID: parseInt(warehouseId),
                WareHouse_Name: warehouseName,
                WareHouse_Address: warehouseAddress,
                Author: authors,
                Category: categories
            };

            // Gửi dữ liệu sách lên server
            $.ajax({
                url: 'api/book',
                method: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(bookData),
                success: function(response) {
                    $('#addBookModal').modal('hide');
                    loadBooks();
                    alert('Thêm sách thành công!');
                    // Xóa form
                    $('#addBookForm')[0].reset();
                },
                error: function(xhr, status, error) {
                    const errorMessage = xhr.responseJSON?.errors
                        ? JSON.stringify(xhr.responseJSON.errors)
                        : error;
                    alert('Lỗi khi thêm sách: ' + errorMessage);
                    console.error('Chi tiết lỗi:', xhr.responseJSON);
                }
            });
        })
        .fail(function(xhr, status, error) {
            alert('Lỗi khi lấy danh sách sách: ' + error);
            console.error('Chi tiết lỗi:', xhr.responseJSON);
        });
}
function loaddropdownadd() {
    // Publisher dropdown
    $.ajax({
        url: 'api/publisher/allpublisher',
        method: 'GET',
        success: function (publishers) {
            var publisherSelect = $('#publisher');
            publisherSelect.empty();
            publishers.forEach(function (publisher) {
                publisherSelect.append(`
                    <option value="${publisher.publisher_ID}" 
                            >
                                   ${publisher.publisher_Name} (${publisher.publisher_Phone})
                    </option>
                `);
            });
        }
    });

    // Warehouse dropdown
    $.ajax({
        url: 'api/wareHouse/allwarehouse',
        method: 'GET',
        success: function (warehouses) {
            var warehousesSelect = $('#warehouse');
            warehousesSelect.empty();
            warehouses.forEach(function (warehouse) {
                warehousesSelect.append(`
                    <option value="${warehouse.wareHouse_ID}"
                            >
                        ${warehouse.wareHouse_Name} (${warehouse.wareHouse_Address})
                    </option>
                `);
            });
        }
    });

    // Author dropdown
    $.ajax({
        url: 'api/author/allauthor',
        method: 'GET',
        success: function (authors) {
            var authorSelect = $('#author');
            authorSelect.empty();
            authors.forEach(function (author) {
                authorSelect.append(`
                    <option value="${author.author_ID}"
                            >
                        ${author.author_Name}
                    </option>
                `);
            });
            authorSelect.append('<option value="add_new_author">Add New Author</option>');
        }
    });

    // Category dropdown
    $.ajax({
        url: 'api/category/allcategory',
        method: 'GET',
        success: function (categories) {
            var categorySelect = $('#category');
            categorySelect.empty();
            categories.forEach(function (category) {
               
                categorySelect.append(`
                    <option value="${category.category_ID}"
                            >
                        ${category.category_Name}
                    </option>
                `);
            });
        }
    });
}
function addauthor(){
    var authorName=$('#authorName').val();
    $.get('/api/author/allauthor')
        .done(function(authors) {
            const maxAuthorId = authors.length > 0
                ? Math.max(...authors.map(author => author.author_ID)) //Sử dụng spread operator (...) để "mở rộng" mảng thành các đối số riêng biệt mà Math.max có thể nhận được
                : 0;
            var authorData={
                Author_ID: maxAuthorId+1,
                Author_name: authorName
            };
    $.ajax({
        url:'api/author',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(authorData),
        success: function(response) {
            $('#addAuthorModal').modal('hide');
            updateAuthorDropdown(response);
        },
        error: function(xhr, status, error) {
            const errorMessage = xhr.responseJSON?.errors
                ? JSON.stringify(xhr.responseJSON.errors)
                : error;
            alert('Lỗi khi thêm tác giả: ' + errorMessage);
            console.error('Chi tiết lỗi:', xhr.responseJSON);
        }
    });
        })
        .fail(function(xhr, status, error) {
            alert('Lỗi khi lấy danh sách tác giả: ' + error);
            console.error('Chi tiết lỗi:', xhr.responseJSON);
        });
}
function updateAuthorDropdown(newAuthor) {
    var newOption = new Option(newAuthor.name, newAuthor.id);
    $('#author').append(newOption);
}

