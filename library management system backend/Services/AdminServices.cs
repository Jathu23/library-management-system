using library_management_system.Database.Entiy;
using library_management_system.DTOs;
using library_management_system.DTOs.Admin;
using library_management_system.Repositories;
using library_management_system.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace library_management_system.Services
{
    public class AdminServices
    {
        private readonly AdminRepo _adminRepo;
        private readonly ImageService _imageService;
        private readonly BCryptService _bcryptService;
        private readonly JwtService _jwtService;

        public AdminServices(AdminRepo adminRepo,
                            ImageService imageService,
                            BCryptService bCryptService,
                            JwtService jwtService)
        {
            _adminRepo = adminRepo;
            _imageService = imageService;
            _bcryptService = bCryptService;
            _jwtService = jwtService;
        }

        public async Task<ApiResponse<string>> CreateAdmin(AdminRequstModel AdminRequstDto)
        {
            var response = new ApiResponse<string>();
            var exuser = await _adminRepo.GetAdminByEmailOrNic(AdminRequstDto.Email);

          try
            {
                if (exuser == null)
                    throw new Exception("An Admin with this email already exists.");

                var profileImagePath = await SaveProfileImage(AdminRequstDto.ProfileImage);

                var admin = new Admin
                {
                    AdminNic = AdminRequstDto.AdminNic,
                    FirstName = AdminRequstDto.FirstName,
                    LastName = AdminRequstDto.LastName,
                    FullName = $"{AdminRequstDto.FirstName} {AdminRequstDto.LastName}",
                    Email = AdminRequstDto.Email,
                    ProfileImage = AdminRequstDto.ProfileImage,
                    PasswordHash = _bcryptService.HashPassword(AdminRequstDto.Password)

                };
                await _adminRepo.CreateAdmin(admin);
                response.Success = true;
                response.Message = "Admin created successfully.";
                response.Data = _jwtService.GenerateAdminToken(admin);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while creating the Admin.";
                response.Errors.Add(ex.Message);
            }
            return response;
        }


        public async Task<ApiResponse<string>> LoginAdmin(AdminLoginRequset adminLoginRequset)
        {
            var response = new ApiResponse<string>();

            var admin = await _adminRepo.GetAdminByEmailOrNic(adminLoginRequset.EmailOrNic);
            if (admin == null || !_bcryptService.VerifyPassword(adminLoginRequset.Password, admin.PasswordHash))
            {
                response.Success = false;
                response.Message = "Login failed";
                response.Errors.Add("Invalid email or password.");
                return response;
            }

            response.Success = true;
            response.Message = "Login successful";
            response.Data = _jwtService.GenerateAdminToken(admin);

            return response;
        }


        private async Task<string> SaveProfileImage(IFormFile? profileImage)
        {
            if (profileImage == null)
                return "default1img.jpg";

            var image = new List<IFormFile> { profileImage };
            var imagePath = await _imageService.SaveImages(image, "adminImages");
            return imagePath.First();
        }

    }
}
