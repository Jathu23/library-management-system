import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.testing';


@Component({
  selector: 'app-user-layout',
  templateUrl: './user-layout.component.html',
  styleUrls: ['./user-layout.component.css']
})
export class UserLayoutComponent implements OnInit {
  isCollapsed = false;
  menuStates: { [key: string]: boolean } = {};
  userData: any = {}; // Store the fetched user data
  profileImage: string = ''; // Store the profile image URL
  userName: string = ''; // Store the user's first name
 resourcBaseUrl=environment.resourcBaseUrl
  constructor(private router: Router, private http: HttpClient) {}

  ngOnInit(): void {
    this.loadUserData();
  }

  // Toggle the sidebar visibility
  toggleSidebar() {
    this.isCollapsed = !this.isCollapsed;
  }

  // Toggle menu visibility
  toggleMenu(menuKey: string) {
    this.menuStates[menuKey] = !this.menuStates[menuKey];
  }

  // Check if menu is expanded
  isMenuExpanded(menuKey: string): boolean {
    return this.menuStates[menuKey] || false;
  }

  // Logout function to remove the token and navigate to login page
  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  // Fetch user data from the API
  loadUserData() {
    const tokenData = environment.getTokenData();
    if (tokenData && tokenData.ID) {
      const userId = Number(tokenData.ID);

      // Fetch user data using the ID
      environment.fetchUserDataById(this.http, userId).then((userData: any) => {
        if (userData) {
          this.userData = userData.data;
          console.log(userData.data.id);
          
          // Assuming the user data has a field 'firstName' and 'profileImage'
          this.userName = userData.data.firstName || 'Username';
          this.profileImage = `${userData.data.profileImage}` || 'default-profile.jpg'; // Use a default image if none provided
        } else {
          console.error('Failed to fetch user data');
        }
      }).catch(error => {
        console.error('Error fetching user data:', error);
      });
    } else {
      console.warn('User ID not found in token data');
    }
  }
}
