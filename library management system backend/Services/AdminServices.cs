using Azure;
using library_management_system.Database.Entiy;
using library_management_system.DTOs;
using library_management_system.DTOs.Admin;
using library_management_system.Repositories;
using library_management_system.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Text.Json;

namespace library_management_system.Services
{
    public class AdminServices
    {
        private readonly AdminRepo _adminRepo;
        private readonly ImageService _imageService;
        private readonly BCryptService _bcryptService;
        private readonly JwtService _jwtService;
        private readonly LoginRepository _loginRepo;
        private readonly UserRepo _userRepo;

        public AdminServices(AdminRepo adminRepo,
                            ImageService imageService,
                            BCryptService bCryptService,
                            JwtService jwtService,
                            LoginRepository loginRepository,
                            UserRepo userRepo)
        {
            _adminRepo = adminRepo;
            _imageService = imageService;
            _bcryptService = bCryptService;
            _jwtService = jwtService;
            _loginRepo = loginRepository;
            _userRepo = userRepo;
        }

        public async Task<ApiResponse<AuthResponse>> CreateAdmin(AdminRequstModel AdminRequstDto)
        {
            var response = new ApiResponse<AuthResponse>();
            var exLoginData = await _loginRepo.GetByEmailOrNic(AdminRequstDto.Email);
            var alladmins = await _adminRepo.GetAllAdmins();

          try
            {
                if (alladmins.Count() == 5)
                    throw new Exception("Only 5 admin accounts are allowed.");
               
                if (exLoginData != null)
                    throw new Exception("An User with this email  already exists.");

                var profileImagePath = await SaveProfileImage(AdminRequstDto.ProfileImage);

                var isMasterExists = alladmins.Any(admin => admin.IsMaster);

                var admin = new Admin
                {
                    AdminNic = AdminRequstDto.AdminNic,
                    FirstName = AdminRequstDto.FirstName,
                    LastName = AdminRequstDto.LastName,
                    FullName = $"{AdminRequstDto.FirstName} {AdminRequstDto.LastName}",
                    Email = AdminRequstDto.Email,
                    IsMaster = !isMasterExists, 
                    ProfileImage = profileImagePath
                };

                var Createdadmin   =  await _adminRepo.CreateAdmin(admin);

                var logindata = new LoginT
                {
                    Email = AdminRequstDto.Email,
                    PasswordHash = _bcryptService.HashPassword(AdminRequstDto.Password),
                    NIC = Createdadmin.AdminNic,
                    MemberId = Createdadmin.id,
                    Role = "admin"

                };

              var state =  await _loginRepo.Addlogdata(logindata);

                if (Createdadmin != null && state == true)
                {
                   

                    response.Success = true;
                    response.Message = isMasterExists? "Admin created successfully (slave account).": "Admin created successfully (master account).";

                    response.Data = new AuthResponse
                    {
                        Token = _jwtService.GenerateAdminToken(admin),
                        Role = "admin",
                        Ismaster = !isMasterExists,
                    };
                }
                else
                {
                    throw new Exception("error");

                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while creating the Admin.";
                response.Errors.Add(ex.Message);
            }
            return response;
        }


        //public async Task<ApiResponse<string>> LoginAdmin(AdminLoginRequset adminLoginRequset)
        //{
        //    var response = new ApiResponse<string>();

        //    var admin = await _adminRepo.GetAdminByEmailOrNic(adminLoginRequset.EmailOrNic);
        //    if (admin == null || !_bcryptService.VerifyPassword(adminLoginRequset.Password, admin.PasswordHash))
        //    {
        //        response.Success = false;
        //        response.Message = "Login failed";
        //        response.Errors.Add("Invalid email or password.");
        //        return response;
        //    }

        //    response.Success = true;
        //    response.Message = "Login successful";
        //    response.Data = _jwtService.GenerateAdminToken(admin);

        //    return response;
        //}


        private async Task<string> SaveProfileImage(IFormFile? profileImage)
        {
            if (profileImage == null)
                return "default1img.jpg";

            var image = new List<IFormFile> { profileImage };
            var imagePath = await _imageService.SaveImages(image, "adminImages");
            return imagePath.First();
        }



        public async Task<ApiResponse<List<Admin>>> GetAllAdmins()
        {
            var response = new ApiResponse<List<Admin>>();

            try
            {
                var admins = await _adminRepo.GetAllAdmins();

                if (admins == null || !admins.Any())
                {
                    response.Success = false;
                    response.Message = "No admins found.";
                }
                else
                {
                    response.Success = true;
                    response.Message = "Admins retrieved successfully.";
                    response.Data = admins;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while fetching admins.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponse<Admin?>> GetAdminById(int adminId)
        {
            var response = new ApiResponse<Admin?>();

            try
            {
                var admin = await _adminRepo.GetAdminById(adminId);

                if (admin == null)
                {
                    response.Success = false;
                    response.Message = "Admin not found.";
                }
                else
                {
                    response.Success = true;
                    response.Message = "Admin retrieved successfully.";
                    response.Data = admin;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while fetching the admin.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponse<string>> DeleteAdminById(int adminId)
        {
            var response = new ApiResponse<string>();

            try
            {
                var deleted = await _adminRepo.DeleteAdminById(adminId);

                if (!deleted)
                {
                    response.Success = false;
                    response.Message = "Admin not found or could not be deleted.";
                }
                else
                {
                    response.Success = true;
                    response.Message = "Admin deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while deleting the admin.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponse<string>?> ActiveteUserAccount(string NimOrEmail)
        {
            var response = new ApiResponse<string?>();

            try
            {
               
                var user = await _userRepo.ActivateUserAccount(NimOrEmail);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                    response.Data = null;
                    return response;
                }
                response.Success = true;
                response.Message = "User account activated successfully.";
                response.Data = $"User with NIC or Email '{NimOrEmail}' has been activated.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while activating the user account.";
                response.Errors.Add(ex.Message);
            }

            return response;


        }
    }
}
