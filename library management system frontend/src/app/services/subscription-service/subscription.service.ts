import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.testing';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {
  private baseurl = environment.apiBaseUrl;
  private apiUrl = `${this.baseurl}/Subscription`; 

  constructor(private http: HttpClient) {}

  getCurrentSubscriptionbyuser(userId:number): Observable<any> {
    return this.http.get(`${this.apiUrl}/active?userId=${userId}`);
  }

  getSubscriptionHistorybyuser(userId:number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/history?userId=${userId}`);
  }

  getPlans(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/plans`);
  }

  getDurations(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/durations`);
  }

  subscribeplan(userId: number, planId: number, durationId: number, method: string): Observable<any> {
    const params = new HttpParams()
      .set('UserId', userId.toString())
      .set('SubscriptionPlanId', planId.toString())
      .set('PaymentDurationId', durationId.toString())
      .set('PayMethod', method);
  
    return this.http.post<any>(`${this.apiUrl}/subscribe`, null, { params });
  }
  
}
