import { Component, OnInit } from '@angular/core';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';

@Component({
  selector: 'app-showbooks',
  templateUrl: './showbooks.component.html',
  styleUrls: ['./showbooks.component.css']
})
export class ShowbooksComponent implements OnInit {
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

    this.isLoading = true;
    this.getbookservice.getNoramlbooks(this.currentPage, this.pageSize).subscribe(
      (response) => {
        if (response.success) {
          const result = response.data;

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
        this.isLoading = false;
      }
    );
  }

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
}