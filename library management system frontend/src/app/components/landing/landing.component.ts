import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/admin-services/auth.service';
import { AdminLoginRequest } from '../../models/interfaces/admin-login-request.interface';
import { ApiResponse } from '../../models/interfaces/api-response.interface';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private router: Router, private authService: AuthService) {}



   userCredentials = { username: '2', password: '2' };


  onLogin(userType?: string) {
   
    let adminCredentials: AdminLoginRequest = {
      emailOrNic:this.username ,
      password: this.password
    };
    if (userType === 'admin') {

      console.log(adminCredentials);
    
       this.authService.login(adminCredentials).subscribe(
        (response: ApiResponse<string>) => {
          if (response.success) {
            console.log(response.message, response);
            this.router.navigate(['/admin']);
          } else {
            console.log(response.message, response.errors);
          }
        },
        (error) => {
          this.errorMessage = error.error.message+" "+error.error.errors;
          if (error.status === 400) {
            console.log(error.error.message, error.error);  
          } else {
            console.error('An error occurred:', error); 
          }
        }
      );

    }
     else if (userType === 'user') {
      if (this.username === this.userCredentials.username && this.password === this.userCredentials.password) {
        this.router.navigate(['/user']);
      } else {
        this.errorMessage = 'Invalid user credentials. Please try again.';
      }
    }
  }
}
