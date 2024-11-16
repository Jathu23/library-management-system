using library_management_system.DTOs;
using library_management_system.Repositories;

namespace library_management_system.Services
{
    public class ReturnService
    {
        private readonly ReturnRepository _repository;

        public ReturnService(ReturnRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<bool>> ReturnLentBook(int lentRecordId)
        {
          
            var lentRecord = await _repository.GetLentRecordById(lentRecordId);

            if (lentRecord == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Lent record not found",
                    Data = false
                };
            }

        
            var bookCopy = await _repository.GetBookCopyById(lentRecord.BookCopyId);
            if (bookCopy == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Book copy not found",
                    Data = false
                };
            }

            var book = await _repository.GetNormalBookById(bookCopy.BookId);
            if (book == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Book not found",
                    Data = false
                };
            }

            bookCopy.IsAvailable = true;

          
            var rentHistory = await _repository.GetRentHistoryByLentRecordId(lentRecordId);
            if (rentHistory == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Rent history not found",
                    Data = false
                };
            }

            rentHistory.ReturnDate = DateTime.Now;

        
            await _repository.ReturnLentBook(lentRecord, rentHistory, bookCopy);

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Book returned successfully",
                Data = true
            };
        }

    }
}
