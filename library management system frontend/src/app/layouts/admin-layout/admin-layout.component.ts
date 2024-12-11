import { Component, HostListener } from '@angular/core';
import { environment } from '../../../environments/environment.testing';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { LockScreenComponent } from '../../components/lock-screen/lock-screen.component';
// import * as jwt_decode from 'jwt-decode';  // Correct import if default import fails

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.css']
})
export class AdminLayoutComponent {
  decodedToken: any;
  tokenExpired: boolean = false;
  isExpanded = true;
  private timeout: any; // Timeout for inactivity
  private isLocked = false; // Track if the screen is already locked
  private inactivityDuration = 6000; // 2 minutes in milliseconds

  constructor( private router:Router,private dialog: MatDialog) {
    const token = localStorage.getItem("token");
    const tokendata = environment.decodeTokenManually(token);
    console.log(tokendata);

    // this.resetTimer();
    
  }
// Listen to user activity events to reset the timer
@HostListener('document:mousemove')
@HostListener('document:keydown')
@HostListener('document:click')
@HostListener('document:scroll')
// onUserActivity() {
//   this.resetTimer();
// }

ngOnInit(): void {
  // this.checkLockStatus();
}

  // Reset the inactivity timer
  private resetTimer() {
    clearTimeout(this.timeout);

    // Start the inactivity timer again
    if (!this.isLocked) {
      this.timeout = setTimeout(() => this.lockScreen(), this.inactivityDuration);
    }
  }
  checkLockStatus() {
    const isLocked = localStorage.getItem('isLocked');
    if (isLocked === 'true') {
      this.lockScreen();
    } else {
      this.resetTimer();
    }
  }


 // Open the lock screen dialog
 private lockScreen() {
  if (!this.isLocked) {
    this.isLocked = true; // Set lock status to true
    const dialogRef = this.dialog.open(LockScreenComponent, {
      disableClose: true, // Prevent closing without PIN
      panelClass: 'full-screen-dialog',
    });

    // When the dialog is closed (after unlocking), reset lock status
    dialogRef.afterClosed().subscribe(() => {
      this.isLocked = false; // Reset lock status
      this.resetTimer(); // Restart the inactivity timer
    });
  }
}
  


  
  // Toggle sidebar
  toggleSidebar() {
    this.isExpanded = !this.isExpanded;
    
  }

  logout() {
    localStorage.removeItem('token'); // Remove the token from localStorage
    this.router.navigate(['/login']); // Redirect to the login page
  }
  
}


