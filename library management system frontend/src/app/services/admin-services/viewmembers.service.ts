import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ViewmembersService {
  constructor(private http: HttpClient) {}

  private GetActiveUsers = `https://localhost:7261/api/User/GetAllActiveUsers`;

  getActiveUsers(currentPage: number, pageSize: number): Observable<any> {
    return this.http.get<any>(`${this.GetActiveUsers}?pageNumber=${currentPage}&pageSize=${pageSize}`);
  }

  private GetSuscribeUsers=`https://localhost:7261/api/User/subscribed-users`;

  getSubscribeUsers(currentPage: number, pageSize: number): Observable<any> {
    return this.http.get<any>(`${this.GetSuscribeUsers}?pageNumber=${currentPage}&pageSize=${pageSize}`);
  }

  private GetNonActiveUsers =`https://localhost:7261/api/User/GetAllDisabledUsers`;

  getNonActiveUsers(currentPage: number, pageSize: number): Observable<any> {
    return this.http.get<any>(`${this.GetNonActiveUsers}?pageNumber=${currentPage}&pageSize=${pageSize}`);
  }

private SearchUrl=`https://localhost:7261/api/User/Search`
getsearchUsers(currentPage: number, pageSize: number, searchstring: string): Observable<any> {
  return this.http.get<any>(`${this.SearchUrl}?searchString=${searchstring}&pageNumber=${currentPage}&pageSize=${pageSize}`);
}

private  activateUrl = `https://localhost:7261/api/Admin/UserAccountActive`;
activateUser(id: number): Observable<any> { 
  return this.http.get(`${this.activateUrl}?id= ${id}`); 
}
}
