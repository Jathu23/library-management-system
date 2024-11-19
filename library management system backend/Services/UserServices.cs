using Azure.Core;
using BCrypt.Net;
using library_management_system.Database.Entiy;
using library_management_system.DTOs;
using library_management_system.DTOs.User;
using library_management_system.Repositories;
using library_management_system.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json;

namespace library_management_system.Services
{
    public class UserServices
    {
        private readonly UserRepo _userRepo;
        private readonly ImageService _imageService;
        private readonly BCryptService _bCryptService;
        private readonly JwtService _jwtService;
        private readonly LoginRepository _loginRepository;

        public UserServices(UserRepo userRepo,
                            ImageService imageService,
                            BCryptService bCryptService,
                            JwtService jwtService,
                            LoginRepository loginRepository)
        {
            _userRepo = userRepo;
            _imageService = imageService;
            _bCryptService = bCryptService;
            _jwtService = jwtService;
            _loginRepository = loginRepository;
        }

        public async Task<ApiResponse<string>> CreateUser(UserRequstModel userRequestDto)
        {
            var response = new ApiResponse<string>();
            var exLoginData = await _loginRepository.GetByEmailOrNic(userRequestDto.Email);


            try
            {
                if (exLoginData != null)
                    throw new Exception("A user with this email already exists.");

                var profileImagePath = await SaveProfileImage(userRequestDto.ProfileImage);

                var user = new User
                {
                    UserNic = userRequestDto.UserNic,
                    FirstName = userRequestDto.FirstName,
                    LastName = userRequestDto.LastName,
                    FullName = $"{userRequestDto.FirstName} {userRequestDto.LastName}",
                    Email = userRequestDto.Email,
                    PhoneNumber = userRequestDto.PhoneNumber,
                    Address = userRequestDto.Address,
                    PasswordHash = _bCryptService.HashPassword(userRequestDto.Password),
                    ProfileImage = profileImagePath,
                    IsActive = userRequestDto.IsActive,
                    IsSubscribed = userRequestDto.IsSubscribed,
                    RegistrationDate = DateTime.Now
                };

              var Createduser = await _userRepo.CreateUser(user);
               
             

                var logindata = new LoginT
                {
                    Email = userRequestDto.Email,
                    PasswordHash = _bCryptService.HashPassword(userRequestDto.Password),
                    NIC = Createduser.UserNic,
                    MemberId = Createduser.Id,
                    Role = "user"

                };

                var state = await _loginRepository.Addlogdata(logindata);

                if (Createduser != null && state == true)
                {
                    response.Success = true;
                    response.Message = "User created successfully.";
                    response.Data = JsonSerializer.Serialize(new
                    {
                        Token = _jwtService.GenerateToken(user),
                        Role = "user"
                    });


                }
                else
                {
                    throw new Exception("error");

                }




            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while creating the user.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponse<string>> LoginUser(LoginRequest loginRequest)
        {
            var response = new ApiResponse<string>();

            var user = await _userRepo.GetUserByEmailOrNic(loginRequest.EmailOrNic);
            if (user == null || !_bCryptService.VerifyPassword(loginRequest.Password, user.PasswordHash))
            {
                response.Success = false;
                response.Message = "Login failed";
                response.Errors.Add("Invalid email or password.");
                return response;
            }

            response.Success = true;
            response.Message = "Login successful";
            response.Data = _jwtService.GenerateToken(user);

            return response;
        }

        private async Task<string> SaveProfileImage(IFormFile? profileImage)
        {
            if (profileImage == null)
                return "defaultimg.jpg";

            var image = new List<IFormFile> { profileImage };
            var imagePath = await _imageService.SaveImages(image, "userimages");
            return imagePath.First();
        }


        public async Task<ApiResponse<List<User>>> GetAllUsers()
        {
            var response = new ApiResponse<List<User>>();

            try
            {

                var users = await _userRepo.GetAllUsers();


                if (users == null || users.Count == 0)
                {
                    response.Success = false;
                    response.Message = "No users found.";
                }
                else
                {
                    response.Success = true;
                    response.Message = "Users retrieved successfully.";
                    response.Data = users;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while fetching users.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }
        public async Task<ApiResponse<string>> SoftDelete(int id)
        {
            var response = new ApiResponse<string>();

            try
            {
                var user = await _userRepo.SoftDelete(id);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                    return response;
                }

                user.IsActive = false;


                response.Success = true;
                response.Message = "User soft deleted successfully.";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred during the soft delete operation.";
                response.Errors.Add(ex.Message);
                return response;
            }
        }
        public async Task<ApiResponse<User>> GetUserByNICorEmail(string emailOrNic)
        {
            var response = new ApiResponse<User>();

            if (string.IsNullOrWhiteSpace(emailOrNic))
            {
                response.Success = false;
                response.Message = "Email or NIC must be provided.";
                return response;
            }

            try
            {

                var user = await _userRepo.GetUserByNICorEmail(emailOrNic);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                    return response;
                }

                if (user.IsActive)
                {
                    response.Success = true;
                    response.Message = "User retrieved successfully.";
                    response.Data = user;
                }
                else
                {
                    response.Success = false;
                    response.Message = "User is not active.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while fetching the user.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }





        public async Task<ApiResponse<List<User>>> GetAllDisabledUsers()
        {
            var response = new ApiResponse<List<User>>();

            try
            {

                var users = await _userRepo.GetAllDisabeledUsers();

                if (users == null || users.Count == 0)
                {
                    response.Success = false;
                    response.Message = "No disabled users found.";
                }
                else
                {
                    response.Success = true;
                    response.Message = "Disabled users retrieved successfully.";
                    response.Data = users;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while fetching the disabled users.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponse<User>> DeleteUserPermanently(int id)
        {
            var response = new ApiResponse<User>();

            if (id <= 0)
            {
                response.Success = false;
                response.Message = "Invalid user ID.";
                return response;
            }

            var user = await _userRepo.DeleteUserPermanently(id);

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else
            {
                response.Success = true;
                response.Message = "User deleted permanently.";
                response.Data = user; // Return deleted user details if needed
            }

            return response;
        }
     
        public async Task<ApiResponse<bool>> UpdateUser(UserInfoUpdateDto userInfoUpdate)
        {
            var response = new ApiResponse<bool>();

            try
            {
             
                var existingUser = await _userRepo.Getuserid(userInfoUpdate.Id);

                if (existingUser == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                    return response;
                }

               
              
                if (userInfoUpdate.ProfileImage != null)
                {
                    existingUser.ProfileImage = await SaveProfileImage(userInfoUpdate.ProfileImage);
                }

              

                existingUser.FirstName = userInfoUpdate.FirstName ?? existingUser.FirstName;
                existingUser.LastName = userInfoUpdate.LastName ?? existingUser.FirstName;
                existingUser.Email = userInfoUpdate.Email ?? existingUser.LastName;
                existingUser.PhoneNumber = userInfoUpdate.PhoneNumber ?? existingUser.PhoneNumber;
                existingUser.Address = userInfoUpdate.Address ?? existingUser.Address;
                existingUser.FullName = $"{existingUser.FirstName} {existingUser.LastName}";

                var updatedUser = await _userRepo.UpdateUser(existingUser);

                response.Success = true;
                response.Message = "User updated successfully.";
                response.Data = updatedUser;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while updating the user.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

    }

}




