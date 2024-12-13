import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MainBookUpdateService } from '../../../../services/bookservice/main-book-update.service';

@Component({
  selector: 'app-edit-audiobook-dialog',
  templateUrl: './edit-audiobook-dialog.component.html',
  styleUrls: ['./edit-audiobook-dialog.component.css'],
})
export class EditAudiobookDialogComponent {
  constructor(
    private audiobookUpdate: MainBookUpdateService,
    public dialogRef: MatDialogRef<EditAudiobookDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public audiobook: any 
  ) {}

  saveChanges(): void {
    this.dialogRef.close(this.audiobook);  
  }
  

  cancel(): void {
    this.dialogRef.close();
  }
}
