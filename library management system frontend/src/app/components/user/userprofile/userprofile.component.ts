import { Component, OnInit } from '@angular/core';
import { jwtDecode } from 'jwt-decode'; // Corrected import

@Component({
  selector: 'app-userprofile',
  templateUrl: './userprofile.component.html',
  styleUrls: ['./userprofile.component.css']
})
export class UserprofileComponent implements OnInit {
  tokenData: any = null;
  user: any = {};

  ngOnInit(): void {
    this.getTokenData();
  }

  getTokenData() {
    const token = localStorage.getItem('token'); // Assuming token is saved with the key 'token'

    if (token) {
      try {
        this.tokenData = jwtDecode(token); // Decode the token
        console.log(this.tokenData); // You can inspect the decoded token here
        this.setUserProfile();
      } catch (error) {
        console.error('Error decoding the token:', error);
      }
    } else {
      console.log('No token found in localStorage');
    }
  }

  setUserProfile() {
    if (this.tokenData) {
      // Set the user object based on decoded token
      this.user = {
        fullName: this.tokenData.FullName || 'John Doe', // Use the correct field names from token
        email: this.tokenData.Email || 'No email available',
        phone: this.tokenData.Phone || 'No phone available',
        UserNic: this.tokenData.UserNic || 'No UserNic available',
        address: this.tokenData.Address || 'No address available',
        profileImage: this.tokenData.ProfileImage || 'https://bootdey.com/img/Content/avatar/avatar7.png', // Default image
        aud: this.tokenData.aud || 'Full Stack Developer', // Default job title
        location: this.tokenData.Location || 'Bay Area, San Francisco, CA' // Default location
      };

      // Log the user profile to the console for debugging
      console.log(this.user);
    }
  }
}
