using library_management_system.Database.Entiy;
using library_management_system.DTOs.LentRecord;
using library_management_system.DTOs;
using library_management_system.Repositories;

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
            book.TotalCopies = book.BookCopies.Count;


            var rentHistory = new RentHistory
            {
                BookCopyId = availableCopy.CopyId,
                UserId = lentRecordDto.UserId,
                AdminId = lentRecordDto.AdminId,
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


        public async Task<ApiResponse<LentRecordAdminDto>> GetLentRecordForAdminAsync(int lentRecordId)
        {
            var lentRecord = await _lentRecordRepository.GetLentRecordWithDetailsAsync(lentRecordId);

            if (lentRecord == null)
            {
                return new ApiResponse<LentRecordAdminDto>
                {
                    Success = false,
                    Message = "Lent record not found",
                    Data = null
                };
            }

          
            var book =  await _lentRecordRepository.GetBookById(lentRecord.BookCopy.BookId);

            var currentDateTime = DateTime.UtcNow;
            var statusValue = (int)(lentRecord.DueDate - currentDateTime).TotalMinutes;

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
                BookTitle =book.Title,
                BookISBN =book.ISBN,
                BookAuthor =book.Author,
                BookGenre = string.Join(", ", book.Genre),
                BookPublishYear =book.PublishYear,
                BookCopyId = lentRecord.BookCopyId,
                BookCondition = lentRecord.BookCopy.Condition,
                LentDate = lentRecord.LentDate,
                DueDate = lentRecord.DueDate,
                Status = status,
                StatusValue = statusValue
            };

            return new ApiResponse<LentRecordAdminDto>
            {
                Success = true,
                Message = "Lent record retrieved successfully",
                Data = lentRecordDto
            };
        }

    }
}
