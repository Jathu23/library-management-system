import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/admin-services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  form: FormGroup;
  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.form = this.fb.group({
      FirstName: new FormControl('', [Validators.required]),
      LastName: new FormControl('', [Validators.required]),
      Password: new FormControl('', [Validators.required]),
      Email: new FormControl('', [Validators.required]),
      PhoneNumber: new FormControl('', [Validators.required]),
      confirmpassword: new FormControl('', [Validators.required]),
      UserNic:new FormControl(''),
      IsActive:true,
      IsSubscribed:false
    });
  }
  onSubmit() {
    console.log(this.form.value);
    const postdata = { ...this.form.value };
    
  }
// username: string = '';
  // password: string = '';
  // errorMessage: string = '';

  // constructor(private router: Router, private authService: AuthService) {}



  //  userCredentials = { username: '2', password: '2' };


  // onLogin(userType?: string) {
   
  //   let adminCredentials: AdminLoginRequest = {
  //     emailOrNic:this.username ,
  //     password: this.password
  //   };
  //   if (userType === 'admin') {

  //     console.log(adminCredentials);
    
  //      this.authService.login(adminCredentials).subscribe(
  //       (response: ApiResponse<string>) => {
  //         if (response.success) {
  //           console.log(response.message, response);
  //           this.router.navigate(['/admin']);
  //         } else {
  //           console.log(response.message, response.errors);
  //         }
  //       },
  //       (error) => {
  //         this.errorMessage = error.error.message+" "+error.error.errors;
  //         if (error.status === 400) {
  //           console.log(error.error.message, error.error);  
  //         } else {
  //           console.error('An error occurred:', error); 
  //         }
  //       }
  //     );

  //   }
  //    else if (userType === 'user') {
  //     if (this.username === this.userCredentials.username && this.password === this.userCredentials.password) {
  //       this.router.navigate(['/user']);
  //     } else {
  //       this.errorMessage = 'Invalid user credentials. Please try again.';
  //     }
  //   }
  // }
}
