import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';
import { BookDeleteServicesService } from '../../../services/bookservice/deletebook.service';
import { MatDialog } from '@angular/material/dialog';
import { EditBookDialogComponent } from '../edit-book-dialog/edit-book-dialog.component';
import { MainBookUpdateService } from '../../../services/bookservice/main-book-update.service';
import { data } from 'jquery';

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



  constructor(private getbookservice: GetbooksService,
     private DleteNBook:BookDeleteServicesService,
     private DeleetMainBook:BookDeleteServicesService,
     private UpdateMainBook:MainBookUpdateService,
     private dialog: MatDialog
    
    ) { }

  ngOnInit(): void {
    this.loadnormalbooks()
  }
  loadnormalbooks() {
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
        console.error('Error fetching normalobooks:', error);
        this.isLoading = false;
      }
    );

  }

  expandedElementId: number | null = null;

  toggleRow(elementId: number): void {
    this.expandedElementId = this.expandedElementId === elementId ? null : elementId;

  }

  // functions for main books

  deleteMainBook(id:any):void{
    if (confirm('Are you sure you want to delete this item?')) {
      this.DeleetMainBook.deleteMainBook(id).subscribe({
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

  addNewBook(){

  }

  openEditDialog(book: any): void {
    const dialogRef = this.dialog.open(EditBookDialogComponent, {
      width: '600px',
      data: { book },
    });
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.updateBook(result);
      }
    })
  }

    updateBook(updatedBook: any): void {
    
      this.UpdateMainBook.updateBook(updatedBook).subscribe(response => {
      
      });
    }
  // -----------------

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
