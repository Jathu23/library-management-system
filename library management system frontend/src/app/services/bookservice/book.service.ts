import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AddBookDto } from '../../models/interfaces/add-newbook.interface';


@Injectable({
  providedIn: 'root',
})
export class BookService {
  private readonly apiUrl = 'https://localhost:7261/api/Books/add';

  constructor(private http: HttpClient) {}

  /**
   * Add a new book to the library
   * @param book - The book details to add
   * @returns Observable of the API response
   */
  addBook(book: AddBookDto): Observable<any> {
    const formData = new FormData();

    // Append all fields to the FormData object
    formData.append('ISBN', book.ISBN);
    formData.append('Title', book.Title);
    formData.append('Author', book.Author);
    formData.append('PublishYear', book.PublishYear.toString());
    formData.append('ShelfLocation', book.ShelfLocation);
    formData.append('TotalCopies', book.TotalCopies.toString());

    // Append each genre as an individual form field
    book.Genre.forEach((genre, index) => {
      formData.append(`Genre[${index}]`, genre);
    });

    // Append cover images (if any)
    if (book.CoverImages) {
      book.CoverImages.forEach((file, index) => {
        formData.append(`CoverImages[${index}]`, file, file.name);
      });
    }

    // Send a POST request with the FormData
    return this.http.post(this.apiUrl, formData, {
      headers: new HttpHeaders({
        // Optionally, add additional headers if required
        // 'Authorization': `Bearer ${yourAuthToken}`
      }),
    });
  }
}
