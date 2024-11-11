using BCrypt.Net;
using library_management_system.Database.Entiy;
using library_management_system.DTOs.User;
using library_management_system.Repositories;

namespace library_management_system.Services
{
    public class UserServices
    {
        private readonly UserRepo _userRepo;

        public UserServices(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User> CreateUser(UserRequstModel userRequestDto)
        {
            
            var user = new User
            {
                UserNic = userRequestDto.UserNic,
                FirstName = userRequestDto.FirstName,
                LastName = userRequestDto.LastName,
                FullName = $"{userRequestDto.FirstName} {userRequestDto.LastName}",
                Email = userRequestDto.Email,
                PhoneNumber = userRequestDto.PhoneNumber,
                Address = userRequestDto.Address,
                PasswordHash = HashPassword(userRequestDto.Password), 
                ProfileImage = userRequestDto.ProfileImage,
                IsActive = userRequestDto.IsActive,
                IsSubscribed = userRequestDto.IsSubscribed,
                RegistrationDate = DateTime.Now  
            };
            Console.WriteLine(user.Id);

            return await _userRepo.CreateUser(user);
        }

        private string HashPassword(string password)
        {
           
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

    }
}
