import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.testing';
import { tap } from 'rxjs/operators'; // Importing tap operator

@Injectable({
  providedIn: 'root'
})
export class GetbooksService {
  private baseurl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  private ebookUrl = `${this.baseurl}/Ebook/`;
  private audiobookUrl = `${this.baseurl}/Audiobook/`;
  private NormalBookUrl = `${this.baseurl}/Books/`;
  private UserNormalBookUrl = `${this.baseurl}/Books/get-all-books`;


  Categorize (genre: string, author: string, publishYear: number, currentPage: number, pageSize: number): Observable<any> {
    const url = `${this.NormalBookUrl}Categorize?genre=${genre}&author=${author}&publishYear=${publishYear}&pageNumber=${currentPage}&pageSize=${pageSize}`;
    return this.http.get<any>(url);
  }
  

  // Log API call for showing books to users
  showBookstoUser(currentPage: number, pageSize: number): Observable<any> {
    const url = `${this.UserNormalBookUrl}?page=${currentPage}&pageSize=${pageSize}`;
    console.log(`[API Request] GET: ${url}`);
    return this.http.get<any>(url).pipe(
      tap(
        (response) => console.log(`[API Response] GET: ${url}`, response),
        (error) => console.error(`[API Error] GET: ${url}`, error)
      )
    );
  }

  // Log API call for fetching audiobooks
  getaudiobooks(currentPage: number, pageSize: number): Observable<any> {
    const url = `${this.audiobookUrl}GetAudiobooks?page=${currentPage}&pageSize=${pageSize}`;
    console.log(`[API Request] GET: ${url}`);
    return this.http.get<any>(url).pipe(
      tap(
        (response) => console.log(`[API Response] GET: ${url}`, response),
        (error) => console.error(`[API Error] GET: ${url}`, error)
      )
    );
  }

  // Log API call for searching audiobooks
  searchAudiobooks(searchString: string, currentPage: number, pageSize: number): Observable<any> {
    const url = `${this.audiobookUrl}Search?searchString=${searchString}&page=${currentPage}&pageSize=${pageSize}`;
    console.log(`[API Request] GET: ${url}`);
    return this.http.get<any>(url).pipe(
      tap(
        (response) => console.log(`[API Response] GET: ${url}`, response),
        (error) => console.error(`[API Error] GET: ${url}`, error)
      )
    );
  }
  searchNormalBooks(searchString: string, currentPage: number, pageSize: number): Observable<any> {
    const url = `${this.NormalBookUrl}Search?searchstring=${searchString}&pageNumber=${currentPage}&pageSize=${pageSize}`;
    console.log(`[API Request] GET: ${url}`);
    return this.http.get<any>(url).pipe(
      tap(
        (response) => console.log(`[API Response] GET: ${url}`, response),
        (error) => console.error(`[API Error] GET: ${url}`, error)
      )
    );
  }
  
  // Log API call for fetching ebooks
  getebooks(currentPage: number, pageSize: number): Observable<any> {
    const url = `${this.ebookUrl}GetEbooks?page=${currentPage}&pageSize=${pageSize}`;
    console.log(`[API Request] GET: ${url}`);
    return this.http.get<any>(url).pipe(
      tap(
        (response) => console.log(`[API Response] GET: ${url}`, response),
        (error) => console.error(`[API Error] GET: ${url}`, error)
      )
    );
  }

  // Log API call for fetching normal books
  getNoramlbooks(currentPage: number, pageSize: number): Observable<any> {
    const url = `${this.NormalBookUrl}get-all-books-with-copies/?page=${currentPage}&pageSize=${pageSize}`;
    console.log(`[API Request] GET: ${url}`);
    return this.http.get<any>(url).pipe(
      tap(
        (response) => console.log(`[API Response] GET: ${url}`, response),
        (error) => console.error(`[API Error] GET: ${url}`, error)
      )
    );
  }

  // Log API call for fetching normal book by ID
  getNoramlbookbyId(bookid: number): Observable<any> {
    const url = `${this.NormalBookUrl}get-book?bookId=${bookid}`;
    console.log(`[API Request] GET: ${url}`);
    return this.http.get<any>(url).pipe(
      tap(
        (response) => console.log(`[API Response] GET: ${url}`, response),
        (error) => console.error(`[API Error] GET: ${url}`, error)
      )
    );
  }

  // Log API call for marking a PDF as read
  markPdfAsRead(ebookId: number): Observable<any> {
    const url = `${this.ebookUrl}MarkAsRead`;
    console.log(`[API Request] POST: ${url}`, { ebookId });
    return this.http.post<any>(url, { ebookId }).pipe(
      tap(
        (response) => console.log(`[API Response] POST: ${url}`, response),
        (error) => console.error(`[API Error] POST: ${url}`, error)
      )
    );
  }

  searchEbooks(searchQuery: string, pageNumber: number, pageSize: number): Observable<any> {
    const searchUrl = `${this.baseurl}Ebook/Search?searchString=${searchQuery}&pageNumber=${pageNumber}&pageSize=${pageSize}`;
    return this.http.get<any>(searchUrl).pipe(
      tap(
        (response) => console.log(`[API Response] GET: ${searchUrl}`, response),
        (error) => console.error(`[API Error] GET: ${searchUrl}`, error)
      )
    );
  }
  
}
