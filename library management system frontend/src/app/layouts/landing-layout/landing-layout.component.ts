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
  isLoading = false;
  signupForm: FormGroup;
  loginForm!: FormGroup;
isLoading2: boolean = false;
isLoading3:  boolean = false;

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
      this.isLoading2=true;
      console.log('Form Submitted', this.signupForm.value);
      const formData = this.signupForm.value;
      this.authService.createuser(formData).subscribe(
        (response) =>{
          if (response.success) {

            setTimeout(() => {
              this.isLoading2=false;
              this.isLoading3=true;
            }, 1000);
            localStorage.setItem('token',response.data.token);
              setTimeout(() => {
                if (response.data.role == "admin") {
                  this.isLoading3=false;
                  this.router.navigate(['/admin/dashboard/audio-books']); 
                }else{
                  this.isLoading3=false;
                  this.router.navigate(['/user']); 
                }
              }, 3300);
          
            console.log('User create successfully:', response.data);
          } else {
            this.isLoading2=false;
            console.error('User create : ', response.errors);
            
          }
        },
        (error) =>{
          setTimeout(
            ()=>{
              this.isLoading2 = false;
             
            },500
           );
           setTimeout(() => {
            alert(error.error.errors[0]);
          }, 520);
          console.error('An error occurred:', error);
        }
      );
    }
  }

  onLogin() {
   
    this.isLoading = true;
   

    if (this.loginForm.valid) {
      const formData = this.loginForm.value;
      console.log('Login Data:', formData);

      this.authService.login(formData).subscribe(
        (response) => {
          if (response.success) {
            setTimeout(() => {
              this.isLoading=false;
              this.isLoading3=true;
            }, 1000);
            localStorage.setItem('token',response.data.token);
              setTimeout(() => {
                if (response.data.role == "admin") {
                  localStorage.setItem('isLocked',"true");
                  this.isLoading3=false;
                  this.router.navigate(['/admin/dashboard/audio-books']); 
                }else{
                  this.isLoading3=false;
                  this.router.navigate(['/user']); 
                }
              }, 3300);
            console.log( response.data.role); 
            
          } else {
            
              this.isLoading = false;
          
            console.error('Login failed:', response.errors);
         
          }
        },
        (error) => {
          setTimeout(
            ()=>{
              this.isLoading = false;
             
            },300
           );
           setTimeout(() => {
            alert(error.error.errors[0]);
          }, 320);
          console.error('An error occurred:', error);
         
        }
      );

    }
     else {
      console.log('Form is invalid');
    }
  }
}