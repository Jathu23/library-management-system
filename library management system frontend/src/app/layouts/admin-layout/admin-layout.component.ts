import { Component } from '@angular/core';
// import * as jwt_decode from 'jwt-decode';  // Correct import if default import fails

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.css']
})
export class AdminLayoutComponent {
  decodedToken: any;
  tokenExpired: boolean = false;

  isExpanded = false;

  constructor() {
    const token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJGdWxsTmFtZSI6IkVzdmFyYW4gSmF0aHVzaGFuIiwiRW1haWwiOiJqYXRodXNoYW5AZ21haWwuY29tIiwiQWRtaW5OaWMiOiIxIiwiZXhwIjoxNzMyODEyNTkyLCJpc3MiOiJsaWJyYXJ5LW1hbmFnZW1lbnQiLCJhdWQiOiJ1c2VycyJ9.zgLGxpO36JaMQr471kq6U0b_H1IYLXEnmP4tS4n9soU';
    // this.decodeJwtToken(token);
  }

  // Toggle sidebar
  toggleSidebar() {
    this.isExpanded = !this.isExpanded;
  }

  // Decode the JWT token
  // decodeJwtToken(token: string): void {
  //   try {
  //     this.decodedToken = jwt_decode(token);
  //     console.log('Decoded Token:', this.decodedToken);
      
  //     // Check if the token has expired
  //     const expirationDate = new Date(0);  // Convert from Unix timestamp to Date object
  //     expirationDate.setUTCSeconds(this.decodedToken.exp);  // Set expiration time from token
  //     const currentDate = new Date();

  //     if (currentDate > expirationDate) {
  //       this.tokenExpired = true;
  //       console.log('Token has expired.');
  //     } else {
  //       this.tokenExpired = false;
  //       console.log('Token is still valid.');
  //     }
      
  //   } catch (error) {
  //     console.error('Error decoding token:', error);
  //   }
  // }
}
