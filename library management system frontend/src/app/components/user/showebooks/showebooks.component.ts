import { Component, OnInit } from '@angular/core';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

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

  sanitizedUrl!: SafeResourceUrl;

  isThumbsUp = false;
  isThumbsDown = false;
  showCommentBox = false;
  reviewText = '';
  reviews: { user: string; text: string; date: string }[] = [];
  thumbsUpCount = 12;
  thumbsDownCount = 21;

  constructor(
    private getbooksService: GetbooksService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.loadEbooks();
  }

  loadEbooks(): void {
    this.getbooksService.getebooks(this.currentPage, this.pageSize).subscribe(
      (response) => {
        console.log('API Response:', response);
        if (response?.data?.items && Array.isArray(response.data.items)) {
          this.ebooks = response.data.items;
        } else {
          console.warn('No eBooks found or invalid data structure:', response);
          this.ebooks = [];
        }
      },
      (error) => {
        console.error('Error fetching eBooks:', error);
      }
    );
  }

  openEbookModal(ebook: any): void {
    this.selectedEbook = ebook;
    this.isModalOpen = true;

    this.sanitizedUrl = this.sanitizer.bypassSecurityTrustResourceUrl(
      'https://localhost:7261/' + this.selectedEbook?.filePath
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

  submitComment(): void {
    if (this.reviewText.trim()) {
      this.reviews.push({
        user: 'john_doe',
        text: this.reviewText,
        date: 'Just now',
      });
      this.reviewText = '';
    }
  }
}
