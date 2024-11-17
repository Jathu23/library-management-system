import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BookService } from '../../../services/bookservice/book.service';
import { AddBookDto } from '../../../models/interfaces/add-newbook.interface';

@Component({
  selector: 'app-addbook',
  templateUrl: './addbook.component.html',
  styleUrl: './addbook.component.css'
})
export class AddbookComponent {
[x: string]: any;
  addBookForm: FormGroup;
  genresList: string[] = ['Fiction', 'Non-Fiction', 'Science', 'Biography', 'Mystery', 'Fantasy'];
  selectedFiles: File[] = [];

  constructor(private fb: FormBuilder, private bookService: BookService) {
    this.addBookForm = this.fb.group({
      ISBN: ['', [Validators.required, Validators.pattern(/^[0-9-]+$/)]], // Allow numbers and hyphens
      Title: ['', Validators.required],
      Author: ['', Validators.required],
      Genre: [[], Validators.required],
      PublishYear: [
        '',
        [Validators.required, Validators.min(1000), Validators.max(new Date().getFullYear())],
      ],
      ShelfLocation: ['', Validators.required],
      TotalCopies: ['', [Validators.required, Validators.min(1)]],
      CoverImages: [null],
    });
  }

  /**
   * Handles the file input changes and stores the selected files.
   */
  onFileSelect(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      this.selectedFiles = Array.from(input.files);
      this.addBookForm.patchValue({ CoverImages: this.selectedFiles });
    }
  }

  /**
   * Submits the form data to the API using the BookService.
   */
  onSubmit(): void {
    if (this.addBookForm.valid) {
      const formData: AddBookDto = this.addBookForm.value;
      formData.CoverImages = this.selectedFiles;

      this.bookService.addBook(formData).subscribe({
        next: (response) => {
          alert('Book added successfully!');
          console.log(response);
          this.addBookForm.reset(); // Reset the form on success
          this.selectedFiles = [];
        },
        error: (error) => {
          alert('Failed to add book. Please try again.');
          console.error(error);
        },
      });
    } else {
      alert('Please fill in all required fields correctly.');
    }
  }
}
