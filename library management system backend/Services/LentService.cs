using library_management_system.Database.Entiy;
using library_management_system.DTOs.LentRecord;
using library_management_system.DTOs;
using library_management_system.Repositories;
using System.Diagnostics.Eventing.Reader;
using static System.Reflection.Metadata.BlobBuilder;
using library_management_system.DTOs.Book;
using library_management_system.DTOs.Ebook;

namespace library_management_system.Services
{
    public class LentService
    {
        private readonly LentRepository _lentRecordRepository;
        private readonly UserRepo _userRepository;
        private readonly AdminRepo _adminRepository;
        private readonly BookRepository _bookRepository;
        

        public LentService(LentRepository lentRecordRepository, UserRepo userRepository, AdminRepo adminRepository)
        {
            _lentRecordRepository = lentRecordRepository;
            _userRepository = userRepository;
            _adminRepository = adminRepository;
        }

        public async Task<ApiResponse<bool>> LendNormalBook(LentRecordDto lentRecordDto)
        {

            var book = await _lentRecordRepository.GetNormalBookWithCopies(lentRecordDto.BookId);
            var user = await _lentRecordRepository.GetUserById(lentRecordDto.UserId);
            var admin = await _lentRecordRepository.GetAdminById(lentRecordDto.AdminId);



            if (book == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Book not found",
                    Data = false
                };
            }

            if (user == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "User not found",
                    Data = false
                };
            }

