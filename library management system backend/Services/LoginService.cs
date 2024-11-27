using library_management_system.DTOs;
using library_management_system.DTOs.LoginPort;
using library_management_system.Repositories;
using library_management_system.Utilities;
using Microsoft.AspNetCore.Identity.Data;

namespace library_management_system.Services
{
    public class LoginService
    {
        private readonly LoginRepository _repository;
        private readonly BCryptService _bCryptService;
        private readonly JwtService _jwtService;
        private readonly UserRepo _userRepo;
        private readonly AdminRepo _adminRepo;

        public LoginService(LoginRepository repository , 
            BCryptService bCryptService,
            JwtService jwtService,
            UserRepo userRepo,
            AdminRepo adminRepo)
        {
            _repository = repository;
            _bCryptService = bCryptService;
            _jwtService = jwtService;
            _userRepo = userRepo;
            _adminRepo = adminRepo;
        }

        public async Task<ApiResponse<AuthResponse>> Login(LoginRequstDto loginRequest)
        {
            var response = new ApiResponse<AuthResponse>();

            var user = await _repository.GetByEmailOrNic(loginRequest.EmailOrNic);
            var isActive = await _repository.GetUserByEmailOrNic(loginRequest.EmailOrNic);

            if (user == null || !_bCryptService.VerifyPassword(loginRequest.Password, user.PasswordHash))
            {
                response.Success = false;
                response.Message = "Login failed";
                response.Errors.Add("Invalid email or password.");
                return response;
            }
            else
            {
                if (user.Role == "user")
                {
                    if (isActive != null && isActive.IsActive == true)
                    {
                        var LoginUser = await _userRepo.Getuserid(user.MemberId);

                        response.Success = true;
                        response.Message = "Login successful";
                        response.Data = new AuthResponse
                        {
                            Token = _jwtService.GenerateToken(LoginUser),
                            Role = "user"
                        };

                        return response;
                    }
                    else
                    {
                        if (isActive != null && isActive.IsActive == false)
                        {
                            response.Success = false;
                            response.Message = "User Not Active";
                            response.Errors.Add("User status is Not Active.");
                            return response;
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "User Not find";
                            response.Errors.Add("User is Not find");
                            return response;
                        }
                    }

                }
                else
                {
                    if (user.Role == "admin" && user != null)
                    {
                        var Loginadmin = await _adminRepo.GetAdminById(user.MemberId);

                        response.Success = true;
                        response.Message = "Login successful";
                        response.Data = new AuthResponse
                        {
                            Token = _jwtService.GenerateAdminToken(Loginadmin),
                            Role = "admin"
                        };

                        return response;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Invalid password or userId";
                        response.Errors.Add("Admin login failed");
                        return response;
                    }


                }

            }
          
        }
    }
}
