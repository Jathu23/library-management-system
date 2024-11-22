import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-show-normalbook',
  templateUrl: './show-normalbook.component.html',
  styleUrl: './show-normalbook.component.css'
})
export class ShowNormalbookComponent implements OnInit {

  data:any[]=[]
  datas:any;

  booksCopie:any;
  booksCopies:any[]=[]
  
  baseUrl:string =  `https://localhost:7261/api/Books/get-all-books-with-copies?page=1&pageSize=20`;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData(): void {
    const apiUrl = this.baseUrl; // Replace with your API URL

    this.http.get(apiUrl).subscribe({
      next: (response) => {
        this.datas = (response)

        this.booksCopie=this.datas.data

        this.datas.data.forEach((element: any) => {
          let sample:any={
            "id": element.id,
            "isbn": element.isbn,
            "title": element.title,
            "author": element.author,
            "toggle": 0,
            "genre": [
              element.genre[0],
              element.genre[1]
            ],
            "publishYear": element.publishYear,
            "shelfLocation": element.shelfLocation,
            "availableCopies": element.availableCopies,
            "totalCopies": element.totalCopies,
            "coverImagePath": [
              element.coverImagePath
            ],
            "bookCopies":element.bookCopies

          }
          this.data.push(sample)
          
        });    
        console.log(this.data);
      },
      error: (err) => {
        console.error('Error fetching data:', err);
  
        
      },
    });
  }



}
