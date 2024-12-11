import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AddEbookDto, EbookMetadataDto } from '../../../models/interfaces/add-newebook.interface';
import { BookService } from '../../../services/bookservice/addbook.service';

@Component({
  selector: 'app-addebook',
  templateUrl: './addebook.component.html',
  styleUrls: ['./addebook.component.css'], // Fixed typo: styleUrl to styleUrls
})
export class AddebookComponent implements OnInit {
  addEbookForm!: FormGroup;
  isLoading = false;
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;
  Nbooks: any[] = [];
  imagePreviews: string[] = [];

  constructor(private fb: FormBuilder, private bookService: BookService) { 
    this.addEbookForm = this.fb.group({
      ISBN: ['343', Validators.required],
      Title: ['fd', Validators.required],
      Author: ['d', Validators.required],
      Genre: ['fd', Validators.required],
      PublishYear: [
        '1234',
        [Validators.required, Validators.pattern(/^\d{4}$/), Validators.min(2000)],
      ],
      EbookFile: [null, Validators.required],
      CoverImages: [null],
      Metadata: this.fb.group({
        Language: ['df', Validators.required],
        Publisher: ['fd', Validators.required],
        Description: ['fd', Validators.required],
        DigitalRights: ['fd', Validators.required],
      }),
    });
  }

  ngOnInit(): void {
   
  }

  onFileSelect(event: any, field: string): void {
    const files = event.target.files;
    if (!files || files.length === 0) return;

    if (field === 'CoverImages') {
      this.imagePreviews = Array.from(files).map((file: any) =>
        URL.createObjectURL(file)
      );
    }
    this.addEbookForm.get(field)?.setValue(files[0]); // Set first file for single-file upload
  }

  onSubmit(): void {
    if (this.addEbookForm.valid) {
      const formData = this.prepareFormData();
      this.bookService.addEbook(formData).subscribe({
        next: (response) => {
          if (response.success) {
            console.log('Response:', response);
            this.addEbookForm.reset();
            this.imagePreviews = [];
            alert('Ebook added successfully!');
          }else{
            alert('Failed to add the ebook. Please try again.');
          }
          
        },
        error: (error) => {
          console.error('Error:', error);
          alert('Failed to add the ebook. Please try again.');
        },
      });
    } else {
      alert('Please fill in all required fields correctly.');
    }
  }

  private prepareFormData(): AddEbookDto {
    const formValue = this.addEbookForm.value;
    const metadata: EbookMetadataDto = {
      Language: formValue.Metadata.Language,
      Publisher: formValue.Metadata.Publisher,
      Description: formValue.Metadata.Description,
      DigitalRights: formValue.Metadata.DigitalRights,
    };

    const formData: AddEbookDto = {
      ISBN: formValue.ISBN,
      Title: formValue.Title,
      Author: formValue.Author,
      Genre: formValue.Genre,
      PublishYear: formValue.PublishYear,
      EbookFile: formValue.EbookFile,
      CoverImages: formValue.CoverImages,
      Metadata: metadata,
    };

    return formData;
  }
}
