import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AdminLoginRequest, UserRequestModel, } from '../../models/interfaces/admin-login-request.interface';
import { ApiResponse } from '../../models/interfaces/api-response.interface';
import { adminRequestModel } from '../../models/interfaces/add-admin.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  
  
}
  


  // createUser(user: UserRequestModel): Observable<ApiResponse<string>> {
  //   const formData: FormData = new FormData();

  //   // Append form data for all the fields, including file if exists
  //   formData.append('UserNic', user.UserNic || '');
  //   formData.append('FirstName', user.FirstName);
  //   formData.append('LastName', user.LastName);
  //   formData.append('Email', user.Email);
  //   formData.append('PhoneNumber', user.PhoneNumber);
  //   formData.append('Address', user.Address);
  //   formData.append('Password', user.Password);
  //   formData.append('IsActive', user.IsActive.toString());
  //   formData.append('IsSubscribed', user.IsSubscribed.toString());

  //   // If there's a profile image, append it to the form data
  //   if (user.ProfileImage) {
  //     formData.append('ProfileImage', user.ProfileImage, user.ProfileImage.name);
  //   }

  //   return this.http.post<ApiResponse<string>>(this.userCreationUrl, formData);
  // }