            if (admin == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Admin not found",
                    Data = false
                };
            }


            var availableCopy = book.BookCopies.FirstOrDefault(copy => copy.IsAvailable);

            if (availableCopy == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "No available copies for this book",
                    Data = false
                };
            }


            var lendDate = DateTime.Now;
            var dueDate = lendDate.AddDays(lentRecordDto.DueDays);


            var lentRecord = new LentRecord
            {
                BookCopyId = availableCopy.CopyId,
                UserId = lentRecordDto.UserId,
                AdminId = lentRecordDto.AdminId,
                LentDate = lendDate,
                DueDate = dueDate
            };


            availableCopy.IsAvailable = false;
            availableCopy.LastBorrowedDate = lendDate;


            book.RentCount++;
            //book.TotalCopies = book.BookCopies.Count;


            var rentHistory = new RentHistory
            {
                BookCopyId = availableCopy.CopyId,
                UserId = lentRecordDto.UserId,
                IAdminId = lentRecordDto.AdminId,
                LendDate = lendDate,
                DueDate = dueDate
                //ReturnDate = null // Not returned yet
            };


            await _lentRecordRepository.LendNormalBook(lentRecord, rentHistory, availableCopy, book);

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Book lent successfully",
                Data = true
            };
        }


        public async Task<ApiResponse<bool>> LendNormalBookByCopyId(LendByCopyIdDto lendByCopyIdDto)
        {
           
            var bookCopy = await _lentRecordRepository.GetBookCopyById(lendByCopyIdDto.BookCopyId);
            var user = await _lentRecordRepository.GetUserById(lendByCopyIdDto.UserId);
            var admin = await _lentRecordRepository.GetAdminById(lendByCopyIdDto.AdminId);

          
            if (bookCopy == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Book copy not found",
                    Data = false
                };
            }

            
            if (!bookCopy.IsAvailable)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Book copy is not available",
                    Data = false
                };
            }

           
            if (user == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "User not found",
                    Data = false
                };
            }

          
            if (admin == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Admin not found",
                    Data = false
                };
            }

        
            var book = await _lentRecordRepository.GetNormalBookWithCopies(bookCopy.BookId);

            if (book == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Associated book not found",
                    Data = false
                };
            }

           
            var lendDate = DateTime.Now;
            var dueDate = lendDate.AddDays(lendByCopyIdDto.DueDays);

            var lentRecord = new LentRecord
            {
                BookCopyId = bookCopy.CopyId,
                UserId = lendByCopyIdDto.UserId,
                AdminId = lendByCopyIdDto.AdminId,
                LentDate = lendDate,
                DueDate = dueDate
            };

            bookCopy.IsAvailable = false;
            bookCopy.LastBorrowedDate = lendDate;

           
            book.RentCount++;

           
            var rentHistory = new RentHistory
            {
                BookCopyId = bookCopy.CopyId,
                UserId = lendByCopyIdDto.UserId,
                IAdminId = lendByCopyIdDto.AdminId,
                LendDate = lendDate,
                DueDate = dueDate
            };

         
            await _lentRecordRepository.LendNormalBook(lentRecord, rentHistory, bookCopy, book);

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Book lent successfully",
                Data = true
            };
        }


        public async Task<ApiResponse<List<LentRecordAdminDto>>> GetLentRecordForAdminAsync(int userid)
        {
            var lentRecords = await _lentRecordRepository.GetLentRecordWithDetailsbyuserid(userid);

            if (lentRecords == null || !lentRecords.Any())
            {
                return new ApiResponse<List<LentRecordAdminDto>>
                {
                    Success = false,
                    Message = "No lent records found",
                    Data = null
                };
            }

            var lentRecordDtos = new List<LentRecordAdminDto>();
            var book = await _lentRecordRepository.GetBookById(lentRecords[0].BookCopy.BookId);

            foreach (var lentRecord in lentRecords)
            {
                

                var currentDateTime = DateTime.UtcNow;
                var statusValue = (int)(lentRecord.DueDate - currentDateTime).TotalMinutes;
                var maxvalue = (int)(lentRecord.DueDate-lentRecord.LentDate).TotalMinutes;
                string status = statusValue > 0
                    ? $"{statusValue / 1440} days {(statusValue % 1440) / 60} hours remaining"
                    : $"{Math.Abs(statusValue) / 1440} days {Math.Abs(statusValue % 1440) / 60} hours over";

                var lentRecordDto = new LentRecordAdminDto
                {
                    Id = lentRecord.Id,
                    UserId = lentRecord.UserId,
                    UserName = lentRecord.User.FullName,
                    UserEmail = lentRecord.User.Email,
                    AdminId = lentRecord.AdminId,
                    AdminName = lentRecord.Admin.FullName,
                    BookId = book.Id,
                    BookTitle = book.Title,
                    BookISBN = book.ISBN,
                    BookAuthor = book.Author,
                    BookGenre = string.Join(", ", book.Genre),
                    BookPublishYear = book.PublishYear,
                    BookCopyId = lentRecord.BookCopyId,
                    BookCondition = lentRecord.BookCopy.Condition,
                    LentDate = lentRecord.LentDate,
                    DueDate = lentRecord.DueDate,
                    Status = status,
                    StatusValue = statusValue,
                    MaxValue = maxvalue
                };

                lentRecordDtos.Add(lentRecordDto);
            }

            return new ApiResponse<List<LentRecordAdminDto>>
            {
                Success = true,
                Message = "Lent records retrieved successfully",
                Data = lentRecordDtos
            };
        }



        public async Task<ApiResponse<List<LentRecordAdminDto>>> GetAllLentRecordsAsync()
        {
            var lentRecords = await _lentRecordRepository.GetAllLentRecordsWithDetailsAsync();

            if (lentRecords == null || !lentRecords.Any())
            {
                return new ApiResponse<List<LentRecordAdminDto>>
                {
                    Success = false,
                    Message = "No lent records found",
                    Data = null
                };
            }

            var lentRecordDtos = new List<LentRecordAdminDto>();

            foreach (var lentRecord in lentRecords)
            {
                var book = await _lentRecordRepository.GetBookById(lentRecord.BookCopy.BookId);

                var currentDateTime = DateTime.UtcNow;
                var statusValue = (int)(lentRecord.DueDate - currentDateTime).TotalMinutes;
                var maxvalue = (int)(lentRecord.DueDate - lentRecord.LentDate).TotalMinutes;
                string status = statusValue > 0
                    ? $"{statusValue / 1440} days {(statusValue % 1440) / 60} hours remaining"
                    : $"{Math.Abs(statusValue) / 1440} days {Math.Abs(statusValue % 1440) / 60} hours over";

                var lentRecordDto = new LentRecordAdminDto
                {
                    Id = lentRecord.Id,
                    UserId = lentRecord.UserId,
                    UserName = lentRecord.User.FullName,
                    UserEmail = lentRecord.User.Email,
                    AdminId = lentRecord.AdminId,
                    AdminName = lentRecord.Admin.FullName,
                    BookId = book.Id,
                    BookTitle = book.Title,
                    BookISBN = book.ISBN,
                    BookAuthor = book.Author,
                    BookGenre = string.Join(", ", book.Genre),
                    BookPublishYear = book.PublishYear,
                    BookCopyId = lentRecord.BookCopyId,
                    BookCondition = lentRecord.BookCopy.Condition,
                    LentDate = lentRecord.LentDate,
                    DueDate = lentRecord.DueDate,
                    Status = status,
                    StatusValue = statusValue,
                    MaxValue=maxvalue
                };

                lentRecordDtos.Add(lentRecordDto);
            }

            return new ApiResponse<List<LentRecordAdminDto>>
            {
                Success = true,
                Message = "All lent records retrieved successfully",
                Data = lentRecordDtos
            };
        }


        public async Task<ApiResponse<PaginatedResult<LentHistoryAdminDto>>> GetAllRentHistory(int page, int pageSize)
        {
            try
            {
                // Fetch rent history records from the repository
                var (records, totalRecords) = await _lentRecordRepository.GetAllRentHistory(page, pageSize);
                var Book = await _lentRecordRepository.GetBookById(records[0].BookCopy.BookId);


                if (records == null || !records.Any())
                {
                    return new ApiResponse<PaginatedResult<LentHistoryAdminDto>>
                    {
                        Success = false,
                        Message = "No history found",
                        Data = null
                    };
                }

                var lentHistoryDtos = new List<LentHistoryAdminDto>();

                foreach (var rec in records)
                {
                    int statusValue;
                    string status;
                    int maxvalue;
                    var currentDateTime = DateTime.UtcNow;

                    // Calculate the status and status value
                    if (rec.ReturnDate == null)
                    {
                        statusValue = (int)(rec.DueDate - currentDateTime).TotalMinutes;
                        maxvalue = (int)(rec.DueDate - rec.LendDate).TotalMinutes;
                        status = statusValue > 0
                            ? $"{statusValue / 1440} days {(statusValue % 1440) / 60} hours remaining"
                            : $"{Math.Abs(statusValue) / 1440} days {Math.Abs(statusValue % 1440) / 60} hours overdue";
                    }
                    else
                    {
                        statusValue = 0;
                        status = "Closed";
                        maxvalue = 0;
                    }

                    // Construct DTO
                    var lentRecordDto = new LentHistoryAdminDto
                    {
                        Id = rec.Id,
                        UserId = rec.UserId,
                        UserName = rec.User.FullName,
                        UserEmail = rec.User.Email,
                        IAdminId = rec.IAdminId,
                        RAdminId = rec.RAdminId,
                        IAdminName = rec.IssuingAdmin?.FullName,  // Get Issuing Admin Name
                        RAdminName = rec.ReceivingAdmin?.FullName, // Get Receiving Admin Name
                        BookId = rec.BookCopy.BookId,
                        BookTitle = Book.Title,
                        BookISBN = Book.ISBN,
                        BookAuthor =Book.Author,
                        BookGenre = string.Join(", ", Book.Genre),
                        BookPublishYear = Book.PublishYear,
                        BookCopyId = rec.BookCopyId,
                        BookCondition = rec.BookCopy.Condition,
                        LentDate = rec.LendDate,
                        DueDate = rec.DueDate,
                        ReturnDate = rec.ReturnDate,
                        Status = status,
                        StatusValue = statusValue,
                        MaxValue =maxvalue
                    };

                    lentHistoryDtos.Add(lentRecordDto);
                }

                // Create a paginated result
                var paginatedResult = new PaginatedResult<LentHistoryAdminDto>
                {
                    Items = lentHistoryDtos,
                    TotalCount = totalRecords,
                    CurrentPage = page,
                    PageSize = pageSize
                };

                return new ApiResponse<PaginatedResult<LentHistoryAdminDto>>
                {
                    Success = true,
                    Message = "Records retrieved successfully",
                    Data = paginatedResult
                };
            }
            catch (Exception ex)
            {
                
                return new ApiResponse<PaginatedResult<LentHistoryAdminDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving rent history",
                    Errors = new List<string> { ex.Message },
                    Data = null
                };
            }
        }




    }
}
