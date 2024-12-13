import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.testing';

@Injectable({
  providedIn: 'root'
})
export class MainBookUpdateService {
  private baseurl = environment.apiBaseUrl;
  private apiBaseUrl = `${this.baseurl}/Ebook`;
  private apiUrl = `${this.baseurl}/Books/Update`;

  private apiaudiourl = `${this.baseurl}/Audiobook`;



  constructor(private http: HttpClient) {}



  updateBook(book: any): Observable<any> {
    const headers = new HttpHeaders({'Content-Type': 'application/json'});

    return this.http.put<any>(`${this.apiUrl}?Id=${book.Id}&ISBN=${book.ISBN}&Title=${book.Title}&Author=${book.Author}&Genre=${book.Genre}&PublishYear=${book.PublishYear}&ShelfLocation=${book.ShelfLocation}`, book, { headers });
  }

  updateEbook(ebook: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<any>(
      `${this.apiBaseUrl}/update?Id=${ebook.id}&Title=${ebook.title}&Author=${ebook.author}&Genre=${ebook.genre}&PublishYear=${ebook.publishYear}&Publisher=${ebook.metadata.publisher}&Language=${ebook.metadata.language}&Description=${ebook.metadata.description}&DigitalRights=${ebook.metadata.digitalRights}`,
      ebook,
      { headers }
    );

  }


  updateAudiobook(audiobook: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<any>(
      `${this.apiaudiourl}/update-audiobook?Id=${audiobook.id}&Title=${audiobook.title}&Author=${audiobook.author}&Genre=${audiobook.genre}&PublishYear=${audiobook.publishYear}&Publisher=${audiobook.publisher}&Language=${audiobook.language}&Description=${audiobook.description}&DigitalRights=${audiobook.digitalRights}`,
      audiobook,
      { headers }
    );
  }
  
}




