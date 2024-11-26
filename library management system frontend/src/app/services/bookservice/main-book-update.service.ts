import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MainBookUpdateService {
  http: any;

  // private mainBookUpdateUrl =  `https://localhost:7261/api/Books/Update?Id=5&ISBN=333333&Title=3333Just&Author=Denojan&Genre=string3&Genre=string3&Genre=string&PublishYear=2010&ShelfLocation=d1&TotalCopies=10`
  private mainBookUpdateUrl =  `https://localhost:7261/api/Books/Update?Id`



  constructor() { }
  
  updateBook(id: number, book: any): Observable<any> {
    return this.http.put(`${this.mainBookUpdateUrl}=${id}`, book);
  }
}
