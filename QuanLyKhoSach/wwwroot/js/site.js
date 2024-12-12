$(document).ready(function () {
    loadBooks();
    $('#updateButton').click(function () {
        var bookId = $(this).data('id'); 
        updateBook(bookId); 
    });
});
function addbook() {
    console.log('openaddBookModal function is called');
    $('#addBookModal').modal('show'); 
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
                            <strong>Tác giả:</strong> ${book.author || 'Chưa rõ'}
                        </p>
                        <p class="card-text">
                            <strong>Thể loại:</strong> ${book.category || 'Chưa rõ'}
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
                            <button class="btn btn-primary btn-sm" onclick="getBookById(${book.book_ID})">Chi tiết</button>
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
        url: `/api/book/${id}`,  // Đảm bảo sử dụng template literals đúng cách
        method: 'GET',
        dataType: 'json',
        success: function (book) {
            $('#editBookId').val(book.book_ID);
            $('#editBookName').val(book.book_Name);
            $('#editBookDescription').val(book.book_Description || '');      
            $('#editBookQuantity').val(book.book_Quantity);
            $('#editBookYear').val(book.book_Year);
            $('#editBookPublisher').val(book.publisher_Name);
            $('#editPublisherPhone').val(book.publisher_Phone);
            $('#editPublisherId').val(book.publisher_ID);
            $('#editBookWarehouse').val(book.wareHouse_Name);
            $('#editWareHouseAddress').val(book.wareHouse_Address);
            $('#editBookAuthor').val(book.author);
            $('#editBookCategory').val(book.category);
            $('#editWarehouseId').val(book.wareHouse_ID);
            // Open the edit modal
            $('#editBookModal').modal('show');
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
        var bookId = id = $('#editBookId').val();  
        var bookName = $('#editBookName').val();  
        var bookDescription = $('#editBookDescription').val();  
        var bookQuantity = $('#editBookQuantity').val();  
        var bookYear = $('#editBookYear').val();  
        var publisherName = $('#editBookPublisher').val();  
        var publisherPhone = $('#editPublisherPhone').val();  
        var publisherId = $('#editPublisherId').val();  
        var wareHouseName = $('#editBookWarehouse').val();  
        var wareHouseAddress = $('#editWareHouseAddress').val();  
        var wareHouseId = $('#editWarehouseId').val();
        var author = $('#editAuthor').val();  
        var category = $('#editCategory').val();  
        var bookImage = null;  

        // Xác thực trường dữ liệu
        if (!bookName || !bookDescription || !bookQuantity || !bookYear) {
            alert("Vui lòng điền đầy đủ thông tin");
            return;
        }
        //if (!wareHouseId || !publisherId) {
        //    alert('Vui lòng chọn nhà xuất bản và kho sách');
        //    return;
        //}
        var bookData = {
            Book_ID: bookId,
            Book_Name: bookName,
            Book_Description: bookDescription,
            Book_Quantity: parseInt(bookQuantity),
            Book_Year: parseInt(bookYear),
            Publisher_Name: publisherName, 
            Publisher_Phone: publisherPhone,
            Publisher_ID: parseInt(publisherId),
            WareHouse_Name: wareHouseName, 
            WareHouse_Address: wareHouseAddress,
            WareHouse_ID: wareHouseId,
            Author: [author], 
            Category: [category],
            BookImage: bookImage,
        }; 
        $.ajax({
            url: `/api/book/${bookData.Book_ID}`,
            method: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(bookData),
            success: function (response) {
                // Close the modal
                $('#editBookModal').modal('hide');

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
function add() {
    var bookdata = {
        Book_name = $('#bookName').val(),
        Book_description = $('#bookDescription').val(),
        Book_year = $('#bookYear').val(),
        Book_quantity = $('#bookQuantity').val(),
        Publisher_Name = $('#publisher').val(),
        author =[$('#author').val()],
        category =[$('#category').val()],
        WareHouse_name = $('#wareHouse').val(),
    };
    $.ajax({
        url: 'api/book',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(bookdata),
        success: function (response) {
            // Đóng modal sau khi lưu thành công
            $('#addBookModal').modal('hide');
            // Làm mới danh sách sách hoặc thông báo thành công
            alert('Thêm sách thành công!');
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi
            alert('Có lỗi xảy ra: ' + error);
        }
    });
}
