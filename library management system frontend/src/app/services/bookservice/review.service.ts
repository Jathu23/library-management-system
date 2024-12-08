import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment.testing';

export interface ReviewRequest {
  userId: number;
  bookId: number;
  reviewText: string;
  rating: number;
}

export interface ReviewResponse<T> {
  success: boolean;
  message: string;
  data: T;
  errors: string[];
}

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  private base = environment.apiBaseUrl;
  private baseUrl = `${this.base}/Review`;

  constructor(private http: HttpClient) {}

  // Add Normal Book Review
  addNormalBookReview(review: ReviewRequest): Observable<ReviewResponse<any>> {
    const url = `${this.baseUrl}/normal-book/review`;
    return this.http.post<ReviewResponse<any>>(url, null, { params: review as any }).pipe(
      catchError(this.handleError)
    );
  }

  // Get Reviews for Normal Book
  getNormalBookReviews(bookId: number): Observable<ReviewResponse<any>> {
    const url = `${this.baseUrl}/normal-book-reviews`;
    return this.http.get<ReviewResponse<any>>(url, { params: { bookId: bookId.toString() } }).pipe(
      catchError(this.handleError)
    );
  }

  // Add Ebook Review
  addEbookReview(review: ReviewRequest): Observable<ReviewResponse<any>> {
    const url = `${this.baseUrl}/ebook-review`;
    return this.http.post<ReviewResponse<any>>(url, null, { params: review as any }).pipe(
      catchError(this.handleError)
    );
  }

  // Get Reviews for Ebook
  getEbookReviews(bookId: number): Observable<ReviewResponse<any>> {
    const url = `${this.baseUrl}/ebook-reviews`;
    return this.http.get<ReviewResponse<any>>(url, { params: { bookId: bookId.toString() } }).pipe(
      catchError(this.handleError)
    );
  }

  // Add Audiobook Review
  addAudiobookReview(review: ReviewRequest): Observable<ReviewResponse<any>> {
    const url = `${this.baseUrl}/audiobook-review`;
    return this.http.post<ReviewResponse<any>>(url, null, { params: review as any }).pipe(
      catchError(this.handleError)
    );
  }

  // Get Reviews for Audiobook
  getAudiobookReviews(bookId: number): Observable<ReviewResponse<any>> {
    const url = `${this.baseUrl}/audiobook-reviews`;
    return this.http.get<ReviewResponse<any>>(url, { params: { bookId: bookId.toString() } }).pipe(
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
