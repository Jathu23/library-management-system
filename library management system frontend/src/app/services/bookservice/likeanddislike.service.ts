import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment.testing';

export interface LikeDislikeRequest {
  bookId: number;
  userId: number;
  isLiked: boolean;
}

export interface LikeDislikeResponse<T> {
  success: boolean;
  message: string;
  data: T;
  errors: string[];
}

@Injectable({
  providedIn: 'root',
})
export class LikeanddislikeService {
  private base = environment.apiBaseUrl;
  private baseUrl = `${this.base}/LikeDislike`;
  private ebookUrl = `${this.base}/Ebook/`;
  private audiobookUrl = `${this.base}/Audiobook/`;

  constructor(private http: HttpClient) {}

  // Add Like/Dislike for Normal Book
  addNormalBookLikeDislike(request: LikeDislikeRequest): Observable<LikeDislikeResponse<any>> {
    const url = `${this.baseUrl}/normalbook`;
    const params = new HttpParams()
      .set('bookId', request.bookId.toString())
      .set('userId', request.userId.toString())
      .set('isLiked', request.isLiked.toString());

    return this.http.post<LikeDislikeResponse<any>>(url, null, { params }).pipe(
      catchError(this.handleError)
    );
  }

  // Get Normal Book Like/Dislike Count
  getNormalBookLikeDislikeCount(bookId: number, isLiked: boolean): Observable<LikeDislikeResponse<any>> {
    const url = `${this.baseUrl}/normalbook-count-bookId`;
    const params = new HttpParams()
      .set('bookId', bookId.toString())
      .set('isLiked', isLiked.toString());

    return this.http.get<LikeDislikeResponse<any>>(url, { params }).pipe(
      catchError(this.handleError)
    );
  }

  // Add Like/Dislike for Ebook
  addEbookLikeDislike(request: LikeDislikeRequest): Observable<LikeDislikeResponse<any>> {
    const url = `${this.baseUrl}/ebook`;
    const params = new HttpParams()
      .set('bookId', request.bookId.toString())
      .set('userId', request.userId.toString())
      .set('isLiked', request.isLiked.toString());

    return this.http.post<LikeDislikeResponse<any>>(url, null, { params }).pipe(
      catchError(this.handleError)
    );
  }

  // Get Ebook Like/Dislike Count
  getEbookLikeDislikeCount(bookId: number, isLiked: boolean): Observable<LikeDislikeResponse<any>> {
    const url = `${this.baseUrl}/ebook-count-bookId`;
    const params = new HttpParams()
      .set('bookId', bookId.toString())
      .set('isLiked', isLiked.toString());

    return this.http.get<LikeDislikeResponse<any>>(url, { params }).pipe(
      catchError(this.handleError)
    );
  }

  // Add Like/Dislike for Audiobook
  addAudiobookLikeDislike(request: LikeDislikeRequest): Observable<LikeDislikeResponse<any>> {
    const url = `${this.baseUrl}/audiobook`;
    const params = new HttpParams()
      .set('bookId', request.bookId.toString())
      .set('userId', request.userId.toString())
      .set('isLiked', request.isLiked.toString());

    return this.http.post<LikeDislikeResponse<any>>(url, null, { params }).pipe(
      catchError(this.handleError)
    );
  }

  // Get Audiobook Like/Dislike Count
  getAudiobookLikeDislikeCount(bookId: number, isLiked: boolean): Observable<LikeDislikeResponse<any>> {
    const url = `${this.baseUrl}/audiobook-count-bookId`;
    const params = new HttpParams()
      .set('bookId', bookId.toString())
      .set('isLiked', isLiked.toString());

    return this.http.get<LikeDislikeResponse<any>>(url, { params }).pipe(
      catchError(this.handleError)
    );
  }

  addAudioBookClick(bookId: number): Observable<any> {
    const url = `${this.audiobookUrl}AddClick`;
    const params = new HttpParams().set('bookid', bookId.toString());
  
    return this.http.post<any>(url, null, { params }).pipe(
      catchError(this.handleError)
    );
  }
  addEBookClick(bookId: number): Observable<any> {
    const url = `${this.ebookUrl}AddClick`;
    const params = new HttpParams().set('bookid', bookId.toString());
  
    return this.http.post<any>(url, null, { params }).pipe(
      catchError(this.handleError)
    );
  }
    


  // Handle HTTP errors
  private handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'An unknown error occurred!';
    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = `Client error: ${error.error.message}`;
    } else {
      // Server-side error
      errorMessage = `Server error: ${error.status}, Message: ${error.message}`;
    }
    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}
