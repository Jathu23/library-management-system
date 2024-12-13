import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.testing';

@Injectable({
  providedIn: 'root'
})
export class BookDeleteServicesService {

  private baseUrl = environment.apiBaseUrl;

  private normalBookDeleteUrl = `${this.baseUrl}/Books`;
  private normalMainBookDeleteUrl = `${this.baseUrl}/Books`;
  private deleteEBookUrl = `${this.baseUrl}/Ebook/DeleteEbook?id`
  private deleteAudiobookUrl = `${this.baseUrl}/Audiobook/delete`;



  constructor(private http: HttpClient) { }

  deleteItem(id: number): Observable<void> {
    return this.http.delete<void>(`${this.normalBookDeleteUrl}/delete-copy?copyId=${id}`);

  }

  deleteMainBook(id: number): Observable<void> {
    return this.http.delete<void>(`${this.normalMainBookDeleteUrl}/delete-book?bookId=${id}`);

  }
  deleteEBook(id: number): Observable<void> {

    return this.http.delete<void>(`${this.deleteEBookUrl}=${id}`);

  }

  deleteAudioBook(id: number): Observable<void> {
    return this.http.delete<void>(`${this.deleteAudiobookUrl}?id=${id}`);
  }

  
  

}
