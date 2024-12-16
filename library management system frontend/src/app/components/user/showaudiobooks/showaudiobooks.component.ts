import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';
import { ReviewRequest, ReviewResponse, ReviewService } from '../../../services/bookservice/review.service';
import { LikeanddislikeService } from '../../../services/bookservice/likeanddislike.service';
import { environment } from '../../../../environments/environment.testing';
import { MatPaginator } from '@angular/material/paginator';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-showaudiobooks',
  templateUrl: './showaudiobooks.component.html',
  styleUrls: ['./showaudiobooks.component.css']
})
export class ShowaudiobooksComponent implements OnInit, OnDestroy {
  audiobooks: any[] = []; // Store all fetched audiobooks

  currentPage = 1;
  pageSize = 2;
  totalItems = 0;
  isModalOpen = false;
  selectedAudiobook: any = null;
  playingAudio: any = null;
  audio = new Audio();
  isPlaying = false;
  progress: number = 0;
  currentTime: string = '0:00';
  duration: string = '0:00';
  searchQuery: string = '';
  isLoding = false; // Prevent multiple API calls
  reviews: any[] = []; // Store fetched reviews here
  reviewText: string ='';
  currentUserId: number=0;
  rating: number=1;
  likeCount:number=0;
  dislikeCount:number=0;
  IsSubscribed:boolean=false;
  resoursBase = environment.resourcBaseUrl;
  modelwindow=false;
  currentContext: 'all' | 'search' | 'filter' = 'all'; // Tracks the current operation
  constructor(
     private getbookservice: GetbooksService ,
     private reviewservice:ReviewService, 
     private likedislikeservice:LikeanddislikeService,
     private http:HttpClient
    ) { 
    const tokendata = environment.getTokenData();
    this.currentUserId= Number(tokendata.ID);
    this.fetchLoggedInUser();
  }
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  ngOnInit() {
    this.loadAudiobooks();
    // Event listener for when audio metadata is loaded
    this.audio.addEventListener('loadedmetadata', () => {
      this.duration = this.formatTime(this.audio.duration);

    

    });
    // Event listener for when audio time updates (for progress bar)
    this.audio.addEventListener('timeupdate', () => {
      this.progress = (this.audio.currentTime / this.audio.duration) * 100;
      this.currentTime = this.formatTime(this.audio.currentTime);
    });

    // Event listener for when audio finishes playing
    this.audio.addEventListener('ended', () => {
      this.isPlaying = false;
    });

    // Restore audio state if available
    this.restoreAudioState();

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


  ngOnDestroy() {
    this.saveAudioState();
    this.stopAudio(); // Ensure audio stops when navigating away
  }

  saveAudioState() {
    const audioState = {
      id: this.playingAudio ? this.playingAudio.id : null,
      currentTime: this.audio.currentTime,
      isPlaying: this.isPlaying,
    };
    localStorage.setItem('audioState', JSON.stringify(audioState));
  }

  restoreAudioState() {
    const audioState = localStorage.getItem('audioState');
    if (audioState) {
      const state = JSON.parse(audioState);
      if (state.id) {
        const savedAudiobook = this.audiobooks.find(a => a.id === state.id);
        if (savedAudiobook) {
          this.playingAudio = savedAudiobook;
          this.audio.src = `https://localhost:7261/${savedAudiobook.filePath}`;
          this.audio.currentTime = state.currentTime;
          this.progress = (this.audio.currentTime / this.audio.duration) * 100;
          this.currentTime = this.formatTime(this.audio.currentTime);
          this.duration = this.formatTime(this.audio.duration);

          if (state.isPlaying) {
            this.audio.play();
            this.isPlaying = true;
          }
        }
      }
    }
  }

  loadAudiobooks() {
    if (this.isLoding) {
      return;
    }
    this.currentContext = 'all';
    // Only call the API if data has not been loaded
    if (!this.isLoding) {
      this.isLoding = true;
      this.getbookservice.getaudiobooks(this.currentPage, this.pageSize).subscribe(
        (response) => {
          console.log('API Response:', response);
          const result = response.data;
          this.audiobooks = result.items;
          this.totalItems = result.totalCount;
          this.isLoding = false;
          this.restoreAudioState(); // Restore the state after loading audiobooks
        },
        (error) => {
          this.isLoding = false;
          console.error('Error fetching audiobooks:', error);
        }
      );
    }
  }
 // Search books
 onSearch(): void {
  this.currentPage = 1; // Reset pagination
  this.currentContext = 'search';
  this.fetchBooks(); // Unified fetch logic
}

fetchBooks(): void {
  if (this.isLoding) return;
  this.isLoding = true;

  if (this.currentContext === 'all') {
    this.loadAudiobooks();
  } else if (this.currentContext === 'search') {
    this.getbookservice
      .searchAudiobooks(this.searchQuery, this.currentPage, this.pageSize)
      .subscribe(
        (response) => {
          const result = response.data;
          this.audiobooks = result.items;
          this.totalItems = result.totalCount;
          this.isLoding = false;
          this.restoreAudioState(); // Restore the state after loading audiobooks
        }
        ,
        (error) => {
          console.error('Error fetching search results:', error);
          this.isLoding = false;
        }
      );
  } else if (this.currentContext === 'filter') {
    // this.getbookservice
    //   .Categorize(this.selectedGenre, this.selectedAuthor, this.selectedYear, this.currentPage, this.pageSize)
    //   .subscribe(
    //     (response) => this.handleBookResponse(response),
    //     (error) => {
    //       console.error('Error fetching filtered books:', error);
    //       this.isLoding = false;
    //     }
    //   );
  }
}

  openModal(audiobook: any) {
    this.selectedAudiobook = audiobook;
    this.isModalOpen = true;
    this.fetchAudiobookReviews(audiobook.id);
    this.fetchDislikeAndLike(audiobook.id,true);
    this.fetchDislikeAndLike(audiobook.id,false);
  }

  closeModal() {
    this.isModalOpen = false;
  }

  playAudio(audiobook: any) {
    if(this.IsSubscribed===false){
      this.modelwindow=true
      return;
    }
    if (this.playingAudio?.id !== audiobook.id || !this.isPlaying) {
      // Stop the currently playing audio if any
      this.stopAudio();

      // Set the new audio track and play it
      this.playingAudio = audiobook;
      this.audio.src = `https://localhost:7261/${audiobook.filePath}`;
      this.audio.play();
      this.isPlaying = true;
    } else {
      // If the same audio is already playing, pause it
      this.audio.pause();
      this.isPlaying = false;
    }
  }

  togglePlay() {
    if (this.isPlaying) {
      this.audio.pause();
    } else {
      this.audio.play();
    }
    this.isPlaying = !this.isPlaying;
  }

  updateProgress(event: any) {
    this.audio.currentTime = (event.target.value / 100) * this.audio.duration;
  }

  stopAudio() {
    if (this.audio) {
      this.audio.pause();
      this.audio.currentTime = 0;
      this.isPlaying = false;
    }
  }

  // Handle pagination changes
  onPageChange(event: any): void {
    const { pageIndex, pageSize } = event;
    if (pageSize !== this.pageSize) {
      this.currentPage = 1;
    } else {
      this.currentPage = pageIndex + 1;
    }
    this.pageSize = pageSize;
    this.loadAudiobooks();
  
    
  }

  formatTime(seconds: number): string {
    const minutes = Math.floor(seconds / 60);
    const secs = Math.floor(seconds % 60);
    return `${minutes}:${secs < 10 ? '0' : ''}${secs}`;
  }

  skipForward() {
    this.audio.currentTime += 5;
    if (this.audio.currentTime > this.audio.duration) {
      this.audio.currentTime = this.audio.duration;
    }
    this.updateProgressBar();
  }

  skipBackward() {
    this.audio.currentTime -= 5;
    if (this.audio.currentTime < 0) {
      this.audio.currentTime = 0;
    }
    this.updateProgressBar();
  }

  updateProgressBar() {
    this.progress = (this.audio.currentTime / this.audio.duration) * 100;
    this.currentTime = this.formatTime(this.audio.currentTime);
  }






// State Variables
isThumbsUp = false;
isThumbsDown = false;
showCommentBox = false;

// Toggle Thumbs Up
toggleThumbsUp() {
  this.isThumbsUp = !this.isThumbsUp;
  if (this.isThumbsUp && this.isThumbsDown) {
    this.isThumbsDown = false; // Ensure only one is active
  }
}

// Toggle Thumbs Down
toggleThumbsDown() {
  this.isThumbsDown = !this.isThumbsDown;
  if (this.isThumbsDown && this.isThumbsUp) {
    this.isThumbsUp = false; // Ensure only one is active
  }
}

// Toggle Comment Box
toggleCommentBox() {
  this.showCommentBox = !this.showCommentBox;
}


fetchAudiobookReviews(bookId: number): void {
  this.reviewservice.getAudiobookReviews(bookId).subscribe(
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

submitAudiobookReview() {
  // Validate input fields
  if (!this.selectedAudiobook ) {
    alert('Please provide a valid rating (1-5) and a review text.');
    return;
  }

  // Prepare the review request
  const review: ReviewRequest = {
    userId: this.currentUserId, // Replace with actual logged-in user ID
    bookId: this.selectedAudiobook.id,
    reviewText: this.reviewText,
    rating: this.rating
  };

  // Call the service method to submit the review
  this.reviewservice.addAudiobookReview(review).subscribe({
    next: (response: ReviewResponse<any>) => {
      if (response.success) {
        alert('Review submitted successfully.');
        this.fetchAudiobookReviews(this.selectedAudiobook.id); // Refresh the review list
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



fetchDislikeAndLike(bookid:number, isLiked: boolean): void {
  this.likedislikeservice.getAudiobookLikeDislikeCount(bookid, isLiked).subscribe({
    next: (response) => {
      if (response.success) {
        if (isLiked) {
          this.likeCount = response.data;
        }else{
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

like_or_dislikeAudiobook(like:boolean): void {
  const likeDislikeRequest = {
    bookId: this.selectedAudiobook.id,
    userId: this.currentUserId,
    isLiked: like,
  };

  this.likedislikeservice.addAudiobookLikeDislike(likeDislikeRequest).subscribe({
    next: (response) => {
      if (response.success) {
       alert(response.message);
       if (like) {
        this.fetchDislikeAndLike(this.selectedAudiobook.id,like);
        if (!response.success){
          this.toggleThumbsUp(); 
        }
        
       }else{
        this.fetchDislikeAndLike(this.selectedAudiobook.id,like);
        if (!response.success){
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

addClick(){
  this.likedislikeservice.addAudioBookClick(this.selectedAudiobook.id).subscribe(
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

closeModal1(){
  this.modelwindow=false;
  this.closeModal();
}


}