import { Component } from '@angular/core';
import { environment } from '../../../environments/environment.testing';
import { Router } from '@angular/router';
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

  constructor( private router:Router) {
    const token = localStorage.getItem("token");
    
    const tokendata = environment.decodeTokenManually(token);
    console.log(tokendata);
    
  }

  // Toggle sidebar
  toggleSidebar() {
    this.isExpanded = !this.isExpanded;
  }

 

  logout() {
    localStorage.removeItem('token'); // Remove the token from localStorage
    this.router.navigate(['/login']); // Redirect to the login page
  }
  
}


