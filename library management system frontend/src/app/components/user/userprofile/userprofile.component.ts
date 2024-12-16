import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../services/user-service/user.service';
import { DatePipe } from '@angular/common';
import { environment } from '../../../../environments/environment.testing';

@Component({
  selector: 'app-userprofile',
  templateUrl: './userprofile.component.html',
  styleUrls: ['./userprofile.component.css']
})
export class UserprofileComponent implements OnInit {
  user: any = {};
  isEditing: boolean = false;
  profileImage: File | null = null;  
userbaseurl =environment.resourcBaseUrl
 
  readonlyFields = ['id', 'isActive', 'isSubscribed', 'registrationDate','email'];


  userFields = [
    { label: 'First Name', key: 'firstName' },
    { label: 'Last Name', key: 'lastName' },
    { label: 'Email', key: 'email' },
    { label: 'Phone', key: 'phoneNumber' },
    { label: 'Address', key: 'address' },
    { label: 'User NIC', key: 'userNic' }
  ];

  recentActivities: string[] = [
    'Updated profile information',
    'Added a new book review',
    'Joined the Science Fiction group',
    'Completed the JavaScript tutorial'
  ];

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.fetchUserDetails();
  }

  fetchUserDetails() {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const tokenData = this.decodeToken(token);
        const userIdentifier = tokenData.UserNic || tokenData.Email;
        if (userIdentifier) {
          this.userService.GetUserByEmailorNic(userIdentifier).subscribe(
            (response) => {
              this.user = response?.data || {};
              console.log('Fetched User:', this.user);
            },
            (error) => console.error('Error fetching user:', error)
          );
        }
      } catch (error) {
        console.error('Error decoding token:', error);
      }
    }
  }

  decodeToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }

  toggleEdit() {
    this.isEditing = true;
  }

  
  saveUser(form: any) {
    if (form.valid) {
      const updatedUser = { ...this.user };
  
      if (this.profileImage) {
        updatedUser.profileImage = this.profileImage; 
      } else {
        delete updatedUser.profileImage;  
      }
  
      
      this.userService.updateUser(updatedUser).subscribe(
        (response) => {
          alert('User details updated successfully!');
          console.log('User updated:', response);
          this.isEditing = false;
        },
        (error) => {
          console.error('Error updating user:', error);
        }
      );
    } else {
      alert('Please fill out the form correctly.');
    }
  }
  
  
  cancelEdit() {
    this.fetchUserDetails();
    this.isEditing = false;
  }

  isFieldReadonly(fieldKey: string): boolean {
    return this.readonlyFields.includes(fieldKey) || !this.isEditing;
  }

  onImageSelected(event: any): void {
    const file = event.target.files[0]; 
    if (file) {
      this.profileImage = file;
      
      const reader = new FileReader();
      reader.onload = () => {
        this.user.profileImage = reader.result as string;  
      };
      reader.readAsDataURL(file);  
    } else {
      this.profileImage = null; 
    }
  }
  
}
