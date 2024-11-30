import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Add a custom header (e.g., Authorization)
     const token = localStorage.getItem('token'); // Retrieve your JWT token
    // const token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJGdWxsTmFtZSI6IkVzdmFyYW4gSmF0aHVzaGFuIiwiRW1haWwiOiJqYXRodXNoYW5AZ21haWwuY29tIiwiQWRtaW5OaWMiOiIxIiwiZXhwIjoxNzMyODk0ODk1LCJpc3MiOiJsaWJyYXJ5LW1hbmFnZW1lbnQiLCJhdWQiOiJ1c2VycyJ9.VlXT6iODTBxcwyuRZH8BV4Y0IMHTjn5asK-NwL_U4v8';
    if (token) {
      const clonedRequest = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        },
      });
      return next.handle(clonedRequest);
    }

    // If no token, pass the original request
    return next.handle(req);
  }
}
