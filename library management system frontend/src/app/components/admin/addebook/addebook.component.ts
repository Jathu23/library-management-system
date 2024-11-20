import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AddEbookDto } from '../../../models/interfaces/add-newebook.interface';
import { BookService } from '../../../services/bookservice/book.service';

@Component({
  selector: 'app-addebook',
  templateUrl: './addebook.component.html',
  styleUrl: './addebook.component.css'
})
export class AddebookComponent implements OnInit {
  addEbookForm!: FormGroup;
  imagePreviews: string[] = [];

  constructor(private fb: FormBuilder , private bookService: BookService) {}

  ngOnInit(): void {
    this.addEbookForm = this.fb.group({
      ISBN: ['', Validators.required],
      Title: ['', Validators.required],
      Author: ['', Validators.required],
      Genre: ['', Validators.required],
      PublishYear: ['', [Validators.required, Validators.min(1000)]],
      EbookFile: ['', Validators.required],
      CoverImages: ['']
    });
  }

  onFileSelect(event: any, field: string): void {
    const files = event.target.files;
    if (field === 'CoverImages') {
      this.imagePreviews = Array.from(files).map((file: any) =>
        URL.createObjectURL(file)
      );
    }
    this.addEbookForm.get(field)?.setValue(files);
  }

  onSubmit(): void {
    if (this.addEbookForm.valid) {
      const formData: AddEbookDto = this.addEbookForm.value;
      console.log('Form submitted:', formData);
      this.bookService.addEbook(formData).subscribe({
        next: (response) => {
          console.log(response);
          this.addEbookForm.reset(); 
          this.imagePreviews = [];
          alert('Book added successfully!');
        },
        error: (error) => {
          alert('Failed to add book. Please try again.');
          console.log(error);
        },
      });

    }
  }
}