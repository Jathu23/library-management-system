import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-lock-screen',
  templateUrl: './lock-screen.component.html',
  styleUrls: ['./lock-screen.component.css'],
})
export class LockScreenComponent implements OnInit {
  pinForm: FormGroup;
  time: string = '';
  date: string = '';

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<LockScreenComponent>
  ) {
    this.pinForm = this.fb.group({
      pin: ['', [Validators.required, Validators.minLength(4)]],
    });
  }

  ngOnInit(): void {
    this.updateDateTime();
    setInterval(() => this.updateDateTime(), 1000); // Update every second
  }

  updateDateTime(): void {
    const now = new Date();
    this.time = now.toLocaleTimeString('en-US', {
      hour: 'numeric',
      minute: '2-digit',
      second: '2-digit',
      hour12: true,
    });
    this.date = now.toLocaleDateString('en-US', {
      weekday: 'long',
      month: 'long',
      day: 'numeric',
    });
  }

  unlock() {
    const enteredPin = this.pinForm.get('pin')?.value;
    const correctPin = '1234'; // Replace with actual pin logic.
    if (enteredPin === correctPin) {
      this.dialogRef.close();
    } else {
      alert('Invalid PIN');
    }
  }
}
