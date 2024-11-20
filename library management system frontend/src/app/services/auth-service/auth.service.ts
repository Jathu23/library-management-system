import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponse } from '../../models/interfaces/api-response.interface';
import { Observable } from 'rxjs';

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

}
