using Azure.Core;
using BCrypt.Net;
using library_management_system.Database.Entiy;
using library_management_system.DTOs;
using library_management_system.DTOs.User;
using library_management_system.Repositories;
using library_management_system.Utilities;
using MailSend.Enums;
using MailSend.Models;
using MailSend.Service;
using Microsoft.AspNetCore.Mvc;
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
        private readonly LentService _lentService;
        private readonly sendmailService _sendmailService;

        public UserServices(
            UserRepo userRepo,
            ImageService imageService,
            BCryptService bCryptService,
            JwtService jwtService,
            LoginRepository loginRepository,
            LentService lentService,
            sendmailService sendmailService)
        {
            _userRepo = userRepo;
            _imageService = imageService;
            _bCryptService = bCryptService;
            _jwtService = jwtService;
            _loginRepository = loginRepository;
            _lentService = lentService;
            _sendmailService = sendmailService;
        }

        public async Task<ApiResponse<AuthResponse>> CreateUser(UserRequstModel userRequestDto)
        {
            var response = new ApiResponse<AuthResponse>();
            var exLoginData = await _loginRepository.GetByEmailOrNic(userRequestDto.Email);
            var nicalreadyexists = await _loginRepository.IsNicAvailable(userRequestDto.UserNic);
                
            try
            {
                if (exLoginData != null)
                    throw new Exception("A user with this email already exists.");
                if(nicalreadyexists)
                    throw new Exception("A user with this Nic already exists.");

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
                    ProfileImage = profileImagePath,
                    IsActive = true,
                    IsSubscribed = false,
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
                    response.Data = new AuthResponse
                    {
                        Token = _jwtService.GenerateToken(user),
                        Role = "user"
                    };
                    var sendMailRequest = new SendMailRequest
                    {
                        EmailType = EmailTypes.accountcreate,
                        Name = $"{userRequestDto.FirstName} {userRequestDto.LastName}",
                        Email= userRequestDto.Email


                    };
                    var res = await _sendmailService.Sendmail(sendMailRequest).ConfigureAwait(false);

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

        //public async Task<ApiResponse<string>> LoginUser(LoginRequest loginRequest)
        //{
        //    var response = new ApiResponse<string>();

        //    var user = await _userRepo.GetUserByEmailOrNic(loginRequest.EmailOrNic);
        //    if (user == null || !_bCryptService.VerifyPassword(loginRequest.Password, user.PasswordHash))
        //    {
        //        response.Success = false;
        //        response.Message = "Login failed";
        //        response.Errors.Add("Invalid email or password.");
        //        return response;
        //    }

        //    response.Success = true;
        //    response.Message = "Login successful";
        //    response.Data = _jwtService.GenerateToken(user);

        //    return response;
        //}

        private async Task<string> SaveProfileImage(IFormFile? profileImage)
        {
            if (profileImage == null)
                return "defaultimg.jpg";

            var image = new List<IFormFile> { profileImage };
            var imagePath = await _imageService.SaveImages(image, "userimages");
            return imagePath.First();
        }


        public async Task<ApiResponse<PaginatedResult<UserDto>>> GetAllActiveUsers(int page, int pageSize)
        {
            try
            {

                if (page <= 0)
                {
                    return new ApiResponse<PaginatedResult<UserDto>>
                    {
                        Success = false,
                        Message = "Page number must be greater than 0.",
                        Data = null
                    };
                }

                if (pageSize <= 0 || pageSize > 100)
                {
                    return new ApiResponse<PaginatedResult<UserDto>>
                    {
                        Success = false,
                        Message = "Page size must be greater than 0 and less than or equal to 100.",
                        Data = null
                    };
                }


                var (subscribers, totalCount) = await _userRepo.GetAllActiveUsers(page, pageSize);

                if (subscribers == null || !subscribers.Any())
                {
                    return new ApiResponse<PaginatedResult<UserDto>>
                    {
                        Success = false,
                        Message = "No Active users found for the given pagination parameters.",
                        Data = null
                    };
                }


                var subscriberDtos = subscribers.Select(user => new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    UserNic = user.UserNic,
                   


                }).ToList();

                var result = new PaginatedResult<UserDto>
                {
                    Items = subscriberDtos,
                    TotalCount = totalCount,
                    CurrentPage = page,
                    PageSize = pageSize
                };


                return new ApiResponse<PaginatedResult<UserDto>>
                {
                    Success = true,
                    Message = "Active users retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {

                return new ApiResponse<PaginatedResult<UserDto>>
                {
                    Success = false,
                    Message = $"An error occurred while retrieving the Active users: {ex.Message}",
                    Data = null
                };
            }
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

                if (!user.IsActive)
                {

                    response.Success = false;
                    response.Message = "User is already soft-deleted.";
                    return response;
                }

                user.IsActive = false;


                response.Success = true;
                response.Message = "User is already softDeleted";
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
        public async Task<ApiResponse<UserDetialDto>> GetUserByNICorEmail(string emailOrNic)
        {
            var response = new ApiResponse<UserDetialDto>();

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

                var userdetial = new UserDetialDto() 
                {
                    Id=user.Id,
                    UserNic =user.UserNic,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FullName = user.FullName,   
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    ProfileImage = user.ProfileImage,
                    RegistrationDate = user.RegistrationDate,
                    IsActive = user.IsActive,
                    IsSubscribed= user.IsSubscribed,
                    BorrowStatus = await _lentService.CanUserBorrowBooks(user.Id)

                    
                };



                response.Success = true;
                    response.Message = "User retrieved successfully.";
                    response.Data = userdetial;
                
               
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while fetching the user.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }





        public async Task<ApiResponse<PaginatedResult<UserDto>>> GetAllNonactiveUsers(int page, int pageSize)
        {
            try
            {

                if (page <= 0)
                {
                    return new ApiResponse<PaginatedResult<UserDto>>
                    {
                        Success = false,
                        Message = "Page number must be greater than 0.",
                        Data = null
                    };
                }

                if (pageSize <= 0 || pageSize > 100)
                {
                    return new ApiResponse<PaginatedResult<UserDto>>
                    {
                        Success = false,
                        Message = "Page size must be greater than 0 and less than or equal to 100.",
                        Data = null
                    };
                }


                var (subscribers, totalCount) = await _userRepo.GetAllNonactiveUsers(page, pageSize);

                if (subscribers == null || !subscribers.Any())
                {
                    return new ApiResponse<PaginatedResult<UserDto>>
                    {
                        Success = false,
                        Message = "No nonActive users found for the given pagination parameters.",
                        Data = null
                    };
                }


                var subscriberDtos = subscribers.Select(user => new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    UserNic = user.UserNic,



                }).ToList();

                var result = new PaginatedResult<UserDto>
                {
                    Items = subscriberDtos,
                    TotalCount = totalCount,
                    CurrentPage = page,
                    PageSize = pageSize
                };


                return new ApiResponse<PaginatedResult<UserDto>>
                {
                    Success = true,
                    Message = "Active users retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {

                return new ApiResponse<PaginatedResult<UserDto>>
                {
                    Success = false,
                    Message = $"An error occurred while retrieving the Active users: {ex.Message}",
                    Data = null
                };
            }
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
                }else
                {
                    if (existingUser.UserNic == userInfoUpdate.UserNic)
                    {

                    }
                    else
                    {
                        // Check if the NIC is already in use
                        var nicalreadyexists = await _loginRepository.IsNicAvailable(userInfoUpdate.UserNic);

                        if (nicalreadyexists)
                        {
                            throw new Exception("A user with this Nic already exists.");
                        }
                        else
                        {
                            // If NIC is available, update the UserNic of the existing user
                            existingUser.UserNic = userInfoUpdate.UserNic;
                        }
                    }

                }


                if (userInfoUpdate.ProfileImage != null)
                {
                    existingUser.ProfileImage = await SaveProfileImage(userInfoUpdate.ProfileImage);
                }



                existingUser.FirstName = userInfoUpdate.FirstName ?? existingUser.FirstName;
                existingUser.LastName = userInfoUpdate.LastName ?? existingUser.FirstName;
              
                existingUser.PhoneNumber = userInfoUpdate.PhoneNumber ?? existingUser.PhoneNumber;
                existingUser.Address = userInfoUpdate.Address ?? existingUser.Address;
                existingUser.FullName = $"{existingUser.FirstName} {existingUser.LastName}";

                var updatedUser = await _userRepo.UpdateUser(existingUser);
                var updatelogindata = await _loginRepository.UpdateUserLoginData(existingUser.Email, userInfoUpdate.UserNic);

                    if (updatedUser && updatelogindata)
                {
                    response.Success = true;
                    response.Message = "User updated successfully.";
                    response.Data = updatedUser;
                }
                else
                {
                    throw new Exception("fail updating the user.");
                }
               
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while updating the user.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponse<PaginatedResult<usersearchDTO>>> SearchUsersAsync(string searchString, int pageNumber, int pageSize)
        {
            try
            {
                var (users, totalRecords) = await _userRepo.SearchAsync(searchString, pageNumber, pageSize);

                var userDtos = users.Select(u => new usersearchDTO
                {
                    Id = u.Id,
                    UserNic = u.UserNic,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Address = u.Address

                }).ToList();

                var paginatedResult = new PaginatedResult<usersearchDTO>
                {
                    Items = userDtos,
                    TotalCount = totalRecords,
                    CurrentPage = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponse<PaginatedResult<usersearchDTO>>
                {
                    Success = true,
                    Message = "Users retrieved successfully.",
                    Data = paginatedResult
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<PaginatedResult<usersearchDTO>>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<List<string>>> GetUserEmailsByPrefix(string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                return new ApiResponse<List<string>>
                {
                    Success = false,
                    Message = "Prefix cannot be empty",
                    Data = null,
                    Errors = new List<string> { "Invalid input: prefix is null or empty" }
                };
            }

            var usernames = await _userRepo.GetUserEmailsByPrefix(prefix);

            if (usernames == null || !usernames.Any())
            {
                return new ApiResponse<List<string>>
                {
                    Success = false,
                    Message = "No usernames found",
                    Data = new List<string>(),
                    Errors = new List<string>()
                };
            }

            return new ApiResponse<List<string>>
            {
                Success = true,
                Message = "Usernames retrieved successfully",
                Data = usernames
            };
        }
        public async Task<ApiResponse<PaginatedResult<UserDto>>> GetSubscribedUsersWithPagination(int page, int pageSize)
        {
            try
            {
               
                if (page <= 0)
                {
                    return new ApiResponse<PaginatedResult<UserDto>>
                    {
                        Success = false,
                        Message = "Page number must be greater than 0.",
                        Data = null
                    };
                }

                if (pageSize <= 0 || pageSize > 100)
                {
                    return new ApiResponse<PaginatedResult<UserDto>>
                    {
                        Success = false,
                        Message = "Page size must be greater than 0 and less than or equal to 100.",
                        Data = null
                    };
                }

               
                var (subscribers, totalCount) = await _userRepo.GetSubscribedUsersAsync(page, pageSize);

                if (subscribers == null || !subscribers.Any())
                {
                    return new ApiResponse<PaginatedResult<UserDto>>
                    {
                        Success = false,
                        Message = "No subscribed users found for the given pagination parameters.",
                        Data = null
                    };
                }

                
                var subscriberDtos = subscribers.Select(user => new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    UserNic=user.UserNic,
                    
                   
                  
                }).ToList();

                var result = new PaginatedResult<UserDto>
                {
                    Items = subscriberDtos,
                    TotalCount = totalCount,
                    CurrentPage = page,
                    PageSize = pageSize
                };

                
                return new ApiResponse<PaginatedResult<UserDto>>
                {
                    Success = true,
                    Message = "Subscribed users retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
               
                return new ApiResponse<PaginatedResult<UserDto>>
                {
                    Success = false,
                    Message = $"An error occurred while retrieving the subscribed users: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<OverallUserReportDto> GetOverallUserReport()
        {
            return await _userRepo.GetUserStatistics();
        }


        //funcionjs for dashboard

		public async Task<int> GetAllUsersCountAsync()
		{
			return await _userRepo.GetAllUserCountAsync();
		}

		public async Task<int> GetActiveUserCountAsync()
		{
            return await _userRepo.GetActiveUserCountAsync();
		}

		public async Task<int> GetNonActiveUserCountAsync()
		{
			return await _userRepo.GetNonActiveUserCountAsync();
		}

		public async Task<int> GetSubscribedUserCountAsync()
		{
            return await _userRepo.GetSubscribedUserCountAsync();
		}


        public async Task<ApiResponse<usersearchDTO>> GetUserById(int id)
        {
            var response = new ApiResponse<usersearchDTO>();

            if (id <= 0)
            {
                response.Success = false;
                response.Message = "Invalid user ID.";
                return response;
            }

            try
            {
                var user = await _userRepo.GetByid(id);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                    return response;
                }

                var userDto = new usersearchDTO
                {
                    Id = user.Id,
                    UserNic = user.UserNic,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    IsActive = user.IsActive,
                    IsSubscribed = user.IsSubscribed,
                    ProfileImage = user.ProfileImage
                };

                response.Success = true;
                response.Message = "User retrieved successfully.";
                response.Data = userDto;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while fetching the user.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

		public async Task<List<User>> FilterUsersBySubscribedAndBest(int count)
		{
			try
			{
				return await _userRepo.FilterUsersBySubscribedAndBest(count);
			}
			catch (Exception ex)
			{
				// Log the exception (use a logging framework or service as appropriate)
				Console.WriteLine($"An error occurred: {ex.Message}");

				// Optionally, rethrow the exception or return a default value
				throw; // Rethrow the exception to be handled by higher-level logic
			}
		}

        public async Task<int> GetuserCountAsync()
        {
            try
            {
                return await _userRepo.GetActiveUserCountAsync();
            }
            catch (Exception ex)
            {
                // Log the exception details (e.g., using a logging framework like Serilog or NLog)
                Console.WriteLine($"An error occurred while counting audiobooks: {ex.Message}");

                // Optionally rethrow the exception or return a default value
                // throw;
                return 0; // Returning 0 in case of an error
            }
        }


    }

}




