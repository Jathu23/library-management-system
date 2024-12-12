import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  email: string = '';
  otp: string = '';
  generatedOTP: string = '';
  otpTimer: number = 60;
  otpInterval: any;

  newPassword: string = '';
  confirmPassword: string = '';

  emailError: string = '';
  otpError: string = '';
  passwordError: string = '';

  isEmailValid: boolean = false;
  step: number = 1; // 1: Email Input, 2: OTP, 3: New Password

  constructor() {}

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

    // Simulate OTP generation
    this.generatedOTP = Math.floor(10000 + Math.random() * 90000).toString();
    console.log('Generated OTP:', this.generatedOTP); // For debugging
    this.step = 2;

    // Start OTP timer
    this.startOTPTimer();
  }

  startOTPTimer() {
    this.otpTimer = 60; // Reset Timer
    this.otpInterval = setInterval(() => {
      if (this.otpTimer > 0) {
        this.otpTimer--;
      } else {
        clearInterval(this.otpInterval);
        this.otpError = 'OTP has expired. Please request a new one.';
      }
    }, 1000);
  }

  verifyOTP() {
    if (this.otp !== this.generatedOTP) {
      this.otpError = 'Invalid OTP. Please try again.';
      return;
    }
    clearInterval(this.otpInterval);
    this.step = 3;
  }

  updatePassword() {
    if (this.newPassword.length < 6 || this.newPassword !== this.confirmPassword) {
      this.passwordError = 'Passwords do not match or are too short.';
      return;
    }

    alert('Password updated successfully!');
    // Simulate backend call here
    this.resetComponent();
  }

  resetComponent() {
    this.email = '';
    this.otp = '';
    this.generatedOTP = '';
    this.newPassword = '';
    this.confirmPassword = '';
    this.isEmailValid = false;
    this.step = 1;
    clearInterval(this.otpInterval);
  }
}
