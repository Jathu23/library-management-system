import { Component, OnInit, OnDestroy } from '@angular/core';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';
import { ReviewService } from '../../../services/bookservice/review.service';

@Component({
  selector: 'app-showaudiobooks',
  templateUrl: './showaudiobooks.component.html',
  styleUrls: ['./showaudiobooks.component.css']
})
export class ShowaudiobooksComponent implements OnInit, OnDestroy {
  audiobooks: any[] = []; // Store all fetched audiobooks
  filteredAudiobooks: any[] = []; // Store audiobooks after filtering
  currentPage = 1;
  pageSize = 10;
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
  isAudiobooksLoaded = false; // Prevent multiple API calls
  reviews: any[] = []; // Store fetched reviews here

  constructor(private getbookservice: GetbooksService , private reviewservice:ReviewService) { 
  
  }

  ngOnInit() {
    this.fetchAudiobookReviews(1);
    // this.loadAudiobooks();

    // // Event listener for when audio metadata is loaded
    // this.audio.addEventListener('loadedmetadata', () => {
    //   this.duration = this.formatTime(this.audio.duration);
    // });

    // // Event listener for when audio time updates (for progress bar)
    // this.audio.addEventListener('timeupdate', () => {
    //   this.progress = (this.audio.currentTime / this.audio.duration) * 100;
    //   this.currentTime = this.formatTime(this.audio.currentTime);
    // });

    // // Event listener for when audio finishes playing
    // this.audio.addEventListener('ended', () => {
    //   this.isPlaying = false;
    // });

    // // Restore audio state if available
    // this.restoreAudioState();
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
    // Only call the API if data has not been loaded
    if (!this.isAudiobooksLoaded) {
      this.getbookservice.getaudiobooks(this.currentPage, this.pageSize).subscribe(
        (response) => {
          console.log('API Response:', response);
          const result = response.data;
          this.audiobooks = result.items;
          this.totalItems = result.totalCount;
          this.filteredAudiobooks = [...this.audiobooks]; // Initially show all audiobooks
          this.isAudiobooksLoaded = true; // Mark data as loaded
          this.restoreAudioState(); // Restore the state after loading audiobooks
        },
        (error) => {
          console.error('Error fetching audiobooks:', error);
        }
      );
    }
  }

  filterAudiobooks() {
    const query = this.searchQuery.toLowerCase();
    this.filteredAudiobooks = this.audiobooks.filter((audiobook) =>
      audiobook.title.toLowerCase().includes(query) ||
      audiobook.author.toLowerCase().includes(query) ||
      audiobook.genre.toLowerCase().includes(query)
    );
  }

  openModal(audiobook: any) {
    this.selectedAudiobook = audiobook;
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
  }

  playAudio(audiobook: any) {
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

  onPageChange(event: any) {
    const { pageIndex, pageSize } = event;
    this.currentPage = pageIndex + 1;
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

fetchdislikeandlike(){
  
}


}