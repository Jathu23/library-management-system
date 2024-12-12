import { Component, HostListener, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment.testing';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { LockScreenComponent } from '../../components/lock-screen/lock-screen.component';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.css']
})
export class AdminLayoutComponent implements OnInit {
  decodedToken: any;
  tokenExpired: boolean = false;
  isExpanded = true;
  private timeout: any; // Timeout for inactivity
  private isLocked = false; // Track if the screen is already locked
  private inactivityDuration = 900000; // 2 minutes in milliseconds

  constructor(private router: Router, private dialog: MatDialog) {
    // Get token from localStorage and decode it
    const token = localStorage.getItem("token");
    if (token) {
      this.decodedToken = environment.decodeTokenManually(token);
    }
    this.resetTimer(); // Initialize inactivity timer
  }

  ngOnInit(): void {
    this.checkLockStatus(); // Check if screen was locked during the previous session
  }

  // Listen to user activity events to reset the timer
  @HostListener('document:mousemove')
  @HostListener('document:keydown')
  @HostListener('document:click')
  @HostListener('document:scroll')
  onUserActivity() {
    this.resetTimer();
  }

  // Reset the inactivity timer
  private resetTimer() {
    if (!this.isLocked) {
      clearTimeout(this.timeout);
      // Start the inactivity timer again
      this.timeout = setTimeout(() => this.lockScreen(), this.inactivityDuration);
    }
  }

  // Check if the screen was locked
  private checkLockStatus() {
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
      localStorage.setItem('isLocked', "true");

      const dialogRef = this.dialog.open(LockScreenComponent, {
        disableClose: true, // Prevent closing without PIN
        panelClass: 'full-screen-dialog'
      });

      // When the dialog is closed (after unlocking), reset lock status
      dialogRef.afterClosed().subscribe(() => {
        this.isLocked = false; // Reset lock status
        localStorage.setItem('isLocked', "false");
        this.resetTimer(); // Restart the inactivity timer
      });
    }
  }

  // Toggle sidebar
  toggleSidebar() {
    this.isExpanded = !this.isExpanded;
  }

  // Logout and remove token from localStorage
  logout() {
    localStorage.removeItem('token'); // Remove the token from localStorage
    localStorage.removeItem('isLocked'); // Optionally clear lock status
    this.router.navigate(['/login']); // Redirect to the login page
  }
}
