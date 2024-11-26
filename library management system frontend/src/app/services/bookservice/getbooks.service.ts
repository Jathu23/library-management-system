import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GetbooksService {

  constructor(private http: HttpClient) {}

private ebookUrl = `https://localhost:7261/api/Ebook/GetEbooks`;

private audiobookUrl = `https://localhost:7261/api/Audiobook/`;

private NormalBookUrl = `https://localhost:7261/api/Books/get-all-books-with-copies`


getaudiobooks(currentPage:number,pageSize:number): Observable<any> {
  return this.http.get<any>(this.audiobookUrl + `GetAudiobooks?page=${currentPage}&pageSize=${pageSize}`);
}

searchAudiobooks(searchString: string,currentPage: number,pageSize: number): Observable<any> {
  return this.http.get<any>( this.audiobookUrl +`Search?searchString=${searchString}&page=${currentPage}&pageSize=${pageSize}`);
}


getebooks(currentPage: number, pageSize: number): Observable<any> {
  return this.http.get<any>(this.ebookUrl + `?page=${currentPage}&pageSize=${pageSize}`);
}

getNoramlbooks(currentPage: number, pageSize: number): Observable<any> {
  return this.http.get<any>(this.NormalBookUrl + `?page=${currentPage}&pageSize=${pageSize}`);
}




}
