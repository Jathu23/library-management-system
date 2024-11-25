import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookDeleteServicesService {

  private normalBookDeleteUrl = `https://localhost:7261/api/Books`;


  constructor(private http: HttpClient) { }

  deleteItem(id: number): Observable<void> {
    return this.http.delete<void>(`${this.normalBookDeleteUrl}/delete-copy?copyId=${id}`);

  }

}
