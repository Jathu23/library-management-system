using library_management_system.Database.Entiy;
using library_management_system.DTOs;
using library_management_system.DTOs.User;

namespace library_management_system.IService
{
    public interface IUserService
    {
         Task<ApiResponse<AuthResponse>> CreateUser(UserRequstModel userRequestDto);

        Task<string> SaveProfileImage(IFormFile? profileImage);


        Task<ApiResponse<PaginatedResult<UserDto>>> GetAllActiveUsers(int page, int pageSize);

        Task<ApiResponse<string>> SoftDelete(int id);


        Task<ApiResponse<UserDetialDto>> GetUserByNICorEmail(string emailOrNic);

        Task<ApiResponse<PaginatedResult<UserDto>>> GetAllNonactiveUsers(int page, int pageSize);

        Task<ApiResponse<User>> DeleteUserPermanently(int id);


        Task<ApiResponse<bool>> UpdateUser(UserInfoUpdateDto userInfoUpdate);

        Task<ApiResponse<PaginatedResult<usersearchDTO>>> SearchUsersAsync(string searchString, int pageNumber, int pageSize);

        Task<ApiResponse<List<string>>> GetUserEmailsByPrefix(string prefix);


        Task<ApiResponse<PaginatedResult<UserDto>>> GetSubscribedUsersWithPagination(int page, int pageSize);

        Task<OverallUserReportDto> GetOverallUserReport();

        Task<int> GetAllUsersCountAsync();

        Task<int> GetActiveUserCountAsync();

        Task<int> GetNonActiveUserCountAsync();

        Task<int> GetSubscribedUserCountAsync();

        Task<ApiResponse<usersearchDTO>> GetUserById(int id);

        Task<List<User>> FilterUsersBySubscribedAndBest(int count);



    }
}
