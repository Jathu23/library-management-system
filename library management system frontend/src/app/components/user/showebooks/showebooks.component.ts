import { Component, OnInit } from '@angular/core';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { LikeanddislikeService } from '../../../services/bookservice/likeanddislike.service';
import { ReviewRequest, ReviewResponse, ReviewService } from '../../../services/bookservice/review.service';
import { environment } from '../../../../environments/environment.testing';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-showebooks',
  templateUrl: './showebooks.component.html',
  styleUrls: ['./showebooks.component.css'],
})
export class ShowebooksComponent implements OnInit {
  ebooks: any[] = [];
  isModalOpen = false;
  selectedEbook: any | null = null;
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;
  sanitizedUrl!: SafeResourceUrl;
  resoursBase = environment.resourcBaseUrl;
  searchQuery = ''; // Variable to store the search query
  isThumbsUp = false;
  isThumbsDown = false;
  showCommentBox = false;
  reviewText = '';
  reviews:any[]=[];
  thumbsUpCount = 12;
  thumbsDownCount = 21;
  currentUserId: number;
  likeCount: any;
  dislikeCount: any;
  rating: number = 1;
  IsSubscribed:boolean=false;
  isLoding = false;

  constructor(
    private getbooksService: GetbooksService,
    private sanitizer: DomSanitizer,
    private likedislikeservice: LikeanddislikeService,
    private reviewservice: ReviewService,
      private http:HttpClient
  ) {
    const tokendata = environment.getTokenData();
    this.currentUserId = Number(tokendata.ID);
    this.fetchLoggedInUser();
  }

  ngOnInit(): void {
    this.loadEbooks();
  }

  loadEbooks(): void {
    if (this.isLoding) return;
    this.isLoding = true;
    this.getbooksService.getebooks(this.currentPage, this.pageSize).subscribe(
      (response) => {
        console.log('API Response:', response);
        if (response?.data?.items && Array.isArray(response.data.items)) {
          this.ebooks = response.data.items;
          this.totalItems = response.data.totalCount ; // Ensure totalItems is assigned
          this.isLoding = false;
        } else {
         
          this.isLoding = false;
        }
      },
      (error) => {
        console.error('Error fetching eBooks:', error);
        this.isLoding = false;
      }
    );
  }
  onPageChange(event: any): void {
    const { pageIndex, pageSize } = event;
    if (pageSize !== this.pageSize) {
      this.currentPage = 1;
    } else {
      this.currentPage = pageIndex + 1;
    }
    this.pageSize = pageSize;
  console.log("pagesizw",this.pageSize);
  console.log("pagenum",this.currentPage);
  
  this.loadEbooks();
  }
  // Handle ebook search functionality
  searchForEbooks(): void {
    this.getbooksService.searchEbooks(this.searchQuery, this.currentPage, this.pageSize).subscribe(
      (response) => {
        console.log('Search API Response:', response);
        if (response?.data?.items && Array.isArray(response.data.items)) {
          this.ebooks = response.data.items;
          this.totalItems = response.data.totalCount || 0;  // Assuming totalCount is returned
        } else {
          console.warn('No eBooks found or invalid data structure:', response);
          this.ebooks = [];
        }
      },
      (error) => {
        console.error('Error searching ebooks:', error);
      }
    );
  }

  // Handle search input change
  onSearchChange(): void {
    this.currentPage = 1; // Reset to the first page on search
    this.loadEbooks();  // Reload ebooks based on search query
  }

  openEbookModal(ebook: any): void {
    this.selectedEbook = ebook;
    this.isModalOpen = true;
    this.fetchEbookReviews(ebook.id)
    this.addClick(ebook.id);
    this.fetchDislikeAndLike(ebook.id, true);
    this.fetchDislikeAndLike(ebook.id, false);

    this.sanitizedUrl = this.sanitizer.bypassSecurityTrustResourceUrl(
      this.resoursBase + this.selectedEbook?.filePath
    );
  }

  closeModal(): void {
    this.isModalOpen = false;
    this.selectedEbook = null;
  }

  onImageError(event: Event): void {
    const target = event.target as HTMLImageElement;

    if (target.src.includes('default-cover.jpg')) {
      console.warn('Default image not found:', target.src);
      return;
    }

    target.src = 'assets/default-cover.jpg';
  }

