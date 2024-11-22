import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AudiobookService {
  private audioBookGetUrl = 'http://localhost:5149/api/Audiobook/GetAudiobooks?page=1&pageSize=32';

  constructor(private http: HttpClient) {}

  getAudiobooks(): Observable<any> {
    return this.http.get(this.audioBookGetUrl);
  }
}
