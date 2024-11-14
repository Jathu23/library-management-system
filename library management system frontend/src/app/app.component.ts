import { Component} from '@angular/core';
import { AuthService } from './services/admin-services/auth.service';
import { AdminLoginRequest } from './models/interfaces/admin-login-request.interface';
import { ApiResponse } from './models/interfaces/api-response.interface';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

 
  constructor(private authService: AuthService) {}
  
  private adminLoginRequest: AdminLoginRequest = {
    emailOrNic: '11',
    password: '1'
  };

  onclick() {
    this.authService.login(this.adminLoginRequest).subscribe(
      (response: ApiResponse<string>) => {
        if (response.success) {
          console.log(response.message, response);
        } else {
          console.log(response.message, response.errors);
        }
      },
      (error) => {
        // Handle HTTP errors (e.g., 400 Bad Request)
        if (error.status === 400) {
          console.log(error.error.message, error.error);  // You can process the error here, like displaying a message
        } else {
          console.error('An error occurred:', error);  // Log or handle other errors (e.g., 500 Internal Server Error)
        }
      }
    );
  }
  
 

  
}
