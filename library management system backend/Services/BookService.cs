﻿using library_management_system.Database.Entiy;
using library_management_system.DTOs;
using library_management_system.DTOs.Book;
using library_management_system.Repositories;
using library_management_system.Utilities;

namespace library_management_system.Services
{
    public class BookService
    {
        private readonly BookRepository _bookRepository;
        private readonly ImageService _imageService;

        public BookService(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ApiResponse<int>> AddNewBook(AddBookDto bookDto)
        {
            try
            {
                var coverImagePaths = await SaveCoverImages(bookDto.CoverImages);


                var book = new NormalBook
                {
                    ISBN = bookDto.ISBN,
                    Title = bookDto.Title,
                    Author = bookDto.Author,
                    Genre = bookDto.Genre,
                    PublishYear = bookDto.PublishYear,
                    ShelfLocation = bookDto.ShelfLocation,
                    TotalCopies = bookDto.TotalCopies,
                    CoverImagePath = coverImagePaths,
                    AddedDate = DateTime.Now,
                    RentCount = 0,
                };

               
                int bookId = await _bookRepository.AddNewBookWithCopies(book, bookDto.TotalCopies);
                if (bookId != null)
                {
                    return new ApiResponse<int>
                    {
                        Success = true,
                        Message = "Book and copies added successfully",
                        Data = bookId
                    };
                }
                return new ApiResponse<int>
                {
                    Success = false,
                    Message = "error while adding the book",
                    Data = bookId
                };

            } 
            catch (Exception ex)
            {
                return new ApiResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while adding the book",
                    Errors = new List<string> { ex.Message }
                };
            }
        }


