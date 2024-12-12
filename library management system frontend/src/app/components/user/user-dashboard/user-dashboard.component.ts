import { Component, OnDestroy, OnInit } from '@angular/core';

import { AudiobookService } from '../../../services/bookservice/audiobook.service'

import {GetbooksService} from '../../../services/bookservice/getbooks.service'
@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrl: './user-dashboard.component.css'
})
export class UserDashboardComponent implements OnInit, OnDestroy {

  constructor(
    private audiobookService: AudiobookService,
    private EbookService: GetbooksService


  ) {

  }
  ngOnDestroy(): void {

    if (this.autoSlideInterval) {
      clearInterval(this.autoSlideInterval);  // Clear the interval when the component is destroyed
    }
  }

  audiobooks: any[] = [];
  Ebooks:any[]=[];
  imgageBaseUrl:string=`https://localhost:7261/`

  ngOnInit(): void {
    this.fetchTopAudiobooks(5)
    this.fetchTopEbooks(5)
    this.startAutoSlide();
  }

  fetchTopAudiobooks(count: number): void {
    this.audiobookService.getTopAudiobooks(count).subscribe(
      (data) => {
        this.audiobooks = data;
        console.log(this.audiobooks);

      },
      (error) => {
        console.error('Error fetching audiobooks:', error);
      }
    );

  }

  // fubctions to fetch top e books
  fetchTopEbooks(count: number): void {
    this.EbookService.getTopEbooks(count).subscribe(
      (data) => {
        this.Ebooks = data;
        console.log( "Ebook"+this.Ebooks);

      },
      (error) => {
        console.error('Error fetching audiobooks:', error);
      }
    );

  }

  currentIndex = 0;
  autoSlideInterval: any;

  moveSlide(direction: number) {
    const totalBooks = this.audiobooks.length;
    this.currentIndex = (this.currentIndex + direction + totalBooks) % totalBooks;

    // Update the carousel position (to show the right book)
    const carouselContainer = document.querySelector('.carousel-container') as HTMLElement;
    carouselContainer.style.transform = `translateX(-${this.currentIndex * 100}%)`;  // Move the container based on the current index
  }

  // Function to start automatic sliding
  startAutoSlide() {
    this.autoSlideInterval = setInterval(() => {
      this.moveSlide(1);  // Move to the next slide every 5 seconds
    }, 3000);  // 5000 ms = 5 seconds
  }

  // Pause automatic slide when mouse is over the carousel
  pauseAutoSlide() {
    clearInterval(this.autoSlideInterval);
  }

  // Resume automatic slide when mouse leaves the carousel
  resumeAutoSlide() {
    this.startAutoSlide();
  }

  
  

}
