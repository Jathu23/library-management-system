import { Component, OnInit } from '@angular/core';
import { ForgotPasswordService } from '../../../services/auth-service/forgot-password.service';


@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  email: string = '';
  otp: string = '';
  newPassword: string = '';
  confirmPassword: string = '';

  emailError: string = '';
  otpError: string = '';
  passwordError: string = '';
  successMessage: string = '';
  isEmailValid: boolean = false;

  step: number = 1;

  otpTimer: number = 120; 
  otpInterval: any;
  otpSentTime: number = 0; 

  constructor(private forgotPasswordService: ForgotPasswordService) {}

  ngOnInit(): void {}

  validateEmail() {
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    this.isEmailValid = emailPattern.test(this.email);
    this.emailError = this.isEmailValid ? '' : 'Please enter a valid email.';
  }

  sendOTP() {
    if (!this.isEmailValid) {
      this.emailError = 'Invalid email address!';
      return;
    }

    // Send OTP to the backend
    this.forgotPasswordService.sendToken(this.email).subscribe({
      next: (response) => {
        if (response.success) {
          this.successMessage = response.message;
          this.step = 2;
          this.otpSentTime = Date.now(); 
          this.startOTPTimer();
        } else {
          this.emailError = response.message;
        }
      },
      error: (error) => {
        this.emailError = 'Error sending OTP. Please try again.';
      }
    });
  }

  startOTPTimer() {
    this.otpTimer = 120; 
    this.otpInterval = setInterval(() => {
      const elapsedTime = Math.floor((Date.now() - this.otpSentTime) / 1000); 
      if (elapsedTime < 120) {
        this.otpTimer = 120 - elapsedTime; 
      } else {
        clearInterval(this.otpInterval); 
        this.otpError = 'OTP has expired. Please request a new one.';
      }
    }, 1000);
  }

  verifyOTP() {
    if (!this.otp) {
      this.otpError = 'Please enter the OTP.';
      return;
    }

    this.forgotPasswordService.verifyOtp( this.otp).subscribe({
      next: (response) => {
        if (response.success) {
          this.successMessage = response.message;
          this.step = 3;
        } else {
          this.otpError = response.message;
        }
      },
      error: (error) => {
        this.otpError = 'Invalid OTP. Please try again.';
      }
    });
  }

  updatePassword() {
    if (this.newPassword.length < 6 || this.newPassword !== this.confirmPassword) {
      this.passwordError = 'Passwords do not match or are too short.';
      return;
    }

    this.forgotPasswordService.resetPassword(this.email, this.otp, this.newPassword).subscribe({
      next: (response) => {
        if (response.success) {
          this.successMessage = 'Password updated successfully!';
          this.resetComponent();
        } else {
          this.passwordError = response.message;
        }
      },
      error: (error) => {
        this.passwordError = 'Error updating password. Please try again.';
      }
    });
  }

  resetComponent() {
    this.email = '';
    this.otp = '';
    this.newPassword = '';
    this.confirmPassword = '';
    this.isEmailValid = false;
    this.step = 1;
    this.successMessage = '';
    this.emailError = '';
    this.otpError = '';
    this.passwordError = '';
    clearInterval(this.otpInterval);
  }
}