  toggleThumbsUp(): void {
    this.isThumbsUp = !this.isThumbsUp;
    if (this.isThumbsUp && this.isThumbsDown) {
      this.isThumbsDown = false;
      this.thumbsDownCount--;
    }
    this.thumbsUpCount += this.isThumbsUp ? 1 : -1;
  }

  toggleThumbsDown(): void {
    this.isThumbsDown = !this.isThumbsDown;
    if (this.isThumbsDown && this.isThumbsUp) {
      this.isThumbsUp = false;
      this.thumbsUpCount--;
    }
    this.thumbsDownCount += this.isThumbsDown ? 1 : -1;
  }

  toggleCommentBox(): void {
    this.showCommentBox = !this.showCommentBox;
  }


  fetchLoggedInUser() {
    environment.fetchUserDataById(this.http, this.currentUserId).then((userData) => {
      if (userData) {
        this.IsSubscribed=userData.data.isSubscribed// Store the logged-in user data
        console.log('Logged-in user data:', this.IsSubscribed);
      } else {
        console.warn('Failed to fetch logged-in user data.');
      }
    }).catch((error) => {
      console.error('Error fetching user data:', error);
    });
  }


  fetchEbookReviews(bookId: number): void {
    this.reviewservice.getEbookReviews(bookId).subscribe(
      (response) => {
        if (response.success) {
          console.log('Audiobook Reviews:', response.data);
          // Handle success: e.g., assign data to a local variable
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

  submitEbookReview() {
    // Validate input fields
    if (!this.selectedEbook) {
      alert('Please provide a valid rating (1-5) and a review text.');
      return;
    }

    // Prepare the review request
    const review: ReviewRequest = {
      userId: this.currentUserId, // Replace with actual logged-in user ID
      bookId: this.selectedEbook.id,
      reviewText: this.reviewText,
      rating: this.rating
    };

    // Call the service method to submit the review
    this.reviewservice.addEbookReview(review).subscribe({
      next: (response: ReviewResponse<any>) => {
        if (response.success) {
          alert('Review submitted successfully.');
          this.fetchEbookReviews(this.selectedEbook.id); // Refresh the review list
          this.reviewText = ''; // Clear the review text input
          this.rating = 1; // Reset the rating input
        } else {
          alert(`Failed to submit review: ${response.message}`);
        }
      },
      error: (error) => {
        console.error('Error submitting review:', error);
        alert('An error occurred while submitting the review. Please try again later.');
      }
    });
  }



  fetchDislikeAndLike(bookid: number, isLiked: boolean): void {
    this.likedislikeservice.getEbookLikeDislikeCount(bookid, isLiked).subscribe({
      next: (response) => {
        if (response.success) {
          if (isLiked) {
            this.likeCount = response.data;
          } else {
            this.dislikeCount = response.data;
          }

        } else {
          console.warn('Failed to fetch like/dislike count for audiobook:', response.message);
        }
      },
      error: (error) => {
        console.error('Error fetching like/dislike count for audiobook:', error);
      },
    });
  }

  like_or_dislikeAudiobook(like: boolean): void {
    const likeDislikeRequest = {
      bookId: this.selectedEbook.id,
      userId: this.currentUserId,
      isLiked: like,
    };

    this.likedislikeservice.addEbookLikeDislike(likeDislikeRequest).subscribe({
      next: (response) => {
        if (response.success) {
          alert(response.message);
          if (like) {
            this.fetchDislikeAndLike(this.selectedEbook.id, like);
            if (!response.success) {
              this.toggleThumbsUp();
            }

          } else {
            this.fetchDislikeAndLike(this.selectedEbook.id, like);
            if (!response.success) {
              this.toggleThumbsDown();
            }

          }
        } else {
          console.warn(`Failed to like audiobook: ${response.message}`);
        }
      },
      error: (error) => {
        console.error('Error liking audiobook:', error);
      },
    });
  }

  addClick(id: number) {
    // setTimeout(() => {
    //   console.log(this.selectedEbook);

    // }, 100);

    this.likedislikeservice.addEBookClick(id).subscribe(
      (result) => {
        if (result) {
          console.log('Click added successfully.');
        } else {
          console.log('Failed to add click.');
        }
      },
      (error) => {
        console.error('Error:', error.message);
      }
    );

  }

  // Handle pagination changes


}
