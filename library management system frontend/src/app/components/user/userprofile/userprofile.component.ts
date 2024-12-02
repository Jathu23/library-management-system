import { Component, OnInit } from '@angular/core';
import { jwtDecode } from 'jwt-decode'; // Corrected import
import { UserService } from '../../../services/user-service/user.service';

@Component({
  selector: 'app-userprofile',
  templateUrl: './userprofile.component.html',
  styleUrls: ['./userprofile.component.css']
})
export class UserprofileComponent implements OnInit {
  tokenData: any = null;
  user: any = {};

  constructor(private userService: UserService) {}

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
    // Fetch user details by UserNic or Email
    const userIdentifier = this.tokenData.UserNic || this.tokenData.Email;

    if (userIdentifier) {
      this.userService.GetUserByEmailorNic(userIdentifier).subscribe(
        (userData) => {
          console.log('Fetched user data:', userData); // Log the fetched data
          const data = userData.data;
          console.log(data);
          console.log(data.profileImage);
          

          // Update the user object with the fetched data
          this.user = {
            fullName: data.fullName || 'John Doe', // Use the fetched full name
            firstName: data.firstName || 'John Doe',
            lastName: data.lastName || 'John Doe',
            email: data.email || 'No email available',
            phone: data.phoneNumber || 'No phone available', // Use phoneNumber from fetched data
            UserNic: data.userNic || 'No UserNic available', // Use userNic from fetched data
            address: data.address || 'No address available', // Use address from fetched data
            profileImage: data.profileImage ? `https://localhost:7261/${data.profileImage}` : 'https://bootdey.com/img/Content/avatar/avatar7.png',

            aud: this.tokenData.aud || 'Full Stack Developer', // Use the aud field from the token
            registrationDate: data.registrationDate || 'Not available' // Use registrationDate from fetched data
          };

          // Log the updated user profile to the console for debugging
          console.log(this.user);
        },
        (error) => {
          console.error('Error fetching user data:', error);
        }
      );
    } else {
      console.log('No valid UserNic or Email found to fetch user data');
    }
  }
}
