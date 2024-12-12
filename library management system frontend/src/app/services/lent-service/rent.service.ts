import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../../../environments/environment.testing';

@Injectable({
  providedIn: 'root'
})
export class RentService {
  private baseurl = environment.apiBaseUrl;
  
  private rentUrl = `${this.baseurl}/Lent/`;
  private returnUrl = `${this.baseurl}/Return/`;
  

  constructor(private http: HttpClient) {}

  getlentrecByuserid(id:number): Observable<any> {
    return this.http.get<any>(this.rentUrl + `get-records-by-userid-admin?Userid=${id}`);
  }
  getallrentrecods(getoverdue: boolean = false): Observable<any> {
    return this.http.get<any>(`${this.rentUrl}get-all-records-admin?getoverdue=${getoverdue}`);
  }
  
  rentnormalbookbycopyid(bcopyid:number,userid:number,adminid:number,duedays:number){
    const url = `${this.rentUrl}lend-by-book-copyid`;
  const params = new HttpParams()
    .set('BookCopyId', bcopyid.toString())
    .set('UserId', userid.toString())
    .set('AdminId', adminid.toString())
    .set('DueDays', duedays.toString());

  return this.http.post(url, null, { params });
  }

  getrenthistory(currentPage:number,pageSize:number):Observable<any> {
      return this.http.get<any>(this.rentUrl + `get-all-historys-admin?page=${currentPage}&pageSize=${pageSize}`);
  }

  returnNormalbook(lentId:number,adminId:number){
     
    const url = `${this.returnUrl}return-lent-book`;
  const params = new HttpParams()
    
    .set('lentRecordId', lentId.toString())
    .set('ResiveAdminId', adminId.toString())
  return this.http.post(url, null, { params });
  }

  getUserLentRecords(userId: number, getoverdue: boolean = false): Observable<any> {
    // Append the getoverdue query parameter to the URL
    return this.http.get<any>(`${this.rentUrl}lent-records-by-userid-user?userId=${userId}&getoverdue=${getoverdue}`);
  }
  

  getUserLendingHistory(userId: number): Observable<any> {
    
    return this.http.get<any>(this.rentUrl+ `rent-history-by-userid?userId=${userId}`);
  }

  getLentReport(date: string | null): Observable<any> {
    const url = date != null ? `${this.rentUrl}Lent-Report?date=${date}` : `${this.rentUrl}Lent-Report`;
    return this.http.get<any>(url);
  }
  getLentReportPdfAll(date: Date | null): Observable<Blob> {
    // Construct the URL based on whether date is provided or not
    const url = date != null 
      ? `${this.rentUrl}Lent-Report-pdf?date=${date.toISOString()}`  // If date is provided, include it in the URL
      : `${this.rentUrl}Lent-Report-pdf`;  // If date is null, do not include date parameter
  
    // Send the GET request with the constructed URL and the response type set to 'blob' for the PDF file
    return this.http.get(url, { responseType: 'blob' });
  }
  
  getLentReportByUserId(userId: number | null): Observable<any> {
    const url = userId != null ? `${this.rentUrl}Lent-Report-ByUserid?userid=${userId}` : `${this.rentUrl}Lent-Report-ByUserid`;
    return this.http.get<any>(url);
  }

  getLentReportPdf(userid: number | null): Observable<Blob> {
    // Construct the URL based on the userid (if available)
    const url = userid != null 
      ? `${this.rentUrl}Lent-Report-ByUserid-pdf?userid=${userid}` 
      : `${this.rentUrl}Lent-Report-ByUserid-pdf`;
  
    // Make the HTTP GET request and return the response as a Blob (for PDF file)
    return this.http.get(url, { responseType: 'blob' });
  }
  
  
  getBookLendingReport(bookId: number | null): Observable<any> {
    const url = bookId != null ? `${this.rentUrl}Book-lending-report?bookId=${bookId}` : `${this.rentUrl}Book-lending-report`;
    return this.http.get<any>(url);
  }
  
  getLendingCountReport(bookId: number | null): Observable<any> {
    const url = bookId != null ? `${this.rentUrl}LendingCount-report?bookId=${bookId}` : `${this.rentUrl}LendingCount-report`;
    return this.http.get<any>(url);
  }
  
        
}
