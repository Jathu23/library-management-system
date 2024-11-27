import { Component, OnInit } from '@angular/core';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';

@Component({
  selector: 'app-showbooks',
  templateUrl: './showbooks.component.html',
  styleUrls: ['./showbooks.component.css']
})
export class ShowbooksComponent implements OnInit {
  
  books: any[] = [];  
  currentPage: number = 1;  // Default to the first page
  pageSize: number = 10;  
  totalBooks: number = 0; 
  totalPages: number = 0; 
  pageNumbers: number[] = [];  
  isLoading: boolean = false;

  constructor(private getBooksService: GetbooksService) {}

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    if (this.isLoading) return;  // Prevent making multiple requests at once
    this.isLoading = true;

    // Get books for the current page
    this.getBooksService.showBookstoUser(this.currentPage, this.pageSize).subscribe(
      (response) => {
        const result = response.data;

        // Replace the current list of books with the new set of books
        this.books = result.items;

        // Update total book count and calculate total pages
        this.totalBooks = result.totalCount;
        this.totalPages = Math.ceil(this.totalBooks / this.pageSize);

        // Generate pagination numbers
        this.generatePagination();

        this.isLoading = false;
      },
      (error) => {
        console.error('Error loading books:', error);
        this.isLoading = false;
      }
    );
  }

  generatePagination(): void {
    this.pageNumbers = [];
    for (let i = 1; i <= this.totalPages; i++) {
      this.pageNumbers.push(i);
    }
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;  // Update the current page number
      this.loadBooks();  // Load books for the updated page
    }
  }
}
