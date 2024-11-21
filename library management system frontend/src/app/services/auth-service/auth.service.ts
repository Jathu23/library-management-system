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


  constructor(private http: HttpClient) { }
  
//login method
  login(LoginRequest: any): Observable<ApiResponse<any>> {
    return this.http.post<ApiResponse<any>>(this.LoginUrl, LoginRequest);
  }
  private readonly adminCreateUrl = 'http://localhost:5149/api/Admin/CreateAdmin';



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
}
