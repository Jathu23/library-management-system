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
  curentAdminId: number= 1005;

  constructor(private fb: FormBuilder, private adminService: AdminService) {}

  ngOnInit(): void {
    this.adminForm = this.fb.group({
      AdminNic: ['', Validators.required],
      FirstName: ['', Validators.required],
      LastName: ['', Validators.required],
      Email: ['', [Validators.required, Validators.email]],
      Password: ['', [Validators.required, Validators.minLength(6)]],
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
        next: (res) => {
          alert(res.message);
          this.adminForm.reset();
        },
        error: (err) => {
          console.error(err);
          alert(err.error.errors[0]);
        },
      });
    }
  }

  transferMasterControl(): void {
    if (this.selectedAdminId) {
      this.adminService.transferMasterControl(this.curentAdminId,this.selectedAdminId).subscribe({
        next: (res) => {
          alert(res.message);
          this.selectedAdminId=0;
        },
        error: (err) => {
          console.error(err);
          alert(err.error.errors[0]);
          alert('Failed to transfer master control.');
          this.selectedAdminId=0;
        },
      });
    } else {
      alert('Please select an admin.');
      
    }
  }
}

