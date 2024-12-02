import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import {MainBookUpdateService} from '../../../services/bookservice/main-book-update.service'

@Component({
  selector: 'app-edit-ebook-dialog',
  templateUrl: './edit-ebook-dialog.component.html',
  styleUrls: ['./edit-ebook-dialog.component.css'],
})
export class EditEbookDialogComponent {
  constructor(
    private EbookUpdate:MainBookUpdateService,
    public dialogRef: MatDialogRef<EditEbookDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public ebook: any
  ) {

    
  }

 
  saveChanges(): void {
    this.dialogRef.close(this.ebook); // Pass updated ebook back to parent
  }

  cancel(): void {
    this.dialogRef.close(); // Close without saving
  }
}
