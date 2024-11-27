import { Component, OnInit } from '@angular/core';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';

@Component({
  selector: 'app-showbooks',
  templateUrl: './showbooks.component.html',
  styleUrls: ['./showbooks.component.css']
})
export class ShowbooksComponent implements OnInit {
<<<<<<< HEAD
  
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
=======
  isLoading = false;
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;
  Normalbooks: any[] = [];
  searchQuery: string = '';
  isSearchActive = false;
  isModalOpen = false;
  selectedBook: any = null;
  currentImageIndex = 0;

  constructor(private getbookservice: GetbooksService) {}

  ngOnInit(): void {
    this.loadNormalBooks();
  }

  loadNormalBooks(): void {
    if (this.isLoading) return;

>>>>>>> 33e886cef7b1cd36617ca74019b21c4420523e08
    this.isLoading = true;

<<<<<<< HEAD
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
=======
          // Ensure coverImagePath is treated as an array and resolve paths
          this.Normalbooks = [
            ...this.Normalbooks,
            ...result.items.map((book: any) => ({
              ...book,
              coverImagePath: Array.isArray(book.coverImagePath)
                ? book.coverImagePath.map((path: string) => this.resolveImagePath(path))
                : [this.resolveImagePath(book.coverImagePath)]
            }))
          ];
          this.totalItems = result.totalCount;
          this.currentPage++;
          this.isLoading = false;
        }
      },
      (error) => {
        console.error('Error fetching normal books:', error);
>>>>>>> 33e886cef7b1cd36617ca74019b21c4420523e08
        this.isLoading = false;
      }
    );
  }

<<<<<<< HEAD
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
=======
  onSearch(): void {
    if (this.isLoading) return;

    this.isLoading = true;
    this.currentPage = 1;
    this.Normalbooks = [];
    this.isSearchActive = !!this.searchQuery;

    if (this.searchQuery === '') {
      this.loadNormalBooks();
    } else {
      this.getbookservice.searchAudiobooks(this.searchQuery, this.currentPage, this.pageSize).subscribe(
        (response) => {
          if (response.success) {
            const result = response.data;

            this.Normalbooks = [
              ...this.Normalbooks,
              ...result.items.map((book: any) => ({
                ...book,
                coverImagePath: Array.isArray(book.coverImagePath)
                  ? book.coverImagePath.map((path: string) => this.resolveImagePath(path))
                  : [this.resolveImagePath(book.coverImagePath)]
              }))
            ];
            this.totalItems = result.totalCount;
            this.currentPage++;
          }
          this.isLoading = false;
        },
        (error) => {
          console.error('Error searching books:', error);
          this.isLoading = false;
        }
      );
    }
  }

  onScroll(): void {
    const scrollContainer = document.querySelector('.scroll-container') as HTMLElement;
    if (!scrollContainer) return;

    const { scrollTop, scrollHeight, clientHeight } = scrollContainer;
    if (scrollTop + clientHeight >= scrollHeight - 10 && this.Normalbooks.length < this.totalItems) {
      this.isSearchActive ? this.onSearch() : this.loadNormalBooks();
    }
  }

  openModal(book: any): void {
    this.selectedBook = book;
    this.isModalOpen = true;
    this.currentImageIndex = 0; // Reset slider index when opening the modal
  }

  closeModal(): void {
    this.isModalOpen = false;
    this.selectedBook = null;
  }

  nextImage(): void {
    if (!this.selectedBook || !this.selectedBook.coverImagePath) return;
    this.currentImageIndex = (this.currentImageIndex + 1) % this.selectedBook.coverImagePath.length;
  }

  prevImage(): void {
    if (!this.selectedBook || !this.selectedBook.coverImagePath) return;
    this.currentImageIndex =
      (this.currentImageIndex - 1 + this.selectedBook.coverImagePath.length) % this.selectedBook.coverImagePath.length;
  }

  resolveImagePath(path: string): string {
    if (!path) {
      return 'assets/images/defaultcover.jpg'; // Fallback to default cover
    }
    return path.startsWith('http') ? path : `assets/images/${path}`;
  }
>>>>>>> 33e886cef7b1cd36617ca74019b21c4420523e08
}
