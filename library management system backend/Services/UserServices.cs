﻿using Azure.Core;
using BCrypt.Net;
using library_management_system.Database.Entiy;
using library_management_system.DTOs;
using library_management_system.DTOs.User;
using library_management_system.Repositories;
using library_management_system.Utilities;

namespace library_management_system.Services
{
    public class UserServices
    {
        private readonly UserRepo _userRepo;
        private readonly ImageService _imageService;
        private readonly BCryptService _bCryptService;
        private readonly JwtService _jwtService;

        public UserServices(UserRepo userRepo,
                            ImageService imageService,
                            BCryptService bCryptService,
                            JwtService jwtService)
        {
            _userRepo = userRepo;
            _imageService = imageService;
            _bCryptService = bCryptService;
            _jwtService = jwtService;
        }

        public async Task<ApiResponse<string>> CreateUser(UserRequstModel userRequestDto)
        {
            var response = new ApiResponse<string>();
            var exuser = await _userRepo.GetUserByEmailOrNic(userRequestDto.Email);
           

            try
            {
                if (exuser != null)            
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

                await _userRepo.CreateUser(user);
                response.Success = true;
                response.Message = "User created successfully.";
                response.Data = _jwtService.GenerateToken(user);
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


    }
}
