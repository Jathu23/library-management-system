import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private Url = `https://localhost:7261/api/User/`;

  constructor(private http: HttpClient) {}

  GetUserEmailsByPrefix(prefix:string): Observable<any> {
    return this.http.get<any>(this.Url + `GetUserEmails?prefix=${prefix}`);
  }
  GetUserByEmailorNic(emaiornic:string){
    return this.http.get<any>(this.Url + `getSingleUserByNICorEmail?emailorNic=${emaiornic}`);
  }

}
