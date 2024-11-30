import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {MainBookUpdateService} from '../../../services/bookservice/main-book-update.service'


@Component({
  selector: 'app-edit-book-dialog',
  templateUrl: './edit-book-dialog.component.html',
  styleUrl: './edit-book-dialog.component.css'
})
export class EditBookDialogComponent {
  editForm: FormGroup;
  message: string=''

  constructor(
    private bookUpdateServices: MainBookUpdateService,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EditBookDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { book: any }) 
    {
      this.editForm = this.fb.group({
      Id: [data.book.id],
      ISBN: [data.book.isbn, Validators.required],
      Title: [data.book.title, Validators.required],
      Author: [data.book.author, Validators.required],
      Genre: [data.book.genre, Validators.required],
      PublishYear: [data.book.publishYear, Validators.required],
      ShelfLocation: [data.book.shelfLocation],
      TotalCopies: [data.book.totalCopies, Validators.required],
      AvailableCopies: [data.book.availableCopies, Validators.required],
    });
  }

  save(): void {

    this.bookUpdateServices.updateBook(this.editForm.value).subscribe(
      (response) => {
        alert("'Book updated successfully'")
        console.log('Book updated successfully', response);
        this.message = 'Book updated successfully';
        this.dialogRef.close(this.editForm.value);
      },
      (error) => {
        console.error('Error updating book', error);
        this.message = 'Error updating book';
      }
    );

  }

  cancel(): void {
    this.dialogRef.close();
  }

}
