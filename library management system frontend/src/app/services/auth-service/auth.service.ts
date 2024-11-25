import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponse } from '../../models/interfaces/api-response.interface';
import { Observable } from 'rxjs';
import { adminRequestModel } from '../../models/interfaces/add-admin.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly LoginUrl = 'https://localhost:7261/api/Login/login';
  private readonly adminCreateUrl = 'http://localhost:7261/api/Admin/CreateAdmin';
  private readonly userCreateUrl = 'https://localhost:7261/api/User/create';


  constructor(private http: HttpClient) { }
  
//login method
  login(LoginRequest: any): Observable<ApiResponse<any>> {
    return this.http.post<ApiResponse<any>>(this.LoginUrl, LoginRequest);
  }
 
  createAdmin(adminData: adminRequestModel): Observable<ApiResponse<string>> {
    const formData: FormData = new FormData();

    // Append form data for all admin fields
    formData.append('UserNic', adminData.AdminNic || '');
    formData.append('FirstName', adminData.FirstName);
    formData.append('LastName', adminData.LastName);
    formData.append('Email', adminData.Email);
    formData.append('Password', adminData.Password);

    return this.http.post<ApiResponse<string>>(this.adminCreateUrl, formData);
  }

  createuser(userData:any):Observable<ApiResponse<any>>{
    const formData = new FormData();

  formData.append('UserNic', userData.userNic || '');
  formData.append('FirstName', userData.firstName);
  formData.append('LastName', userData.lastName);
  formData.append('Email', userData.email);
  formData.append('PhoneNumber', userData.phoneNumber);
  formData.append('Address', userData.address);
  formData.append('Password', userData.password);

  // If there's a file, append it as 'ProfileImage'
  if (userData.ProfileImage) {
    formData.append('ProfileImage', userData.ProfileImage, userData.ProfileImage.name);
  } else {
    formData.append('ProfileImage', '');
  }
  console.log("api call form : ",formData);
    return this.http.post<ApiResponse<string>>(this.userCreateUrl, formData);
    
    
  }
}
