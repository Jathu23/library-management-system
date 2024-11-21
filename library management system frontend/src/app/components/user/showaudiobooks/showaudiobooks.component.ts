
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
  pageSize = 3; // Number of items per page
  totalItems = 0; // Total number of audiobooks on the server

  constructor(private getbookservice:GetbooksService) {} // Inject service via constructor

  ngOnInit() {
    this.loadAudiobooks(); // Fetch the initial data
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
}