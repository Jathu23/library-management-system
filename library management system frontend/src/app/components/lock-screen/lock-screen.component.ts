import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-lock-screen',
  templateUrl: './lock-screen.component.html',
  styleUrls: ['./lock-screen.component.css'],
})
export class LockScreenComponent {
  pinForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<LockScreenComponent>
  ) {
    this.pinForm = this.fb.group({
      pin: ['', [Validators.required, Validators.minLength(4)]],
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
