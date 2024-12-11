import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../../../environments/environment.testing';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseurl = environment.apiBaseUrl;
  private Url = `${this.baseurl}/User/`;

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
}
