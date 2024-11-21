import { Component, OnInit } from '@angular/core';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';

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
  hasMoreEbooks = true; // To track if more eBooks are available

  constructor(private getbooksService: GetbooksService) {}

  ngOnInit(): void {
    this.loadEbooks();
  }

  loadEbooks(): void {
    this.getbooksService.getebooks(this.currentPage, this.pageSize).subscribe(
      (data) => {
        console.log('Ebooks fetched:', data);
        if (data.items && data.items.length > 0) {
          this.ebooks = data.items;
        } else {
          console.warn('No eBooks found!');
        }
      },
      (error) => {
        console.error('Error fetching eBooks:', error);
      }
    );
  }
  

  onScroll(): void {
    // Implement lazy loading or infinite scrolling
    if (this.hasMoreEbooks) {
      this.currentPage++;
      this.loadEbooks();
    }
  }

  openEbookModal(ebook: any): void {
    this.selectedEbook = ebook;
    this.isModalOpen = true;
  }

  closeModal(): void {
    this.isModalOpen = false;
    this.selectedEbook = null;
  }
}
