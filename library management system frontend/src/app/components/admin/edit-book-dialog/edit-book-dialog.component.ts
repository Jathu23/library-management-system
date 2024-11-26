import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';


@Component({
  selector: 'app-edit-book-dialog',
  templateUrl: './edit-book-dialog.component.html',
  styleUrl: './edit-book-dialog.component.css'
})
export class EditBookDialogComponent {
  editForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EditBookDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { book: any }
  ) {
    this.editForm = this.fb.group({
      id: [data.book.id],
      isbn: [data.book.isbn, Validators.required],
      title: [data.book.title, Validators.required],
      author: [data.book.author, Validators.required],
      genre: [data.book.genre, Validators.required],
      publishYear: [data.book.publishYear, Validators.required],
      shelfLocation: [data.book.shelfLocation],
      totalCopies: [data.book.totalCopies, Validators.required],
      availableCopies: [data.book.availableCopies, Validators.required],
    });
  }

  save(): void {
    if (this.editForm.valid) {
      this.dialogRef.close(this.editForm.value);
    }
  }

  cancel(): void {
    this.dialogRef.close();
  }

}
