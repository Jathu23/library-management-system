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

        public User Add(UserRequstModel user)
        {
            var newUser = new User()
            { 
                NIC = user.NIC,
                Name = user.Name,
                Email = user.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password)

            };

          return  _userRepo.Add(newUser);
           
        }
        public User get(string nic)
        {
            return _userRepo.get(nic);
        }
    }
}
