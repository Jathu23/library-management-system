import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class RentService {

  private rentUrl = `https://localhost:7261/api/Lent/`;

  constructor(private http: HttpClient) {}

  getlentrecByuserid(id:number): Observable<any> {
    return this.http.get<any>(this.rentUrl + `lent-records-id?id=${id}`);
  }
}
