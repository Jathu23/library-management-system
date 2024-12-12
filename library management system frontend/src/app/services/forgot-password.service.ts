import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ForgotPasswordService {
  private apiUrl = 'https://localhost:7261/api/ForgotPassword'; 

  constructor(private http: HttpClient) {}

  sendToken(email: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/send-token?email=${email}`, {});
  }

  resetPassword(email: string, otpCode: string, newPassword: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/reset-password`, {
      email,
      otpCode,
      newPassword
    });
  }
}
