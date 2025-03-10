import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';
import { ViewportScrollPosition } from '@angular/cdk/scrolling';
import { BookDeleteServicesService } from '../../../services/bookservice/deletebook.service';
import { MatDialog } from '@angular/material/dialog';
import { EditEbookDialogComponent } from '../edit-ebook-dialog/edit-ebook-dialog.component';
import {MainBookUpdateService} from '../../../services/bookservice/main-book-update.service'
import { environment } from '../../../../environments/environment.testing';

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
  resourcBaseUrl = environment.resourcBaseUrl;

  constructor(
    private getbookservice: GetbooksService,
    private sanitizer: DomSanitizer,
    private EbookDelete:BookDeleteServicesService,
    private dialog: MatDialog,
    private EbookUpdate:MainBookUpdateService
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
    this.markPdfAsRead(ebook.id);

    this.selectedPdfPath = this.sanitizer.bypassSecurityTrustResourceUrl(
    `${this.resourcBaseUrl}${ebook.filePath}`  
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


  deleteEbook(id: number) {
    if (confirm("Do you want to delete this ebook?")) {
      this.EbookDelete.deleteEBook(id).subscribe(
        (response) => {
          this.ebooks = this.ebooks.filter(ebook => ebook.id !== id);
          console.log('Ebook deleted successfully');
          alert('Ebook deleted successfully!');
        },
        (error) => {
          console.error('Error deleting ebook', error);
          alert('Error deleting ebook');
        }
      );
    }
  }
  

  openEditDialog(ebook: any): void {
    const dialogRef = this.dialog.open(EditEbookDialogComponent, {
      width: '500px',
      data: { ...ebook }, 
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.EbookUpdate.updateEbook(result).subscribe(
          (response) => {
            console.log('Ebook updated successfully:', response);
            alert('Ebook updated successfully!');
            const index = this.ebooks.findIndex((e) => e.id === result.id);
            if (index !== -1) this.ebooks[index] = result;
          },
          (error) => {
            console.error('Failed to update ebook:', error);
            alert('Failed to update ebook. Please try again.');
          }
        );
      }
    });
  }

  isModalOpen = false;
  editedEbook: any = {}; 

  closeModal() {
    this.isModalOpen = false;
  }

  submitEditForm() {
    console.log('Edited Ebook:', this.editedEbook);
    this.closeModal();
  }
}