        public async Task<ApiResponse<int>> UpdateNormalBook(UpdateBookDto bookDto)
        {
            try
            {
                // Retrieve the existing book
                var book = await _bookRepository.GetBookById(bookDto.Id);
                if (book == null)
                {
                    return new ApiResponse<int>
                    {
                        Success = false,
                        Message = "Book not found",
                        Errors = new List<string> { "No book with the provided ID exists." }
                    };
                }

                // Update book properties
                book.ISBN = bookDto.ISBN;
                book.Title = bookDto.Title;
                book.Author = bookDto.Author;
                book.Genre = bookDto.Genre;
                book.PublishYear = bookDto.PublishYear;
                book.ShelfLocation = bookDto.ShelfLocation;
               

                // Save new cover images if provided
                if (bookDto.CoverImages != null && bookDto.CoverImages.Any())
                {
                    var coverImagePaths = await SaveCoverImages(bookDto.CoverImages);
                    book.CoverImagePath = coverImagePaths;
                }

                // Update the book in the repository
                int bookId = await _bookRepository.UpdateNormalBook(book);

                return new ApiResponse<int>
                {
                    Success = true,
                    Message = "Book updated successfully",
                    Data = bookId
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while updating the book",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<int>> AddCopiesToBook(int bookId, int numberOfCopies)
        {
            try
            {
                
                var book = await _bookRepository.GetBookById(bookId);
                if (book == null)
                {
                    return new ApiResponse<int>
                    {
                        Success = false,
                        Message = "Book not found",
                        Errors = new List<string> { "No book with the provided ID exists." }
                    };
                }

              
                var newCopies = new List<BookCopy>();
                for (int i = 0; i < numberOfCopies; i++)
                {
                    var bookCopy = new BookCopy
                    {
                        BookId = bookId,
                        IsAvailable = true,
                        Condition = "New",
                        AddedDate = DateTime.Now
                    };
                    newCopies.Add(bookCopy);
                }
                await _bookRepository.AddBookCopies(newCopies);

                
                book.TotalCopies += numberOfCopies;
                await _bookRepository.UpdateNormalBook(book);

                return new ApiResponse<int>
                {
                    Success = true,
                    Message = $"{numberOfCopies} copies added successfully",
                    Data = book.Id
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while adding copies",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<int>> DeleteBookCopyByCopyId(int copyId)
        {
            try
            {
               
                var bookCopy = await _bookRepository.GetBookCopyById(copyId);
                if (bookCopy == null)
                {
                    return new ApiResponse<int>
                    {
                        Success = false,
                        Message = "Book copy not found",
                        Errors = new List<string> { "No book copy with the provided ID exists." }
                    };
                }

              
                var book = await _bookRepository.GetBookById(bookCopy.BookId);
                if (book == null)
                {
                    return new ApiResponse<int>
                    {
                        Success = false,
                        Message = "Book not found",
                        Errors = new List<string> { "No book with the provided ID exists." }
                    };
                }

               
                await _bookRepository.DeleteBookCopyAsync(bookCopy);

               
                book.TotalCopies -= 1;
                await _bookRepository.UpdateNormalBook(book);

                return new ApiResponse<int>
                {
                    Success = true,
                    Message = "Book copy deleted successfully",
                    Data = book.Id
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while deleting the book copy",
                    Errors = new List<string> { ex.Message }
                };
            }
        }


        public async Task<ApiResponse<int>> DeleteNormalBook(int bookId)
        {
            try
            {
                
                var book = await _bookRepository.GetBookById(bookId);
                if (book == null)
                {
                    return new ApiResponse<int>
                    {
                        Success = false,
                        Message = "Book not found",
                        Errors = new List<string> { "No book with the provided ID exists." }
                    };
                }

               
                await _bookRepository.DeleteAllBookCopiesByBookId(bookId);

                
                await _bookRepository.DeleteNormalBook(book);

                return new ApiResponse<int>
                {
                    Success = true,
                    Message = "Book and its associated copies deleted successfully",
                    Data = book.Id
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while deleting the book and its copies",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<List<NormalBookDto>>> GetAllNormalBooksWithAvailableCopies()
        {
            try
            {
                // Retrieve all normal books along with their copies
                var books = await _bookRepository.GetAllNormalBooksWithAvailableCopies();

                // Map the retrieved books to DTOs for API response
                var bookDtos = books.Select(book => new NormalBookDto
                {
                    Id = book.Id,
                    ISBN = book.ISBN,
                    Title = book.Title,
                    Author = book.Author,
                    Genre = book.Genre,
                    PublishYear = book.PublishYear,
                    ShelfLocation = book.ShelfLocation,
                    TotalCopies = book.TotalCopies,
                    AvailableCopies = book.BookCopies.Count(bc => bc.IsAvailable),
                    CoverImagePath = book.CoverImagePath
                }).ToList();

                return new ApiResponse<List<NormalBookDto>>
                {
                    Success = true,
                    Message = "Normal books retrieved successfully.",
                    Data = bookDtos
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<NormalBookDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving normal books.",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<NormalBookDetailsDto>> GetBookWithAvailableCopies(int bookId)
        {
            try
            {
               
                var book = await _bookRepository.GetBookWithCopies(bookId);

                if (book == null)
                {
                    return new ApiResponse<NormalBookDetailsDto>
                    {
                        Success = false,
                        Message = "Book not found.",
                        Errors = new List<string> { "No book with the provided ID exists." }
                    };
                }

               
                var bookDetailsDto = new NormalBookDetailsDto
                {
                    Id = book.Id,
                    ISBN = book.ISBN,
                    Title = book.Title,
                    Author = book.Author,
                    Genre = book.Genre,
                    PublishYear = book.PublishYear,
                    ShelfLocation = book.ShelfLocation,
                    AvailableCopies = book.BookCopies.Count(bc => bc.IsAvailable),
                    CoverImagePath = book.CoverImagePath,
                    BookCopies = book.BookCopies.Select(bc => new BookCopyDto
                    {
                        CopyId = bc.CopyId,
                        Condition = bc.Condition,
                        IsAvailable = bc.IsAvailable,
                        LastBorrowedDate = bc.LastBorrowedDate
                    }).ToList()
                };

                return new ApiResponse<NormalBookDetailsDto>
                {
                    Success = true,
                    Message = "Book details retrieved successfully.",
                    Data = bookDetailsDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<NormalBookDetailsDto>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the book details.",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<List<NormalBookDetailsDto>>> GetAllBooksWithCopies()
        {
            try
            {
               
                var books = await _bookRepository.GetAllBooksWithCopies();

               
                var bookDtos = books.Select(book => new NormalBookDetailsDto
                {
                    Id = book.Id,
                    ISBN = book.ISBN,
                    Title = book.Title,
                    Author = book.Author,
                    Genre = book.Genre,
                    PublishYear = book.PublishYear,
                    ShelfLocation = book.ShelfLocation,
                    TotalCopies = book.TotalCopies,
                    AvailableCopies = book.BookCopies.Count(bc => bc.IsAvailable),
                    CoverImagePath = book.CoverImagePath,
                    BookCopies = book.BookCopies.Select(bc => new BookCopyDto
                    {
                        CopyId = bc.CopyId,
                        Condition = bc.Condition,
                        IsAvailable = bc.IsAvailable,
                        LastBorrowedDate = bc.LastBorrowedDate
                    }).ToList()
                }).ToList();

                return new ApiResponse<List<NormalBookDetailsDto>>
                {
                    Success = true,
                    Message = "All books with copies retrieved successfully.",
                    Data = bookDtos
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<NormalBookDetailsDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the books.",
                    Errors = new List<string> { ex.Message }
                };
            }
        }





        private async Task<List<string>> SaveCoverImages(List<IFormFile> coverImages)
        {
            if (coverImages == null || !coverImages.Any())
                return new List<string> { "defaultcover.jpg" };

            var imagePaths = await _imageService.SaveImages(coverImages, "BookCoverImages");
            return imagePaths;
        }

    }
}
