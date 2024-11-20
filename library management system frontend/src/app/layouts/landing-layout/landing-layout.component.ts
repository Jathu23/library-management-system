import { Component } from '@angular/core';
import { OnInit } from '@angular/core';

import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-landing-layout',
  templateUrl: './landing-layout.component.html',
  styleUrl: './landing-layout.component.css'
})
export class LandingLayoutComponent  implements OnInit {
  signupForm: FormGroup;

  constructor(private fb: FormBuilder) {
    // Initialize the signup form
    this.signupForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required],
      address: ['', Validators.required],
      profileImage: [''],
      registrationDate: [new Date(), Validators.required] // Auto-filled with the current date and time
    });
  }

  ngOnInit(): void {
    // This will automatically fill the registration date when the component is initialized
    this.signupForm.patchValue({
      registrationDate: new Date().toISOString()
    });
  }

  // Form submission handler
  onSubmit(): void {
    if (this.signupForm.valid) {
      const formData = this.signupForm.value;
      console.log(formData); // Here you would send the form data to your API
    }
  }
}