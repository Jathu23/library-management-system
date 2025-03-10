import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../../../environments/environment.testing';
import { catchError, throwError } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseurl = environment.apiBaseUrl;
  private Url = `${this.baseurl}/User/`;

  

  private suBestUrl = `https://localhost:7261/api/User/subscribed-and-best?count`

  constructor(private http: HttpClient) {}

  GetUserEmailsByPrefix(prefix:string): Observable<any> {
    return this.http.get<any>(this.Url + `GetUserEmails?prefix=${prefix}`);
  }
  GetUserByEmailorNic(emailOrNic:string){
    return this.http.get<any>(this.Url + `getSingleUserByNICorEmail?emailorNic=${emailOrNic}`);
  }

  updateUser(userData: any): Observable<any> {
    const formData: FormData = new FormData();
  
    // Add the user fields to FormData
    formData.append('Id', userData.id);
    formData.append('UserNic', userData.userNic);
    formData.append('FirstName', userData.firstName);
    formData.append('LastName', userData.lastName);
    formData.append('Email', userData.email);
    formData.append('PhoneNumber', userData.phoneNumber);
    formData.append('Address', userData.address);
  
    // Add profile image if it exists
    if (userData.profileImage instanceof File) {
      formData.append('ProfileImage', userData.profileImage, userData.profileImage.name);
    }
  
    // Perform the update request (PUT request)
    return this.http.put<any>(`${this.Url}UpdateUser`, formData);
  }
  
  
  


  // functions for dasboard-users

  // Get total user count
  getTotalUserCount(): Observable<number> {
    return this.http.get<number>(`${this.Url}count-all-users`);
  }

  // Get active user count
  getActiveUserCount(): Observable<number> {
    return this.http.get<number>(`${this.Url}active-count`);
  }

  getNonActiveUserCount(): Observable<number> {
    return this.http.get<number>(`${this.Url}non-active-count`);
  }

  getSubscribedUserCount(): Observable<number> {
    return this.http.get<number>(`${this.Url}subscribed-user-count`);
  }

  getSubscribedAndBestUsers(count: number): Observable<any> {
    return this.http.get(`${this.suBestUrl}/subscribed-and-best`, {
      params: { count: count.toString() },
    });
  }

  private baseUrlLentById = 'https://localhost:7261/api/Lent';

  getLentReportByUserId(userId: number): Observable<any> {
    const url = `${this.baseUrlLentById}/Lent-Report-ByUserid?userid=${userId}`;
    return this.http.get<any>(url).pipe(
      catchError(this.handleError) // Error handling
    );
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    console.error('An error occurred:', error.message);
    return throwError(() => new Error('Error fetching Lent Report.'));
  }

  private apiUrl = 'https://localhost:7261/api/User';

  getUserBookCount(): Observable<{ count: number }> {
    return this.http.get<{ count: number }>(`${this.apiUrl}/Userbooks/count`);
  }

}
