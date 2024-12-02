using library_management_system.Database.Entiy;
using library_management_system.DTOs.LentRecord;
using library_management_system.DTOs;
using library_management_system.Repositories;
using System.Diagnostics.Eventing.Reader;

namespace library_management_system.Services
{
    public class LentService
    {
        private readonly LentRepository _lentRecordRepository;
        private readonly UserRepo _userRepository;
        private readonly AdminRepo _adminRepository;
        private readonly BookRepository _bookRepository;
        private readonly SubscriptionRepository _subscriptionRepository;
        


        public LentService(LentRepository lentRecordRepository, UserRepo userRepository, AdminRepo adminRepository, BookRepository bookRepository, SubscriptionRepository subscriptionRepository)
        {
            _lentRecordRepository = lentRecordRepository;
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _bookRepository = bookRepository;
            _subscriptionRepository = subscriptionRepository;
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


            var user = await _lentRecordRepository.GetUserById(lendByCopyIdDto.UserId);

            if (user == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "User not found",
                    Data = false
                };
            }
            var canborrow = await CanUserBorrowBooks(lendByCopyIdDto.UserId);
            if (!canborrow.CanBorrow)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = canborrow.Message,
                    Data = false
                };
            }
            var bookCopy = await _lentRecordRepository.GetBookCopyById(lendByCopyIdDto.BookCopyId);
            if (bookCopy == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Book copy not found",
                    Data = false
                };
            }
      
           
            var admin = await _lentRecordRepository.GetAdminById(lendByCopyIdDto.AdminId);

          
           

            
            if (!bookCopy.IsAvailable)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Book copy is not available",
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
            //var book = await _lentRecordRepository.GetBookById(lentRecords[0].BookCopy.BookId);

            foreach (var lentRecord in lentRecords)
            {
                

                var currentDateTime = DateTime.Now;
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
                    BookId = lentRecord.BookCopy.Book.Id,
                    BookTitle = lentRecord.BookCopy.Book.Title,
                    BookISBN = lentRecord.BookCopy.Book.ISBN,
                    BookAuthor = lentRecord.BookCopy.Book.Author,
                    BookGenre = string.Join(", ", lentRecord.BookCopy.Book.Genre),
                    BookPublishYear = lentRecord.BookCopy.Book.PublishYear,
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
                //var book = await _lentRecordRepository.GetBookById(lentRecord.BookCopy.BookId);

                var currentDateTime = DateTime.Now;  
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
                    BookId = lentRecord.BookCopy.Book.Id,
                    BookTitle = lentRecord.BookCopy.Book.Title,
                    BookISBN = lentRecord.BookCopy.Book.ISBN,
                    BookAuthor = lentRecord.BookCopy.Book.Author,
                    BookGenre = string.Join(", ", lentRecord.BookCopy.Book.Genre),
                    BookPublishYear = lentRecord.BookCopy.Book.PublishYear,
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
                //var Book = await _lentRecordRepository.GetBookById(records[0].BookCopy.BookId);


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
                    var currentDateTime = DateTime.Now;

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
                        BookTitle = rec.BookCopy.Book.Title,
                        BookISBN = rec.BookCopy.Book.ISBN,
                        BookAuthor = rec.BookCopy.Book.Author,
                        BookGenre = string.Join(", ", rec.BookCopy.Book.Genre),
                        BookPublishYear = rec.BookCopy.Book.PublishYear,
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

        public async Task<ApiResponse<List<LentRecordUserDto>>> GetLentRecordsByUserIdAsync(int userId)
        {
            try
            {

                var lentRecords = await _lentRecordRepository.GetLentRecordsByUserIdAsync(userId);

                if (lentRecords == null || !lentRecords.Any())
                {
                    return new ApiResponse<List<LentRecordUserDto>>
                    {
                        Success = false,
                        Message = "No lent records found for this user",
                        Data = null
                    };
                }

                var lentRecordDtos = new List<LentRecordUserDto>();

                foreach (var lentRecord in lentRecords)
                {

                    var book = await _lentRecordRepository.GetBookById(lentRecord.BookCopy.BookId);

                    if (book == null)
                    {
                        return new ApiResponse<List<LentRecordUserDto>>
                        {
                            Success = false,
                            Message = $"Book details not found for Book ID: {lentRecord.BookCopy.BookId}",
                            Data = null
                        };
                    }


                    var currentDateTime = DateTime.Now;
                    var statusValue = (int)(lentRecord.DueDate - currentDateTime).TotalMinutes;

                    string status = statusValue > 0
                        ? $"{statusValue / 1440} days {(statusValue % 1440) / 60} hours remaining"
                        : $"{Math.Abs(statusValue) / 1440} days {Math.Abs(statusValue % 1440) / 60} hours overdue";


                    var lentRecordDto = new LentRecordUserDto
                    {
                        Id = lentRecord.Id,
                        BookId = book.Id,
                        BookTitle = book.Title,
                        BookCopyId = lentRecord.BookCopyId,
                        BookAuthor = book.Author,
                        LentDate = lentRecord.LentDate,
                        DueDate = lentRecord.DueDate,
                        Status = status,
                        StatusValue = statusValue
                    };

                    lentRecordDtos.Add(lentRecordDto);
                }

                return new ApiResponse<List<LentRecordUserDto>>
                {
                    Success = true,
                    Message = "User-specific lent records retrieved successfully",
                    Data = lentRecordDtos
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<LentRecordUserDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving user-specific lent records",
                    Errors = new List<string> { ex.Message },
                    Data = null
                };
            }
        }


        public async Task<ApiResponse<List<LentHistoryUserDto>>> GetRentHistoryByUser(int userId)
        {
            try
            {

                var records = await _lentRecordRepository.GetRentHistoryByUser(userId);
             

                if (records == null || !records.Any())
                {
                    return new ApiResponse<List<LentHistoryUserDto>>
                    {
                        Success = false,
                        Message = "No history found for this user",
                        Data = null
                    };
                }

                var lentHistoryDtos = new List<LentHistoryUserDto>();

                foreach (var rec in records)
                {
                    int statusValue;
                    string status;
                    var currentDateTime = DateTime.Now;


                    if (rec.ReturnDate == null)
                    {
                        statusValue = (int)(rec.DueDate - currentDateTime).TotalMinutes;
                        status = statusValue > 0
                            ? $"{statusValue / 1440} days {(statusValue % 1440) / 60} hours remaining"
                            : $"{Math.Abs(statusValue) / 1440} days {Math.Abs(statusValue % 1440) / 60} hours overdue";
                    }
                    else
                    {
                        statusValue = 0;
                        status = "Closed";
                    }


                    var lentRecordDto = new LentHistoryUserDto
                    {
                        Id = rec.Id,
                        BookId = rec.BookCopy.BookId,
                        BookTitle = rec.BookCopy.Book.Title,
                        BookAuthor = rec.BookCopy.Book.Author,
                        BookCopyId = rec.BookCopyId,
                        LentDate = rec.LendDate,
                        DueDate = rec.DueDate,
                        ReturnDate = rec.ReturnDate,
                        Status = status,
                        StatusValue = statusValue
                    };

                    lentHistoryDtos.Add(lentRecordDto);
                }

                return new ApiResponse<List<LentHistoryUserDto>>
                {
                    Success = true,
                    Message = "Records retrieved successfully",
                    Data = lentHistoryDtos
                };
            }
            catch (Exception ex)
            {

                return new ApiResponse<List<LentHistoryUserDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving rent history",
                    Errors = new List<string> { ex.Message },
                    Data = null
                };
            }
        }

        public async Task<LendReportDto> GetLentReport(DateTime date)
        {
            var lentRecords = await _lentRecordRepository.GetAllLentRecords(date);

            var pendingLent = lentRecords
                .Where(r => r.ReturnDate == null )
                .Select(MapToLentRecordAdminDto)
                .ToList();

            var onTimeLent = lentRecords
                .Where(r => r.ReturnDate != null && r.ReturnDate <= r.DueDate)
                .Select(MapToLentRecordAdminDto)
                .ToList();

            var laterLent = lentRecords
                .Where(r => r.ReturnDate != null && r.ReturnDate > r.DueDate)
                .Select(MapToLentRecordAdminDto)
                .ToList();

            return new LendReportDto
            {
                Date = date,
                TotalRengings = lentRecords.Count,
                Pending = pendingLent.Count,
                OnTime = onTimeLent.Count,
                Later = laterLent.Count,
                PendingLent = pendingLent,
                OnTimeLent = onTimeLent,
                LaterLent = laterLent
            };
        }

        public async Task<LendReportDto> GetLentReportbyuserid(int userid)
        {
            var lentRecords = await _lentRecordRepository.GetAllLentRecordsbyuserid(userid);

            var pendingLent = lentRecords
                .Where(r => r.ReturnDate == null)
                .Select(MapToLentRecordAdminDto)
                .ToList();

            var onTimeLent = lentRecords
                .Where(r => r.ReturnDate != null && r.ReturnDate <= r.DueDate)
                .Select(MapToLentRecordAdminDto)
                .ToList();

            var laterLent = lentRecords
                .Where(r => r.ReturnDate != null && r.ReturnDate > r.DueDate)
                .Select(MapToLentRecordAdminDto)
                .ToList();

            return new LendReportDto
            {
                Date = DateTime.Now,
                TotalRengings = lentRecords.Count,
                Pending = pendingLent.Count,
                OnTime = onTimeLent.Count,
                Later = laterLent.Count,
                PendingLent = pendingLent,
                OnTimeLent = onTimeLent,
                LaterLent = laterLent
            };
        }
        public async Task<BookLendingReportsDto> GetBookLendingReports(int? bookId)
        {
            var reports = new List<AllBookRendingReportDto>();

            if (bookId.HasValue)
            {
                var book = await _lentRecordRepository.GetBookWithLendingDetailsAsync(bookId.Value);
                if (book == null)
                    throw new Exception($"Book with ID {bookId.Value} not found.");

                reports.Add(MapToDto(book));
            }
            else
            {
                var books = await _lentRecordRepository.GetAllBooksWithLendingDetailsAsync();
                reports = books.Select(MapToDto).ToList();
            }

            return new BookLendingReportsDto
            {
                Created = DateTime.Now,
                Reports = reports
            };
        }


        // Generate Lending Count Report
        public async Task<LendingCountReportsDto> GetLendingCountReportsAsync(int? bookId = null)
        {
            List<BookCopy> bookCopies;

            // Fetch data for all books or a single book
            if (bookId.HasValue)
            {
                bookCopies = await _lentRecordRepository.GetBookCopiesByBookIdAsync(bookId.Value);
            }
            else
            {
                bookCopies = await _lentRecordRepository.GetAllBookCopiesAsync();
            }

            // Prepare the report
            var report = new LendingCountReportsDto
            {
                Created = DateTime.Now,
                CountReports = bookCopies
                    .GroupBy(bc => bc.BookId)
                    .Select(group => new LendingCountReportDto
                    {
                       
                        BookID = group.Key,
                        TotalRentCount = group.Sum(bc => bc.lentcount),
                        induvalCopyrentcount = group.Select(bc => new LendingCountReportDto.InduvalCopyrentcount // Corrected reference
                        {
                            CoppyId = bc.CopyId,
                            RentCount = bc.lentcount
                        }).ToList()
                    })
                    .ToList()
            };

            return report;
        }

        public async Task<BorrowStatusDto> CanUserBorrowBooks(int userId)
        {
            var user = await _userRepository.Getuserid(userId);

            if (user == null)
                throw new Exception("User not found.");

            // Check if the user is subscribed
            if (!user.IsSubscribed)
            {
                // Free plan logic
                var freePlan = await _subscriptionRepository.GetFreePlanAsync();
                var booksBorrowedInLast30Days = await _lentRecordRepository.GetBorrowedCountInLastNDaysAsync(userId, 30);
                return new BorrowStatusDto
                {
                    CanBorrow = booksBorrowedInLast30Days < freePlan.BorrowLimit,
                    BorrowLimit = freePlan.BorrowLimit,
                    BooksBorrowed = booksBorrowedInLast30Days,
                    Message=$"free plan have {freePlan.BorrowLimit} borrow limit per month"
                };
            }

            // Retrieve active subscription
            var activeSubscription = await _subscriptionRepository.GetUserSubscriptionByUserIdAsync(userId);
            if (activeSubscription == null || activeSubscription.Status != "Active")
                throw new Exception("User does not have an active subscription.");

            var paymentDuration = activeSubscription.PaymentDuration;
            if (paymentDuration == null)
                throw new Exception("Payment duration data is missing for the subscription.");

            var borrowLimit = activeSubscription.SubscriptionPlan.BorrowLimit;
            var totalMonths = paymentDuration.Duration;

            // Calculate current cycle
            var daysElapsed = (DateTime.Now - activeSubscription.StartDate).Days;
            var currentCycle = (daysElapsed / 30) + 1;

            if (currentCycle > totalMonths)
                return new BorrowStatusDto
                {
                    CanBorrow = false,
                    BorrowLimit = borrowLimit,
                    BooksBorrowed = 0,
                    Message = "Subscription period has ended."
                };

            // Calculate borrow count for the current cycle
            var cycleStartDate = activeSubscription.StartDate.AddDays((currentCycle - 1) * 30);
            var cycleEndDate = cycleStartDate.AddDays(30).AddTicks(-1);
            var booksBorrowedInCycle = await _lentRecordRepository.GetBorrowedCountInDateRangeAsync(userId, cycleStartDate, cycleEndDate);

            return new BorrowStatusDto
            {
                CanBorrow = booksBorrowedInCycle < borrowLimit,
                BorrowLimit = borrowLimit,
                BooksBorrowed = booksBorrowedInCycle
            };
        }


        private AllBookRendingReportDto MapToDto(NormalBook book)
        {
            var rentDetails = book.BookCopies
                .SelectMany(bc => bc.RentHistories)
                .Select(rh => new AllBookRendingReportDto.BookRentdetial
                {
                    BookCopyId = rh.BookCopyId,
                    UserName = $"{rh.User.FirstName} {rh.User.LastName}",
                    IssuingAdmin = $"{rh.IssuingAdmin.FirstName} {rh.IssuingAdmin.LastName}",
                    ReceivingAdmin = rh.ReceivingAdmin != null
                        ? $"{rh.ReceivingAdmin.FirstName} {rh.ReceivingAdmin.LastName}"
                        : null,
                    LendDate = rh.LendDate,
                    DueDate = rh.DueDate,
                    ReturnDate = rh.ReturnDate
                })
                .ToList();

            return new AllBookRendingReportDto
            {
                BookId = book.Id,
                BookTitle = book.Title,
                ISBN = book.ISBN,
                Author = book.Author,
                BookRentDetails = rentDetails
            };
        }



        private LentHistoryAdminDto MapToLentRecordAdminDto(RentHistory record)
        {
            var currentDateTime = DateTime.Now;
          int  statusValue = (int)(record.DueDate - currentDateTime).TotalMinutes;
          int  maxvalue = (int)(record.DueDate - record.LendDate).TotalMinutes;

            return new LentHistoryAdminDto
            {
                Id = record.Id,
                UserId = record.UserId,
                UserName = $"{record.User.FirstName} {record.User.LastName}",
                UserEmail = record.User.Email,
                IAdminId = record.IAdminId,
                IAdminName = $"{record.IssuingAdmin.FirstName} {record.IssuingAdmin.LastName}",
                RAdminId = record.RAdminId,
                RAdminName = record.ReceivingAdmin != null
                    ? $"{record.ReceivingAdmin.FirstName} {record.ReceivingAdmin.LastName}"
                    : null,
                BookId = record.BookCopy.Book.Id,
                BookTitle = record.BookCopy.Book.Title,
                BookISBN = record.BookCopy.Book.ISBN,
                BookAuthor = record.BookCopy.Book.Author,
                BookGenre = string.Join(", ", record.BookCopy.Book.Genre),
                BookPublishYear = record.BookCopy.Book.PublishYear,
                BookCopyId = record.BookCopyId,
                BookCondition = record.BookCopy.Condition,
                LentDate = record.LendDate,
                DueDate = record.DueDate,
                ReturnDate = record.ReturnDate,
                Status = record.ReturnDate == null ? statusValue > 0
                            ? $"{statusValue / 1440} days {(statusValue % 1440) / 60} hours remaining"
                            : $"{Math.Abs(statusValue) / 1440} days {Math.Abs(statusValue % 1440) / 60} hours overdue" : (record.ReturnDate <= record.DueDate ? "On Time" : "Later"),
                StatusValue =statusValue,
                MaxValue = maxvalue
            };
        }

       



    }
}
