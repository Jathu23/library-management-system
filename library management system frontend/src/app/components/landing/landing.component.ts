import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private router: Router) {}

  onLogin(userType?: string) {
    // Hardcoded login credentials
    const adminCredentials = { username: '1', password: '1' };
    const userCredentials = { username: '2', password: '2' };

    if (userType === 'admin') {
      if (this.username === adminCredentials.username && this.password === adminCredentials.password) {
        this.router.navigate(['/admin']);
      } else {
        this.errorMessage = 'Invalid admin credentials. Please try again.';
      }
    } else if (userType === 'user') {
      if (this.username === userCredentials.username && this.password === userCredentials.password) {
        this.router.navigate(['/user']);
      } else {
        this.errorMessage = 'Invalid user credentials. Please try again.';
      }
    }
  }
}
