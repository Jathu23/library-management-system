import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AudiobookService {
  private audioBookGetUrl = 'http://localhost:5149/api/Audiobook/GetAudiobooks?page=1&pageSize=32';


  private apiUrl = 'https://localhost:7261/api/Audiobook/top';
  constructor(private http: HttpClient) {}

  getAudiobooks(): Observable<any> {
    return this.http.get(this.audioBookGetUrl);
  }

  getTopAudiobooks(count: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/${count}`);
  }

  private mainapiUrl = 'https://localhost:7261/api/Audiobook'; // API Base URL



  getAudiobookCount(): Observable<{ count: number }> {
    return this.http.get<{ count: number }>(`${this.mainapiUrl}/audiobooks/count`);
  }
}
