import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BookService } from '../../../services/bookservice/book.service';
import { AddBookDto } from '../../../models/interfaces/add-newbook.interface';

@Component({
  selector: 'app-addbook',
  templateUrl: './addbook.component.html',
  styleUrl: './addbook.component.css'
})

// export class AddbookComponent {
// [x: string]: any;
//   addBookForm: FormGroup;
//   genresList: string[] = ['Fiction', 'Non-Fiction', 'Science', 'Biography', 'Mystery', 'Fantasy'];
//   selectedFiles: File[] = [];

//   constructor(private fb: FormBuilder, private bookService: BookService) {
//     this.addBookForm = this.fb.group({
//       ISBN: ['', [Validators.required, Validators.pattern(/^[0-9-]+$/)]], // Allow numbers and hyphens
//       Title: ['', Validators.required],
//       Author: ['', Validators.required],
//       Genre: [[], Validators.required],
//       PublishYear: [
//         '',
//         [Validators.required, Validators.min(1000), Validators.max(new Date().getFullYear())],
//       ],
//       ShelfLocation: ['', Validators.required],
//       TotalCopies: ['', [Validators.required, Validators.min(1)]],
//       CoverImages: [null],
//     });
//   }

//   /**
//    * Handles the file input changes and stores the selected files.
//    */
//   onFileSelect(event: Event): void {
//     const input = event.target as HTMLInputElement;
//     if (input.files) {
//       this.selectedFiles = Array.from(input.files);
//       this.addBookForm.patchValue({ CoverImages: this.selectedFiles });
//     }
//   }

//   /**
//    * Submits the form data to the API using the BookService.
//    */
//   onSubmit(): void {
//     if (this.addBookForm.valid) {
//       const formData: AddBookDto = this.addBookForm.value;
//       formData.CoverImages = this.selectedFiles;

//       this.bookService.addBook(formData).subscribe({
//         next: (response) => {
//           alert('Book added successfully!');
//           console.log(response);
//           this.addBookForm.reset(); // Reset the form on success
//           this.selectedFiles = [];
//         },
//         error: (error) => {
//           alert('Failed to add book. Please try again.');
//           console.error(error);
//         },
//       });
//     } else {
//       alert('Please fill in all required fields correctly.');
//     }
//   }
// }




export class AddbookComponent {
  addBookForm: FormGroup;
  genresList: string[] = ['Fiction', 'Non-Fiction', 'Science', 'Biography', 'History'];
  imagePreviews: string[] = []; // Stores image previews

  constructor(private fb: FormBuilder, private bookService: BookService) {
    this.addBookForm = this.fb.group({
      ISBN: ['', [Validators.required, Validators.pattern(/^[0-9-]+$/)]], // Allow numbers and hyphens
      Title: ['', Validators.required],
      Author: ['', Validators.required],
      Genre: [[], Validators.required],
      PublishYear: ['', [Validators.required, Validators.pattern(/^\d{4}$/)]],
      ShelfLocation: ['', Validators.required],
      TotalCopies: ['', [Validators.required, Validators.min(1)]],
      CoverImages: [null]
    });
  }

  /**
   * Handles file input change event.
   * Reads the selected files and generates image previews.
   */
  onFileSelect(event: any): void {
    const files = event.target.files;
    this.imagePreviews = []; // Clear previous previews
    if (files) {
      Array.from(files).forEach((file: any) => {
        const reader = new FileReader();
        reader.onload = (e: any) => {
          this.imagePreviews.push(e.target.result); // Store preview as base64
        };
        reader.readAsDataURL(file); // Convert file to base64 string
      });
      this.addBookForm.patchValue({ CoverImages: files }); // Update form control
    }
  }

  /**
   * Handles form submission.
   */
  onSubmit(): void {
    if (this.addBookForm.valid) {
      const formData: AddBookDto = this.addBookForm.value;
      console.log(formData);
      this.bookService.addBook(formData).subscribe({
        next: (response) => {
          console.log(response);
          this.addBookForm.reset(); // Reset the form on success
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