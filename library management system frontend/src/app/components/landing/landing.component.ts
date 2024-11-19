import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/admin-services/auth.service';

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
      console.log('Login request:', loginRequest);

      this.authService.login(loginRequest).subscribe(
        (response) => {
          if (response.success) {
            
            sessionStorage.setItem('LoggedInUser', JSON.stringify(response.data));
            console.log('User data saved in sessionStorage:', response.data);
            const userDataString = sessionStorage.getItem('LoggedInUser') || '{}'; 
            const userData = JSON.parse(userDataString);
            console.log(userData);

            if (userData) {
              
              if (userData.role === 'admin') { 
                this.router.navigate(['/admin']);
              } else if (userData.role === 'user') {
                this.router.navigate(['/user']);
              } else {
                console.error('Unknown role:', userData.role);
                alert('Unknown user role. Please contact support.');
              }
            } else {
              console.error('User data not found in sessionStorage');
              alert('An error occurred during login. Please try again.');
            }

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

