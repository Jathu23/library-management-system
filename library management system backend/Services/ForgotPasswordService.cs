﻿using library_management_system.DTOs;
using library_management_system.Repositories;
using library_management_system.Utilities;
using MailSend.Enums;
using MailSend.Models;
using MailSend.Service;

public class ForgotPasswordService
{
    private readonly ForgotPasswordRepository _forgotPasswordRepository;
    private readonly UserRepo _userRepo;
    private readonly AdminRepo _adminRepo;
    private readonly sendmailService _sendmailService;
    private readonly BCryptService _bCryptService;

    public ForgotPasswordService(ForgotPasswordRepository forgotPasswordRepository, UserRepo userRepo, AdminRepo adminRepo, sendmailService sendmailService, BCryptService bCryptService)
    {
        _forgotPasswordRepository = forgotPasswordRepository;
        _userRepo = userRepo;
        _adminRepo = adminRepo;
        _sendmailService = sendmailService;
        _bCryptService = bCryptService;
    }


    public async Task<ApiResponse<string>> GenerateAndSendTokenAsync(string email)
    {
        var response = new ApiResponse<string>();

        try
        {

            var tokenCode = new Random().Next(10000, 99999).ToString();

            await _forgotPasswordRepository.SaveTokenAsync(email, tokenCode);

            var sendMailRequest = new SendMailRequest
            {
                EmailType = EmailTypes.otp,
                Email = email,
                Otp = tokenCode
            };

            await _sendmailService.Sendmail(sendMailRequest);

            response.Success = true;
            response.Message = "OTP sent successfully.";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Error sending OTP.";
            response.Errors.Add(ex.Message);
        }

        return response;
    }

  
    public async Task<ApiResponse<string>> ValidateTokenAndUpdatePasswordAsync(string email, string tokenCode, string newPassword)
    {
        var response = new ApiResponse<string>();

        try
        {
            var tokenRecord = await _forgotPasswordRepository.GetLatestTokenAsync(email);

            if (tokenRecord == null || tokenRecord.TokenCode != tokenCode)
                throw new Exception("Invalid OTP.");


            if (DateTime.UtcNow > tokenRecord.CreatedAt.AddMinutes(2))
            {
                await _forgotPasswordRepository.DeleteTokenAsync(tokenRecord);
                throw new Exception("OTP has expired.");
            }

            var hash = _bCryptService.HashPassword(newPassword);

             var result = await _forgotPasswordRepository.UpdatePasswordAsync(email, hash);
            if (result)
            {

                await _forgotPasswordRepository.DeleteTokenAsync(tokenRecord);
                response.Success = true;
                response.Message = "Password updated successfully.";
            }
            else
            {
                throw new Exception("No account associated with this email.");
            }

          
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Error updating password.";
            response.Errors.Add(ex.Message);
        }

        return response;
    }
}
