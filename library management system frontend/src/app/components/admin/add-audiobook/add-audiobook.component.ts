import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BookService } from '../../../services/bookservice/book.service';
import { AddAudiobookDto } from '../../../models/interfaces/add-newaudiobook.interface';

@Component({
  selector: 'app-add-audiobook',
  templateUrl: './add-audiobook.component.html',
  styleUrl: './add-audiobook.component.css'
})
export class AddAudiobookComponent {
  addAudiobookForm: FormGroup;

  constructor(private fb: FormBuilder,private bookService: BookService) {
    this.addAudiobookForm = this.fb.group({
      ISBN: ['', Validators.required],
      Title: ['', Validators.required],
      Author: ['', Validators.required],
      Genre: ['', Validators.required],
      PublishYear: [null, [Validators.required, Validators.min(1900)]],
      AudioFile: [null, Validators.required],
      CoverImage: [null],
      FileFormat: ['', Validators.required],
      Language: ['', Validators.required],
      Narrator: ['', Validators.required],
      Publisher: ['', Validators.required],
      Description: ['', Validators.required],
      DigitalRights: ['', Validators.required],
    });
  }

  onSubmit(): void {

    
    if (this.addAudiobookForm.valid) {
      const formValue = this.addAudiobookForm.value;
      const audiobookDto: AddAudiobookDto = {
        ...formValue,
        AudioFile: formValue.AudioFile[0],
        CoverImage: formValue.CoverImage ? formValue.CoverImage[0] : null,
      };
console.log(audiobookDto);

      // this.bookService.addAudiobook(audiobookDto).subscribe({
      //   next: (response) => {
      //     console.log('Audiobook added successfully', response);
      //   },
      //   error: (error) => {
      //     console.error('Error adding audiobook', error);
      //   },
      // });
    }
  }

  onFileSelect(event: Event, fieldName: string): void {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      this.addAudiobookForm.patchValue({
        [fieldName]: file,
      });
    }
  }
}
