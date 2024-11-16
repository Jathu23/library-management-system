using library_management_system.Database.Entiy;
using library_management_system.DTOs.LentRecord;
using library_management_system.DTOs;
using library_management_system.Repositories;

namespace library_management_system.Services
{
    public class LentService
    {
        private readonly LentRepository _bookRepository;

        public LentService(LentRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ApiResponse<bool>> LendNormalBook(LentRecordDto lentRecordDto)
        {
      
            var book = await _bookRepository.GetNormalBookWithCopies(lentRecordDto.BookId);
            var user = await _bookRepository.GetUserById(lentRecordDto.UserId); 
            var admin = await _bookRepository.GetAdminById(lentRecordDto.AdminId); 



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
                LendDate = lendDate,
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

        
            await _bookRepository.LendNormalBook(lentRecord, rentHistory, availableCopy, book);

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Book lent successfully",
                Data = true
            };
        }

    }
}
