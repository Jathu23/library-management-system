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
  private timeoutId: any;
  isExpanded = false;

  constructor( private router:Router,private dialog: MatDialog) {
    const token = localStorage.getItem("token");
    
    const tokendata = environment.decodeTokenManually(token);
    console.log(tokendata);
    
  }

  ngOnInit(): void {
    // this.checkLockStatus();
  }

  // @HostListener('document:mousemove')
  // @HostListener('document:keydown')
  // resetTimeout() {
  //   clearTimeout(this.timeoutId);
  //   this.startInactivityTimer();
  // }

  startInactivityTimer() {
    this.timeoutId = setTimeout(() => {
      localStorage.setItem('isLocked', 'true');
      this.openLockScreen();
    }, 0.2 * 60 * 1000); // 2 minutes
  }

  checkLockStatus() {
    const isLocked = localStorage.getItem('isLocked');
    if (isLocked === 'true') {
      this.openLockScreen();
    } else {
      this.startInactivityTimer();
    }
  }



  openLockScreen() {
    const dialogRef = this.dialog.open(LockScreenComponent, {
      disableClose: true, // Prevent closing without entering PIN
      panelClass: 'full-screen-dialog', // Ensure full-screen
    });
  
    // dialogRef.afterClosed().subscribe(() => {
    //   localStorage.setItem('isLocked', 'false');
    //   this.startInactivityTimer();
    // });
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


