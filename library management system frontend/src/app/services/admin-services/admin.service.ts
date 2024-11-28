import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private readonly adminurl ='https://localhost:7261/api/Admin/';
  constructor(private http: HttpClient) { }

  getAllAdmins(): Observable<any> {
    return this.http.get<any[]>(this.adminurl+'GetAllAdmins');
  }
  
  addAdmin(adminformdata: any): Observable<any> {
    const formData = new FormData();

    // Append form data fields
    formData.append('AdminNic', adminformdata.AdminNic);
    formData.append('FirstName', adminformdata.FirstName);
    formData.append('LastName', adminformdata.LastName);
    formData.append('Email', adminformdata.Email);
    formData.append('Password', adminformdata.Password);
   
    return this.http.post(this.adminurl+'CreateAdmin', formData);
  }
  
  transferMasterControl(currentMaster: number, newMaster: number): Observable<any> {
    const url = `${this.adminurl}transfer-master-control?CurrentMasterId=${currentMaster}&NewMasterId=${newMaster}`;
    return this.http.post(url, null); 
  }
  
  
  
}
