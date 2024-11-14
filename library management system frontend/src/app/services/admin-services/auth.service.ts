import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AdminLoginRequest } from '../../models/interfaces/admin-login-request.interface';
import { ApiResponse } from '../../models/interfaces/api-response.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly url = 'https://localhost:7261/api/Admin/Aminlogin';

  constructor(private http: HttpClient) {}

  login(adminLoginRequest: AdminLoginRequest): Observable<ApiResponse<string>> {
    return this.http.post<ApiResponse<string>>(this.url, adminLoginRequest);
  }



}
