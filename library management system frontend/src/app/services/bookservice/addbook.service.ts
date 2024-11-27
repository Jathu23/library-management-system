import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AddBookDto } from '../../models/interfaces/add-newbook.interface';
import { AddEbookDto } from '../../models/interfaces/add-newebook.interface';
import { AddAudiobookDto } from '../../models/interfaces/add-newaudiobook.interface';


@Injectable({
  providedIn: 'root',
})
export class BookService {
  private readonly apiUrl = 'https://localhost:7261/api/Books/add';
  private ebookUrl = 'https://localhost:7261/api/Ebook/add'; 
  private audiobookUrl = 'https://localhost:7261/api/Audiobook/add-audiobook'; 

  constructor(private http: HttpClient) {}

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
      Array.from(book.CoverImages).forEach((file: File, index: number) => {
        formData.append(`CoverImages[${index}]`, file, file.name);
      });
    }
    // if (book.CoverImages && book.CoverImages.length > 0) {
    //   Array.from(book.CoverImages).forEach((file: File, index: number) => {
    //     formData.append(`CoverImages[${index}]`, file, file.name);
    //   });
    // }
    

    
    return this.http.post(this.apiUrl, formData, {
      headers: new HttpHeaders({
        // Optionally, add additional headers if required
        // 'Authorization': `Bearer ${yourAuthToken}`
      }),
    });
  }

  
  addEbook(ebook: AddEbookDto): Observable<any> {
    const formData = new FormData();

    formData.append('ISBN', ebook.ISBN);
    formData.append('Title', ebook.Title);
    formData.append('Author', ebook.Author);
    formData.append('Genre', ebook.Genre);
    formData.append('PublishYear', ebook.PublishYear.toString());

    // Append EbookFile
    if (ebook.EbookFile instanceof File) {
      formData.append('EbookFile', ebook.EbookFile, ebook.EbookFile.name);
    } else {
      console.error('EbookFile is not a valid File object.');
    }

    // Append CoverImages (optional)
    if (ebook.CoverImages instanceof File) {
      formData.append('CoverImages', ebook.CoverImages, ebook.CoverImages.name);
    }

    // Append metadata fields
    if (ebook.Metadata) {
      for (const [key, value] of Object.entries(ebook.Metadata)) {
        formData.append(`Metadata.${key}`, value);
      }
    }

    return this.http.post<any>(this.ebookUrl, formData);
  }
  


  addAudiobook(audiobook: AddAudiobookDto): Observable<any> {
    const formData = new FormData();

    // Append form data fields
    formData.append('ISBN', audiobook.ISBN);
    formData.append('Title', audiobook.Title);
    formData.append('Author', audiobook.Author);
    formData.append('Genre', audiobook.Genre);
    formData.append('PublishYear', audiobook.PublishYear.toString());
    formData.append('FileFormat', audiobook.FileFormat);
    formData.append('Language', audiobook.Language);
    formData.append('Narrator', audiobook.Narrator);
    formData.append('Publisher', audiobook.Publisher);
    formData.append('Description', audiobook.Description);
    formData.append('DigitalRights', audiobook.DigitalRights);

    // Append files
    // if (audiobook.AudioFile instanceof File) {
    //   formData.append('AudioFile', audiobook.AudioFile, audiobook.AudioFile.name);
    // } else {
    //   console.error('AudioFile is not a valid File object');
    // }


    if (audiobook.AudioFile) {
      formData.append('AudioFile', audiobook.AudioFile, audiobook.AudioFile.name);
    
      
    }else{
      console.log("file is not aviable");
    }
    

    if (audiobook.CoverImage) {
      formData.append('CoverImage', audiobook.CoverImage, audiobook.CoverImage.name);
    } else {
      console.error('CoverImage is not a valid File object');
    }
    

    // Send POST request
    return this.http.post<any>(this.audiobookUrl, formData);
  }

}
