import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class RentService {

  private rentUrl = `https://localhost:7261/api/Lent/`;

  constructor(private http: HttpClient) {}

  getlentrecByuserid(id:number): Observable<any> {
    return this.http.get<any>(this.rentUrl + `lent-records-id?Userid=${id}`);
  }
  getallrentrecods():Observable<any> {
    return this.http.get<any>(this.rentUrl + `all-lent-All-records`);
  }
  rentnormalbookbycopyid(bcopyid:number,userid:number,adminid:number,duedays:number){
    const url = `${this.rentUrl}lend-by-copy-id`;
  const params = new HttpParams()
    .set('BookCopyId', bcopyid.toString())
    .set('UserId', userid.toString())
    .set('AdminId', adminid.toString())
    .set('DueDays', duedays.toString());

  return this.http.post(url, null, { params });
  }

  getrenthistory(currentPage:number,pageSize:number):Observable<any> {
      return this.http.get<any>(this.rentUrl + `lent-historys?page=${currentPage}&pageSize=${pageSize}`);
  }
}
