import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';

@Component({
  selector: 'app-show-ebook',
  templateUrl: './show-ebook.component.html',
  styleUrls: ['./show-ebook.component.css'],
})
export class ShowEbookComponent implements OnInit {
  isLoading = false;
  currentPage = 0;
  pageSize = 10;
  totalItems = 0;
  ebooks: any[] = [];
  expandedElementId: number | null = null;
  selectedPdfPath: string | null = null;

  constructor(
    private getbookservice: GetbooksService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit() {
    this.loadEbooks();
  }

  loadEbooks() {
    if (this.isLoading) return;

    this.isLoading = true;
    this.getbookservice.getebooks(this.currentPage, this.pageSize).subscribe(
      (response) => {
        const result = response.data;

        this.ebooks = result.items;
        this.totalItems = result.totalCount;
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching ebooks:', error);
        this.isLoading = false;
      }
    );
  }

  toggleRow(elementId: number): void {
    this.expandedElementId =
      this.expandedElementId === elementId ? null : elementId;
  }

  onPageChange(event: any): void {
    const { pageIndex, pageSize } = event;

    if (pageSize !== this.pageSize) {
      this.currentPage = 0;
    } else {
      this.currentPage = pageIndex;
    }

    this.pageSize = pageSize;
    this.loadEbooks();
  }

  viewPdf(ebook: any) {
    // Mark the PDF as read
    this.markPdfAsRead(ebook.id);

    // Update the selected PDF path for the embed tag
    this.selectedPdfPath = this.sanitizer.bypassSecurityTrustResourceUrl(
      ebook.filePath
    ) as string;
  }

  markPdfAsRead(ebookId: number) {
    this.getbookservice.markPdfAsRead(ebookId).subscribe(
      (response) => {
        console.log(`Ebook ${ebookId} marked as read.`);
      },
      (error) => {
        console.error(`Failed to mark ebook ${ebookId} as read:`, error);
      }
    );
  }

  onPdfLoad(ebookId: number) {
    console.log(`PDF for Ebook ID ${ebookId} has loaded.`);
  }
}
