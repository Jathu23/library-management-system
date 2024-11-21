import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { adminRequestModel } from '../../../models/interfaces/add-admin.interface';
import { AuthService } from '../../../services/auth-service/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-new-admin',
  templateUrl: './new-admin.component.html',
  styleUrl: './new-admin.component.css'
})
export class NewAdminComponent {
  adminForm: FormGroup;

  constructor(private fb: FormBuilder,
     private authService: AuthService,
    private router :Router)
     {
   

    this.adminForm = this.fb.group(
      {
      nic:new FormControl ('',[ Validators.required]),
      firstName: new FormControl ('',[ Validators.required]),
      lastName: new FormControl ('',[ Validators.required]),
      email: new FormControl ('',[ Validators.required]),
      password:new FormControl('', [Validators.required, Validators.minLength(6)]),
    });
  }

  onSubmit(): void {
    if (this.adminForm.invalid) {
      return;}
    
      const adminData = {
        ...this.adminForm.value
      };
console.log(adminData);

      this.authService.createAdmin(adminData).subscribe(
        (response) => {
          console.log('Admin created successfully:', response);
          alert('Admin created successfully!');
          this.adminForm.reset(); 
        },
        (error) => {
          console.log('Error creating admin:', error);
          alert('An error occurred while creating the admin.');
        },
      );
    }
  }

