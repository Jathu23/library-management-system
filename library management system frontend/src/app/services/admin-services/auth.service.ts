import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AdminLoginRequest } from '../../models/interfaces/admin-login-request.interface';
import { ApiResponse } from '../../models/interfaces/api-response.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // private readonly adminUrl = 'http://localhost:5149/api/Admin/Aminlogin';
  private readonly userUrl='http://localhost:5149/api/User/login';
  constructor(private http: HttpClient) {}

  login(adminLoginRequest: AdminLoginRequest): Observable<ApiResponse<string>> {
    return this.http.post<ApiResponse<string>>(this.userUrl, adminLoginRequest);
  }


}
