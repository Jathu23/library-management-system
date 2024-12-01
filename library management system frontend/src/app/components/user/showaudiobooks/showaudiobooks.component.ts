
import { Component, OnInit } from '@angular/core';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';

@Component({
  selector: 'app-showaudiobooks',
  templateUrl: './showaudiobooks.component.html',
  styleUrls: ['./showaudiobooks.component.css']
})
export class ShowaudiobooksComponent implements OnInit {
  audiobooks: any[] = [];
  filteredAudiobooks: any[] = [];
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;
  isLoading = false;
  isModalOpen = false;
  selectedAudiobook: any = null;
  playingAudio: any = null;
  audio = new Audio();
  isPlaying = false;
  progress: number = 0;
  currentTime: string = '0:00';
  duration: string = '0:00';
  searchQuery: string = '';

  constructor(private getbookservice: GetbooksService) {}

  ngOnInit() {
    this.loadAudiobooks();
    this.audio.addEventListener('timeupdate', () => {
      this.updateProgressDisplay();
    });
  }

  loadAudiobooks() {
    if (this.isLoading) return;
    this.isLoading = true;

    this.getbookservice.getaudiobooks(this.currentPage, this.pageSize).subscribe(
      (response) => {
        const result = response.data;
        this.audiobooks = result.items;
        this.totalItems = result.totalCount;
        this.filteredAudiobooks = this.audiobooks; 
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching audiobooks:', error);
        this.isLoading = false;
      }
    );
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
      this.stopAudio();
      this.playingAudio = audiobook;
      this.audio.src = `https://localhost:7261/${audiobook.filePath}`;
      this.audio.play();
      this.isPlaying = true;
    } else {
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

  skipTime(seconds: number) {
    if (this.audio) {
      const newTime = this.audio.currentTime + seconds;
      if (newTime >= 0 && newTime <= this.audio.duration) {
        this.audio.currentTime = newTime;
        this.updateProgressDisplay();
      }
    }
  }

  updateProgressDisplay() {
    this.progress = (this.audio.currentTime / this.audio.duration) * 100;
    this.currentTime = this.formatTime(this.audio.currentTime);
    this.duration = this.formatTime(this.audio.duration);
  }
}










// 22222222222222

// export class ShowaudiobooksComponent implements OnInit {
//   audiobooks: any[] = [];
//   isLoading = false;
//   currentPage = 1;
//   pageSize = 4;
//   totalItems = 0;
//   isModalOpen = false;
//   selectedAudiobook: any = null;
//   audioSrc: string = '';
//   backgroundPlay = false;
//   searchQuery: string = '';
//   isSearchActive = false; // Tracks if the user is currently searching

//   constructor(private getbookservice: GetbooksService) {}

//   ngOnInit() {
//     // this.loadAudiobooks(); 
//     console.log("start search text" ,this.searchQuery);
//     console.log("totla items  ",this.totalItems);
//     console.log("currentPage   ",this.currentPage);
    
//   }

//   loadAudiobooks() {
//     if (this.isLoading) return;
//     this.isLoading = true;
   
//   }

// fetchaudiobooks(){
//   this.getbookservice.getaudiobooks(this.currentPage, this.pageSize).subscribe(
//     (response) => {
//       const result = response.data;

//       this.audiobooks = [...this.audiobooks, ...result.items];
//       this.totalItems = result.totalCount;
//       this.currentPage++;
//       this.isLoading = false;
//     },
//     (error) => {
//       console.error('Error fetching audiobooks:', error);
//       this.isLoading = false;
//     }
//   );
// }

//   onSearch(): void {
//     this.audiobooks = [];

//     console.log("triger Onsearch value  =",this.searchQuery);
//     if (this.searchQuery != '') {
//       console.log("totla items  ",this.totalItems);
//       console.log("currentPage   ",this.currentPage);
    
      
//       this.fetchsearchresult();
//       console.log("after 1 fetch  ");
//       console.log("totla items  ",this.totalItems);
//       console.log("currentPage   ",this.currentPage);
    
//     }else{
//       console.log("emty value");
      
//     }
//     // if (this.isLoading) return;
//     // console.log("triger search f  =",this.searchQuery);
//     // this.isLoading = true;
//     // this.currentPage = 1;
//     // this.audiobooks = [];
//     // if (this.searchQuery === '') {
//     //   this.loadAudiobooks(); 
//     // } else {
//     //   console.log("triger search with value  =",this.searchQuery);
      
//     // }

//   }

//   fetchsearchresult(){
//     this.getbookservice.searchAudiobooks(this.searchQuery, this.currentPage, this.pageSize).subscribe(
//       (response) => {
//         const result = response.data;
//         // console.log("result   ",result.items);
        
//         this.audiobooks = [...this.audiobooks, ...result.items];
//         this.totalItems = result.totalCount;
//         this.currentPage++;
//         this.isLoading = false;
//       },
//       (error) => {
//         console.error('Error searching audiobooks:', error);
//         this.isLoading = false;
//       }
//     );
//   }

//   onScroll() {
//     const scrollContainer = document.querySelector('.scroll-container') as HTMLElement;
//     if (!scrollContainer) return;

//     const { scrollTop, scrollHeight, clientHeight } = scrollContainer;

//     if (scrollTop + clientHeight >= scrollHeight - 10 && this.audiobooks.length < this.totalItems) {
//       console.log("hit scroll  ",this.searchQuery,this.currentPage);
      
//       this.fetchsearchresult();
//       console.log("after 1 hit  ");
//       console.log("totla items  ",this.totalItems);
//       console.log("currentPage   ",this.currentPage);
//     }
//   }

//   openAudioPlayer(audiobook: any) {
//     this.selectedAudiobook = audiobook;
//     this.isModalOpen = true;
//     this.audioSrc = `https://localhost:7261/${audiobook.filePath}`;

//     const audioElement = document.querySelector('audio');
//     if (audioElement) {
//       audioElement.src = this.audioSrc;
//       audioElement.play();
//     }
//   }

//   closeModal() {
//     if (!this.backgroundPlay) {
//       const audioElement = document.querySelector('audio');
//       if (audioElement) {
//         audioElement.pause();
//         audioElement.currentTime = 0;
//       }
//       this.audioSrc = '';
//     }

//     this.isModalOpen = false;
//     this.selectedAudiobook = null;
//   }

//   toggleBackgroundPlay() {
//     this.backgroundPlay = !this.backgroundPlay;
//   }
  
// }










// 11111111111111111111111111

// export class ShowaudiobooksComponent implements OnInit {
//   audiobooks: any[] = []; // Store the fetched audiobooks
//   isLoading = false; // Loading flag for API calls
//   currentPage = 1; // Tracks the current page being fetched
//   pageSize = 10; // Number of items per page
//   totalItems = 0; // Total number of audiobooks on the server
//   isModalOpen = false; // Tracks if the modal is open
//   selectedAudiobook: any = null; // Holds the selected audiobook details

//   constructor(private getbookservice: GetbooksService) {}

//   ngOnInit() {
//     this.loadAudiobooks(); // Fetch the initial data
//   }

//   loadAudiobooks() {
//     if (this.isLoading) return; // Prevent multiple simultaneous calls

//     this.isLoading = true;

//     this.getbookservice.getaudiobooks(this.currentPage, this.pageSize).subscribe(
//       (response) => {
//         const result = response.data; // Extract `data` containing `Items`, `TotalCount`, etc.

//         // Append new audiobooks to the list
//         this.audiobooks = [...this.audiobooks, ...result.items];

//         // Update total items and increment the page for the next fetch
//         this.totalItems = result.totalCount;
//         this.currentPage++;
//         this.isLoading = false; // Mark loading as complete
//       },
//       (error) => {
//         console.error('Error fetching audiobooks:', error);
//         this.isLoading = false; // Reset loading flag in case of error
//       }
//     );
//   }

//   onScroll() {
//     const scrollContainer = document.querySelector('.scroll-container') as HTMLElement;
//     if (!scrollContainer) return;

//     const { scrollTop, scrollHeight, clientHeight } = scrollContainer;

//     // Check if the user has scrolled near the bottom of the container
//     if (scrollTop + clientHeight >= scrollHeight - 10 && this.audiobooks.length < this.totalItems) {
//       this.loadAudiobooks(); // Load the next page of audiobooks
//     }
//   }

//   openModal(audiobook: any) {
//     this.selectedAudiobook = audiobook; // Set the selected audiobook
//     this.isModalOpen = true; // Open the modal
//   }

//   closeModal() {
//     this.isModalOpen = false; // Close the modal
//     this.selectedAudiobook = null; // Clear the selected audiobook
//   }
// }

