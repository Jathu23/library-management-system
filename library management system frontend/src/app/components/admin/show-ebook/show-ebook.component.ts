import { Component, OnInit } from '@angular/core';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';

@Component({
  selector: 'app-show-ebook',
  templateUrl: './show-ebook.component.html',
  styleUrl: './show-ebook.component.css'
})
export class ShowEbookComponent implements OnInit{
  isLoading = false;
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;
   ebooks: any[] = [];

  constructor(private getbookservice: GetbooksService) {}

  ngOnInit() {
    this.loadEbooks(); // Load initial data
  }

  loadEbooks() {
    if (this.isLoading) return;

    this.isLoading = true;
    this.getbookservice.getebooks(this.currentPage, this.pageSize).subscribe(
      (response) => {
        const result = response.data;

        this.ebooks = [...this.ebooks, ...result.items];
        this.totalItems = result.totalCount;
        this.currentPage++;
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching audiobooks:', error);
        this.isLoading = false;
      }
    );
  }




  expandedElementId: number | null = null;

  toggleRow(elementId: number): void {
    this.expandedElementId = this.expandedElementId === elementId ? null : elementId;
  }
}
