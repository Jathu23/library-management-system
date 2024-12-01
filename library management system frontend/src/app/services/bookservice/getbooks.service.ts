import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.testing';

@Injectable({
  providedIn: 'root'
})
export class GetbooksService {
  private baseurl = environment.apiBaseUrl;
  constructor(private http: HttpClient) {}

private ebookUrl = `${this.baseurl}/Ebook/`;

private audiobookUrl = `${this.baseurl}/Audiobook/`;

private NormalBookUrl = `${this.baseurl}/Books/`

private UserNormalBookUrl =`${this.baseurl}/Books/get-all-books`;

showBookstoUser(currentPage:number,pageSize:number):Observable<any>{
  return this.http.get<any>(this.UserNormalBookUrl+`?page=${currentPage}&pageSize=${pageSize}`);
}

getaudiobooks(currentPage:number,pageSize:number): Observable<any> {
  return this.http.get<any>(this.audiobookUrl + `GetAudiobooks?page=${currentPage}&pageSize=${pageSize}`);
}

searchAudiobooks(searchString: string,currentPage: number,pageSize: number): Observable<any> {
  return this.http.get<any>( this.audiobookUrl +`Search?searchString=${searchString}&page=${currentPage}&pageSize=${pageSize}`);
}


getebooks(currentPage: number, pageSize: number): Observable<any> {
  return this.http.get<any>(this.ebookUrl + `GetEbooks?page=${currentPage}&pageSize=${pageSize}`);
}

getNoramlbooks(currentPage: number, pageSize: number): Observable<any> {
  return this.http.get<any>(this.NormalBookUrl + `get-all-books-with-copies/?page=${currentPage}&pageSize=${pageSize}`);
}
getNoramlbookbyId(bookid: number): Observable<any> {
  return this.http.get<any>(this.NormalBookUrl + `get-book?bookId=${bookid}`);
}

markPdfAsRead(ebookId: number): Observable<any> {
  return this.http.post<any>(`${this.ebookUrl}MarkAsRead`, { ebookId });
}

}
