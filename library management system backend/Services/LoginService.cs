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

            try
            {
                var user = await _repository.GetByEmailOrNic(loginRequest.EmailOrNic);

               
                if (user == null || !_bCryptService.VerifyPassword(loginRequest.Password, user.PasswordHash))
                {
                    response.Success = false;
                    response.Message = "Login failed";
                    response.Errors.Add("Invalid email or password.");
                    return response;
                }

              
                switch (user.Role)
                {
                    case "user":
                        var loginUser = await _userRepo.Getuserid(user.MemberId);
                        if (loginUser?.IsActive == true)
                        {
                            response.Success = true;
                            response.Message = "Login successful";
                            response.Data = new AuthResponse
                            {
                                Token = _jwtService.GenerateToken(loginUser),
                                Role = "user"
                            };
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "User not active";
                            response.Errors.Add("User status is not active.");
                        }
                        break;

                    case "admin":
                        var loginAdmin = await _adminRepo.GetAdminById(user.MemberId);
                        if (loginAdmin != null)
                        {
                            response.Success = true;
                            response.Message = "Login successful";
                            response.Data = new AuthResponse
                            {
                                Token = _jwtService.GenerateAdminToken(loginAdmin),
                                Role = "admin",
                                Ismaster = loginAdmin.IsMaster
                            };
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "Admin login failed";
                            response.Errors.Add("Invalid admin credentials.");
                        }
                        break;

                    default:
                        response.Success = false;
                        response.Message = "Invalid role";
                        response.Errors.Add("Unrecognized user role.");
                        break;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional, depending on logging implementation)
                Console.Error.WriteLine($"An error occurred during login: {ex.Message}");

                // Return a generic error response
                response.Success = false;
                response.Message = "An unexpected error occurred.";
                response.Errors.Add("Please try again later.");
            }

            return response;
        }

    }
}
