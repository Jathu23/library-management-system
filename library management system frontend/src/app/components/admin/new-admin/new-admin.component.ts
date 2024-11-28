import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AdminService } from '../../../services/admin-services/admin.service';

@Component({
  selector: 'app-new-admin',
  templateUrl: './new-admin.component.html',
  styleUrl: './new-admin.component.css'
})
export class NewAdminComponent {
  adminForm!: FormGroup;
  adminsList: any[] = [];
  selectedAdminId: number | null = 0;
  curentAdminId: number= 2;

  constructor(private fb: FormBuilder, private adminService: AdminService) {}

  ngOnInit(): void {
    this.adminForm = this.fb.group({
      nic: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });

    this.fetchAdmins();
  }

  fetchAdmins(): void {
    this.adminService.getAllAdmins().subscribe(
      (data) => {
        if (data.success) {
          // console.log(data.data);
          this.adminsList=data.data;
          console.log(this.adminsList);
        }
      
    },
  (error)=>{
    console.log(error);
  });
  }

  onSubmit(): void {
    if (this.adminForm.valid) {
      const newAdmin = this.adminForm.value;
      this.adminService.addAdmin(newAdmin).subscribe({
        next: () => {
          alert('Admin added successfully!');
          this.adminForm.reset();
        },
        error: (err) => {
          console.error(err);
          alert('Failed to add admin.');
        },
      });
    }
  }

  transferMasterControl(): void {
    if (this.selectedAdminId) {
      this.adminService.transferMasterControl(this.curentAdminId,this.selectedAdminId).subscribe({
        next: () => {
          alert('Master control transferred successfully!');
          this.fetchAdmins();
          this.selectedAdminId=0;
        },
        error: (err) => {
          console.error(err);
          alert('Failed to transfer master control.');
          this.selectedAdminId=0;
        },
      });
    } else {
      alert('Please select an admin.');
      
    }
  }
}

