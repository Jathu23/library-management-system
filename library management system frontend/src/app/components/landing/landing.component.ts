import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/admin-services/auth.service';
import { AdminLoginRequest } from '../../models/interfaces/admin-login-request.interface';
import { ApiResponse } from '../../models/interfaces/api-response.interface';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent {
  form: FormGroup;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.form = this.fb.group({
      email: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required])
    });
  }

  onSubmit() {
    if (this.form.valid) {
      const loginRequest = {
        emailOrNic: this.form.value.email,
        password: this.form.value.password
      };
      console.log(loginRequest);

      this.authService.login(loginRequest).subscribe(
        (response) => {
          if (response.success) {
            this.router.navigate(['/user']); 
            alert('Login successful!');
          } else {
            console.error('Login failed:', response.message);
            this.errorMessage = response.message;
          }
        },
        (error) => {
          console.error('Error occurred during login:', error);
          alert('An error occurred during login. Please try again later.');
        }
      );
    }
  }

  closeForm() {
    this.form.reset();
    this.errorMessage = ''; 
    this.router.navigate(['']); 
  }
  

}
 
 