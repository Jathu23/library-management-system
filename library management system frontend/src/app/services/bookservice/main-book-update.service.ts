import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.testing';

@Injectable({
  providedIn: 'root'
})
export class MainBookUpdateService {
  private baseurl = environment.apiBaseUrl;

  // private mainBookUpdateUrl = `https://localhost:7261/api/Books/Update`;

  // constructor(private http: HttpClient) { }

  // updateBook(id: number, book: any): Observable<any> {
  //   const params = new HttpParams()
  //     .set('Id', id.toString())
  //     .set('ISBN', book.ISBN)
  //     .set('Title', book.Title)
  //     .set('Author', book.Author)
  //     .set('Genre', book.Genre)
  //     .set('PublishYear', book.PublishYear)
  //     .set('ShelfLocation', book.ShelfLocation)
  //     .set('TotalCopies', book.TotalCopies);

  //   return this.http.put(this.mainBookUpdateUrl, null, { params });
  // }

  private mainBookUpdateUrl = `${this.baseurl}/Books/Update`;

  constructor(private http: HttpClient) {}

  updateBook(book: any): Observable<any> {
    // Send the entire book object in the request body
    return this.http.put(this.mainBookUpdateUrl, book);
  }
}
