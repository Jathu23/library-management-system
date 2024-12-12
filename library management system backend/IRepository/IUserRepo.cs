using library_management_system.Database.Entiy;
using library_management_system.DTOs.User;

namespace library_management_system.IRepository
{
    public interface IUserRepo
    {
        Task<User?> CreateUser(User user);
        Task<User?> GetUserByEmailOrNic(string emailOrNic);
        Task<User?> GetUserByNICorEmail(string emailOrNic);
        Task<User?> Getuserid(int id);
        Task<(List<User> Users, int TotalCount)> GetAllActiveUsers(int pageNumber, int pageSize);
        Task<(List<User> Users, int TotalCount)> GetAllNonactiveUsers(int pageNumber, int pageSize);
        Task<User> SoftDelete(int id);
        Task<User?> ActivateUserAccount(int id);
        Task<User> DeleteUserPermanently(int id);
        Task<bool> UpdateUser(User updatedUserData);
        Task<(List<User> Users, int TotalCount)> SearchAsync(string searchString, int pageNumber, int pageSize);
        Task<List<string>> GetUserEmailsByPrefix(string prefix);
        Task<(List<User> Users, int TotalCount)> GetSubscribedUsersAsync(int pageNumber, int pageSize);
        Task<OverallUserReportDto> GetUserStatistics();
        Task<int> GetAllUserCountAsync();
        Task<int> GetActiveUserCountAsync();
        Task<int> GetNonActiveUserCountAsync();
        Task<int> GetSubscribedUserCountAsync();
        Task<User> GetByid(int id);
        Task<List<User>> FilterUsersBySubscribedAndBest(int count);
    }
}
