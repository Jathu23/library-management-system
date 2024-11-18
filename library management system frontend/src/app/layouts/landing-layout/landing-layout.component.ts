import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-landing-layout',
  templateUrl: './landing-layout.component.html',
  styleUrl: './landing-layout.component.css'
})
export class LandingLayoutComponent {
 // Admin login form
 adminLoginForm: FormGroup;

 // User login form
 userLoginForm: FormGroup;

 constructor(private fb: FormBuilder) {
   // Admin login form initialization
   this.adminLoginForm = this.fb.group({
     adminUsername: ['', [Validators.required]],
     adminPassword: ['', [Validators.required]],
   });

   // User login form initialization
   this.userLoginForm = this.fb.group({
     userUsername: ['', [Validators.required]],
     userPassword: ['', [Validators.required]],
   });
 }

 // Admin login submit
 onAdminLogin(): void {
   if (this.adminLoginForm.valid) {
     const loginData = this.adminLoginForm.value;
     console.log('Admin Login Data:', loginData);
     // Implement admin login logic here (e.g., authentication API)
     alert('Admin Login Successful!'); // Placeholder for real logic
   }
 }

 // User login submit
 onUserLogin(): void {
   if (this.userLoginForm.valid) {
     const loginData = this.userLoginForm.value;
     console.log('User Login Data:', loginData);
     // Implement user login logic here (e.g., authentication API)
     alert('User Login Successful!'); // Placeholder for real logic
   }
 }
}
