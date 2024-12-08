import { Component, OnInit } from '@angular/core';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';
import { LikeanddislikeService } from '../../../services/bookservice/likeanddislike.service';
import { ReviewService } from '../../../services/bookservice/review.service';

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
  isThumbsUp = false;
  isThumbsDown = false;
  showCommentBox = false;
  reviews: any[] = []; // Store fetched reviews here
  reviewText: string ='';
  currentUserId: number=0;
  rating: number=1;
  likeCount:number=0;
  dislikeCount:number=0;

  constructor(private getbookservice: GetbooksService, private reviewservice:ReviewService, private likedislikeservice:LikeanddislikeService) {

  }

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
    this.isThumbsUp = false;
    this.isThumbsDown = false;
    this.showCommentBox = false;
    this.fetchNormalbookReviews(book.id);
  
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
    return path.startsWith('http') ? path : `https://localhost:7261/${path}`;
  }

  // Thumbs Up Functionality
  toggleThumbsUp(): void {
    this.isThumbsUp = !this.isThumbsUp;
    if (this.isThumbsUp) {
      this.isThumbsDown = false; // Uncheck thumbs down if thumbs up is checked
    }
  }

  // Thumbs Down Functionality
  toggleThumbsDown(): void {
    this.isThumbsDown = !this.isThumbsDown;
    if (this.isThumbsDown) {
      this.isThumbsUp = false; // Uncheck thumbs up if thumbs down is checked
    }
  }

  // Comment Box Functionality
  toggleCommentBox(): void {
    this.showCommentBox = !this.showCommentBox;
  }


  
fetchNormalbookReviews(bookId: number): void {
  this.reviewservice.getNormalBookReviews(bookId).subscribe(
    (response) => {
      if (response.success) {
        console.log('Audiobook Reviews:', response.data);
        
        this.reviews = response.data; // Assuming `reviews` is a component property
      } else {
        console.error('Failed to fetch reviews:', response.message);
        // Optionally display an error message to the user
      }
    },
    (error) => {
      console.error('Error fetching reviews:', error);
      // Handle network or server errors
    }
  );
}

}
