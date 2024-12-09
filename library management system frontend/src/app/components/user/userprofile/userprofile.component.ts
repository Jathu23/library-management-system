import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../services/user-service/user.service';
import { environment } from '../../../../environments/environment.testing';

@Component({
  selector: 'app-userprofile',
  templateUrl: './userprofile.component.html',
  styleUrls: ['./userprofile.component.css']
})
export class UserprofileComponent implements OnInit {
  currentUser: any ;
  isEditing: boolean = false;
  currentUserEmail: string = '';

  constructor(private userService: UserService) {
    const tokendata = environment.getTokenData();
    this.currentUserEmail = tokendata.Email;
  }

  ngOnInit(): void {
    this.getUserInfo();
  }

  getUserInfo() {
    if (this.currentUserEmail) {
      this.userService.GetUserByEmailorNic(this.currentUserEmail).subscribe(
        (response) => {
          this.currentUser = response.data;
          console.log(this.currentUser);
          
        },
        (error) => {
          console.error('Error fetching user data:', error);
        }
      );
    } else {
      console.log('No valid email found to fetch user data');
    }
  }

  toggleEdit() {
    this.isEditing = !this.isEditing;
  }

  saveUser() {
    const formData = new FormData();
    formData.append('Id', this.currentUser.id);
    formData.append('UserNic', this.currentUser.userNic);
    formData.append('FirstName', this.currentUser.firstName);
    formData.append('LastName', this.currentUser.lastName);
    formData.append('Email', this.currentUser.email);
    formData.append('PhoneNumber', this.currentUser.phoneNumber);
    formData.append('Address', this.currentUser.address);
  
    // Append the profile image if there is one
    // if (this.currentUser.profileImage) {
    //   formData.append('ProfileImage', this.currentUser.profileImage, this.currentUser.profileImage.name);
    // }
  
    this.userService.updateUser(formData).subscribe(
      (response) => {
        alert('User updated successfully');
        this.isEditing = false; // Exit edit mode after successful update
      },
      (error) => {
        console.error('Error updating user data:', error);
      }
    );
  }

  onFileChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.currentUser.profileImage = file;
    }
  }
  
  
}
