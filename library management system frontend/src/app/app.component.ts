import { Component} from '@angular/core';
import { AuthService } from './services/admin-services/auth.service';
import { AdminLoginRequest } from './models/interfaces/admin-login-request';
import { ApiResponse } from './models/interfaces/api-response.interface';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

 
  // constructor(private authService: AuthService) {}
  
  private adminLoginRequest: AdminLoginRequest = {
    emailOrNic: 'admin@example.com',
    password: 'password123'
  };

onclick() {
//  this.authService.login(this.adminLoginRequest).subscribe(
//       (response: ApiResponse<string>) => {
//         if (response.success) {
//           console.log(response.message, response.data);  
//         } else {
//           console.log(response.message, response.errors);  
//         }
      
  // });

  console.log("hgh");
  



}
 

  
}
