import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.testing';

@Injectable({
  providedIn: 'root'
})
export class StaticsService {
  private base = environment.apiBaseUrl;
  private baseUrl = `${this.base}/Chart`; // Replace with your actual API base URL

  constructor(private http: HttpClient) {}

  // Fetch borrowing trends for the current year
  getBorrowingTrends(): Observable<any> {
    return this.http.get(`${this.baseUrl}/borrowing-trends`);
  }

  // Fetch borrowing trends for all years
  getBorrowingTrendsForAllYears(): Observable<any> {
    return this.http.get(`${this.baseUrl}/all-borrowing-trends`);
  }
}
