
import { Component, inject, OnInit } from '@angular/core';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';

@Component({
  selector: 'app-showaudiobooks',
  templateUrl: './showaudiobooks.component.html',
  styleUrl: './showaudiobooks.component.css'
})

export class ShowaudiobooksComponent implements OnInit {
  audiobooks: any[] = []; // Store the fetched audiobooks
  isLoading = false; // Loading flag for API calls
  currentPage = 1; // Tracks the current page being fetched
  pageSize = 10; // Number of items per page
  totalItems = 0; // Total number of audiobooks on the server
  isModalOpen = false; // Tracks if the modal is open
  selectedAudiobook: any = null; // Holds the selected audiobook details
  audioSrc: string = ''; // Holds the dynamically generated audio URL
  backgroundPlay = false; 


  
  constructor(private getbookservice: GetbooksService) {}

  ngOnInit() {
    this.loadAudiobooks(); // Fetch the initial data
    console.log(this.backgroundPlay);
  }

  
  loadAudiobooks() {
    if (this.isLoading) return; // Prevent multiple simultaneous calls

    this.isLoading = true;

    this.getbookservice.getaudiobooks(this.currentPage, this.pageSize).subscribe(
      (response) => {
        const result = response.data; // Extract `data` containing `Items`, `TotalCount`, etc.

        // Append new audiobooks to the list
        this.audiobooks = [...this.audiobooks, ...result.items];

        // Update total items and increment the page for the next fetch
        this.totalItems = result.totalCount;
        this.currentPage++;
        this.isLoading = false; // Mark loading as complete
      },
      (error) => {
        console.error('Error fetching audiobooks:', error);
        this.isLoading = false; // Reset loading flag in case of error
      }
    );
  }

  onScroll() {
    const scrollContainer = document.querySelector('.scroll-container') as HTMLElement;
    if (!scrollContainer) return;

    const { scrollTop, scrollHeight, clientHeight } = scrollContainer;

    // Check if the user has scrolled near the bottom of the container
    if (scrollTop + clientHeight >= scrollHeight - 10 && this.audiobooks.length < this.totalItems) {
      this.loadAudiobooks(); // Load the next page of audiobooks
    }
  }

  openAudioPlayer(audiobook: any) {
    this.selectedAudiobook = audiobook; // Set the selected audiobook
    this.isModalOpen = true; // Open the modal
   
    this.audioSrc = `https://localhost:7261/${audiobook.filePath}`; // Generate the audio URL
    const audioElement = document.querySelector('audio');
    if (audioElement) {
      audioElement.src = this.audioSrc; 
      audioElement.play(); // Stop the audio
    }
    console.log(this.selectedAudiobook);
    console.log(this.selectedAudiobook.filePath);
    
  }

  closeModal() {
    if (!this.backgroundPlay) {
      // Stop the audio only if backgroundPlay is disabled
      const audioElement = document.querySelector('audio');
      if (audioElement) {
        audioElement.pause();
        audioElement.currentTime = 0;
      }
      this.audioSrc = ''; // Clear the audio source
    }

    this.isModalOpen = false; // Close the modal
    this.selectedAudiobook = null; // Clear the selected audiobook
  }


  toggleBackgroundPlay() {
    this.backgroundPlay = !this.backgroundPlay; 
    this.backgroundPlay = !this.backgroundPlay; 
    console.log(this.backgroundPlay);
  }
  
  
}




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

