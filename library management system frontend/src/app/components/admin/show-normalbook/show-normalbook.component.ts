import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';
import { BookDeleteServicesService } from '../../../services/bookservice/book-delete-services.service';

@Component({
  selector: 'app-show-normalbook',
  templateUrl: './show-normalbook.component.html',
  styleUrl: './show-normalbook.component.css'
})
export class ShowNormalbookComponent implements OnInit {

  isLoading = false;
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;
  Nbooks: any[] = [];



  constructor(private getbookservice: GetbooksService, private DleteNBook:BookDeleteServicesService) { }

  ngOnInit(): void {
    this.loadEbooks()
  }
  loadEbooks() {
    if (this.isLoading) return;

    this.isLoading = true;
    this.getbookservice.getNoramlbooks(this.currentPage, this.pageSize).subscribe(
      (response) => {
        const result = response.data;

        this.Nbooks = [...this.Nbooks, ...result.items];
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

  deleteItem(id: number): void {
    if (confirm('Are you sure you want to delete this item?')) {
      this.DleteNBook.deleteItem(id).subscribe({
        next: () => {
          alert('Item deleted successfully.');
          // Optionally refresh data or update UI
        },
        error: (err) => {
          console.error('Error deleting item:', err);
          alert('Failed to delete item.');
        },
      });
    }
  }
}
