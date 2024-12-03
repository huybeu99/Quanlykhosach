$(document).ready(function () {
    loadBooks();
});

function loadBooks() {
    $.ajax({
        url: '/api/book',
        method: 'GET',
        dataType:'json',
        success: function (data) {
            displayBooks(data);
        },
        error: function (xhr) {
            alert('Error loading books');
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
                                <h5 class="card-title">${book.name}</h5>
                                <p class="card-text">${book.description || 'No description'}</p>
                                <p class="card-text">
                                    <strong>Publisher:</strong> ${book.publisherName}
                                </p>
                                <div class="mt-2">
                                    <button class="btn btn-primary btn-sm"
                                            onclick="editBook(${book.id})">Edit</button>
                                    <button class="btn btn-danger btn-sm"
                                            onclick="deleteBook(${book.id})">Delete</button>
                                </div>
                            </div>
                        </div>
                    </div>
                `;
    });
    $('#booksList').html(html);

    function getBookById(id) {
        if (!id) {
            alert('Book ID is required');
            return;
        }
        $.ajax({
            url: '/books/book/${id}',  
            method: 'GET',
            dataType: 'json',
            success: function (book) {
                // Populate edit modal with book details
                $('#editBookId').val(book.id);
                $('#editBookName').val(book.name);
                $('#editBookDescription').val(book.description || '');

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

    function updateBook() {
        // Collect form data
        var bookId = $('#editBookId').val();
        var bookName = $('#editBookName').val();
        var bookDescription = $('#editBookDescription').val();

        // Prepare the book object
        var bookData = {
            id: parseInt(bookId),
            name: bookName,
            description: bookDescription,
        };

        $.ajax({
            url: '/books/book/${bookId}',
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
            url: '/books/book/${id}',
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
}