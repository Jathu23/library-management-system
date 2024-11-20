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
  addAudiobookForm!: FormGroup;

  constructor(private fb: FormBuilder,private bookService: BookService) {
    this.addAudiobookForm = this.fb.group({
      ISBN: ['fw', Validators.required],
      Title: ['fwf', Validators.required],
      Author: ['fdwf', Validators.required],
      Genre: ['ddf', Validators.required],
      PublishYear: ['111', Validators.required],
      AudioFile: ['', Validators.required],
      CoverImage: [''],
      FileFormat: ['mp3', Validators.required],
      Language: ['eng', Validators.required],
      Narrator: ['ds', Validators.required],
      Publisher: ['ew', Validators.required],
      Description: ['df', Validators.required],
      DigitalRights: ['ds'],
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
      console.log(formValue);
      
console.log(audiobookDto);

      this.bookService.addAudiobook(audiobookDto).subscribe({
        next: (response) => {
          console.log('Audiobook added successfully', response);
        },
        error: (error) => {
          console.error('Error adding audiobook', error);
        },
      });
    }else{
      console.log("formValue invalid ");
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
