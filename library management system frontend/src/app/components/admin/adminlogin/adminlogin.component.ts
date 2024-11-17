import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AdminLoginRequest } from '../../../models/interfaces/admin-login-request.interface';
import { AuthService } from '../../../services/admin-services/auth.service';

@Component({
  selector: 'app-adminlogin',
  templateUrl: './adminlogin.component.html',
  styleUrl: './adminlogin.component.css'
})
export class AdminloginComponent {
  adminLoginRequest: AdminLoginRequest = {
    emailOrNic: '',
    password: ''
  };
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  onAdminLogin() {
    this.authService.adminLogin(this.adminLoginRequest).subscribe(
      (response) => {
        if (response.success) {
          console.log('Admin logged in successfully:', response.data);
          // Navigate to the admin dashboard
          this.router.navigate(['/admin']); 
        } else {
          console.error('Login failed:', response.errors);
          this.errorMessage = 'Invalid username or password.';
        }
      },
      (error) => {
        console.error('An error occurred:', error);
        this.errorMessage = 'Server error. Please try again later.';
      }
    );
  }
}
