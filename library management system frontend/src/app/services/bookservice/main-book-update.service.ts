import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.testing';

@Injectable({
  providedIn: 'root'
})
export class MainBookUpdateService {
  private baseurl = environment.apiBaseUrl;
  private apiBaseUrl = 'https://localhost:7261/api/Ebook'

  // }

  // private mainBookUpdateUrl = `${this.baseurl}/Books/Update`;

  // https://localhost:7261/api/Ebook/update?Id=4&Title=d&Author=asd&Genre=asd&PublishYear=2010&Publisher=asd&Language=asd&Description=asd&DigitalRights=asd

  constructor(private http: HttpClient) {}


  private apiUrl = `${this.baseurl}/Books/Update`;

  updateBook(book: any): Observable<any> {
    const headers = new HttpHeaders({'Content-Type': 'application/json'});

    return this.http.put<any>(`${this.apiUrl}?Id=${book.Id}&ISBN=${book.ISBN}&Title=${book.Title}&Author=${book.Author}&Genre=${book.Genre}&PublishYear=${book.PublishYear}&ShelfLocation=${book.ShelfLocation}`, book, { headers });
  }

  // updateEbook(ebook: any): Observable<any> {
  //   const params = new URLSearchParams({
  //     Id: ebook.id.toString(),
  //     Title: ebook.title,
  //     Author: ebook.author,
  //     Genre: ebook.genre,
  //     PublishYear: ebook.publishYear.toString(),
  //     Publisher: ebook.metadata.publisher,
  //     Language: ebook.metadata.language,
  //     Description: ebook.metadata.description,
  //     DigitalRights: ebook.metadata.digitalRights,
  //   });
  //   return this.http.put(`${this.apiBaseUrl}/update?${params.toString()}`, null);
  // }

  updateEbook(ebook: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<any>(
      `${this.apiBaseUrl}/update?Id=${ebook.id}&Title=${ebook.title}&Author=${ebook.author}&Genre=${ebook.genre}&PublishYear=${ebook.publishYear}&Publisher=${ebook.metadata.publisher}&Language=${ebook.metadata.language}&Description=${ebook.metadata.description}&DigitalRights=${ebook.metadata.digitalRights}`,
      ebook,
      { headers }
    );

  }
}




