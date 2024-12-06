import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.testing';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {
  private baseurl = environment.apiBaseUrl;
  private apiUrl = `${this.baseurl}/Subscription`; 

  constructor(private http: HttpClient) {}

  getCurrentSubscription(): Observable<any> {
    return this.http.get(`${this.apiUrl}/current`);
  }

  getSubscriptionHistory(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/history`);
  }

  getPlans(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/plans`);
  }

  getDurations(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/durations`);
  }

  subscribe(planId: number, durationId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/subscribe`, { planId, durationId });
  }
}
