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
export class LandingLayoutComponent  {

  signupForm: FormGroup;
  loginForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router) 

    {
   
      this.signupForm = this.fb.group(
        {
          firstName: ['', Validators.required],
          lastName: ['', Validators.required],
          userNic: [''],
          phoneNumber: ['', Validators.required],
          email: ['', [Validators.required, Validators.email]],
          address: ['', [Validators.required]],
          password: ['', [Validators.required, Validators.minLength(6)]],
          confirmPassword: ['', Validators.required],
        },
        { validators: this.passwordMatchValidator }
      );

    this.loginForm = this.fb.group({
      emailOrNic: ['', Validators.required],
      password: ['', Validators.required],
    });

  }



  passwordMatchValidator(form: FormGroup) {
    return form.get('password')?.value === form.get('confirmPassword')?.value
      ? null
      : { passwordMismatch: true };
  }

  
  onSubmit() {
    if (this.signupForm.valid) {
      console.log('Form Submitted', this.signupForm.value);
      const formData = this.signupForm.value;
      this.authService.createuser(formData).subscribe(
        (response) =>{
          if (response.success) {
            console.log('User create successfully:', response.data);
          } else {
            console.error('User create : ', response.errors);
            
          }
        },
        (error) =>{
          console.error('An error occurred:', error);
        }
      );
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