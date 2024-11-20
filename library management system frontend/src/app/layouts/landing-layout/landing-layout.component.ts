import { Component } from '@angular/core';
import { OnInit } from '@angular/core';

import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service/auth.service';

@Component({
  selector: 'app-landing-layout',
  templateUrl: './landing-layout.component.html',
  styleUrl: './landing-layout.component.css'
})
export class LandingLayoutComponent  implements OnInit {

  signupForm: FormGroup;
  loginForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router) 

    {
   
    this.signupForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required],
      address: ['', Validators.required],
      profileImage: [''],
      registrationDate: [new Date(), Validators.required] // Auto-filled with the current date and time
    });

    this.loginForm = this.fb.group({
      emailOrNic: ['', Validators.required],
      password: ['', Validators.required],
    });

  }

  ngOnInit(): void {
    // This will automatically fill the registration date when the component is initialized
    this.signupForm.patchValue({
      registrationDate: new Date().toISOString()
    });
  }

  // Form submission handler
  onSubmit(): void {
    if (this.signupForm.valid) {
      const formData = this.signupForm.value;
      console.log(formData); // Here you would send the form data to your API
    }
  }

  onLogin() {
    if (this.loginForm.valid) {
      const formData = this.loginForm.value;
      console.log('Login Data:', formData);

      this.authService.login(formData).subscribe(
        (response) => {
          if (response.success) {
            console.log('Admin logged in successfully:', response.data);
              if (response.data.role == "admin") {
                this.router.navigate(['/admin']); 
              }else{
                this.router.navigate(['/user']); 
              }
            console.log( response.data.role); 
            
          } else {
            console.error('Login failed:', response.errors);
            
          }
        },
        (error) => {
          console.error('An error occurred:', error);
          
        }
      );

    }
     else {
      console.log('Form is invalid');
    }
  }
}