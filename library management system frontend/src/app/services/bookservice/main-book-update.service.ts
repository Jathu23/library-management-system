import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.testing';

@Injectable({
  providedIn: 'root'
})
export class MainBookUpdateService {
  private baseurl = environment.apiBaseUrl;

  // }

  // private mainBookUpdateUrl = `${this.baseurl}/Books/Update`;

  constructor(private http: HttpClient) {}


  private apiUrl = `${this.baseurl}/Books/Update`;

  updateBook(book: any): Observable<any> {
    const headers = new HttpHeaders({'Content-Type': 'application/json'});

    return this.http.put<any>(`${this.apiUrl}?Id=${book.Id}&ISBN=${book.ISBN}&Title=${book.Title}&Author=${book.Author}&Genre=${book.Genre}&PublishYear=${book.PublishYear}&ShelfLocation=${book.ShelfLocation}`, book, { headers });
  }

}


