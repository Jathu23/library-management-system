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
  isEditing: boolean = false; // Flag to toggle edit mode

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.getTokenData();
  }

  getTokenData() {
    const token = localStorage.getItem('token'); // Assuming token is saved with the key 'token'


    if (token) {
      try {
        this.tokenData = jwtDecode(token); // Decode the token
        this.setUserProfile();
        console.log(this.tokenData);
        
      } catch (error) {
        console.error('Error decoding the token:', error);
      }
    } else {
      console.log('No token found in localStorage');
    }
  }

  setUserProfile() {
    const userIdentifier = this.tokenData.UserNic || this.tokenData.Email;

    if (userIdentifier) {
      this.userService.GetUserByEmailorNic(userIdentifier).subscribe(
        (userData) => {
          const data = userData.data;
          console.log(data);
          
          this.user = {
            id: data.id,
            fullName: data.fullName || 'John Doe',
            firstName: data.firstName || 'John Doe',
            lastName: data.lastName || 'John Doe',
            email: data.email || 'No email available',
            phone: data.phoneNumber || 'No phone available',
            UserNic: data.userNic || 'No UserNic available',
            address: data.address || 'No address available',
            profileImage: data.profileImage ? `https://localhost:7261/${data.profileImage}` : 'https://bootdey.com/img/Content/avatar/avatar7.png',
            aud: this.tokenData.aud || 'Full Stack Developer',
            registrationDate: data.registrationDate || 'Not available'
          };
        },
        (error) => {
          console.error('Error fetching user data:', error);
        }
      );
    } else {
      console.log('No valid UserNic or Email found to fetch user data');
    }
  }

  toggleEdit() {
    this.isEditing = !this.isEditing;
  }

  saveUser() {
    const updatedUser = {
      Id: this.user.id,
      UserNic: this.user.UserNic,
      FirstName: this.user.firstName,
      LastName: this.user.lastName,
      Email: this.user.email,
      PhoneNumber: this.user.phone,
      Address: this.user.address,
    };
    console.log(updatedUser);
    

    this.userService.UpdateUser(updatedUser).subscribe(
      (response) => {
        alert("updated successfully")
        console.log('User data updated successfully:', response);
        this.isEditing = false; // Toggle back to view mode after saving
        this.closeEditModal(); // Close the modal after saving
      },
      (error) => {
        console.error('Error updating user data:', error);
      }
    );
  }

  // Method to open the edit profile modal
  openEditModal() {
    const modal = document.getElementById('editProfileModal') as HTMLElement;
    if (modal) {
      modal.style.display = 'block';
    }
  }

  // Method to close the edit profile modal
  closeEditModal() {
    const modal = document.getElementById('editProfileModal') as HTMLElement;
    if (modal) {
      modal.style.display = 'none';
    }
  }
}
