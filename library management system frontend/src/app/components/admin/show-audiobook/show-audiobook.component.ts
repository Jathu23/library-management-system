import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';
import { BookDeleteServicesService } from '../../../services/bookservice/deletebook.service';
import { MainBookUpdateService } from '../../../services/bookservice/main-book-update.service';
import { EditAudiobookDialogComponent } from '../edit-audiobook-dialog/edit-audiobook-dialog/edit-audiobook-dialog.component';
import { environment } from '../../../../environments/environment.testing';

@Component({
  selector: 'app-show-audiobook',
  templateUrl: './show-audiobook.component.html',
  styleUrls: ['./show-audiobook.component.css']
})
export class ShowAudiobookComponent implements OnInit {
  audiobooks: any[] = [];
  isLoading = false;
  totalItems = 0;
  pageSize = 2; 
  currentPage = 1;
  expandedElementId: number | null = null; 
  resoursbase= environment.resourcBaseUrl;

  constructor(
    private getbookservice: GetbooksService,
    private AudiobookDelete: BookDeleteServicesService,
    private AudiobookUpdate: MainBookUpdateService,
    private dialog: MatDialog
  ) {}

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngOnInit() {
    this.loadAudiobooks(0, this.pageSize);
  }

  loadAudiobooks(pageIndex: number, pageSize: number) {
    this.isLoading = true;
    this.getbookservice.getaudiobooks(pageIndex + 1, pageSize).subscribe(
      (response) => {
        const result = response.data;
        this.audiobooks = result.items;
        this.totalItems = result.totalCount;
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching audiobooks:', error);
        this.isLoading = false;
      }
    );
  }

  onPageChange(event: any) {
    const { pageIndex, pageSize } = event;
    if (pageSize !== this.pageSize) {
      this.currentPage = 1;
    } else {
      this.currentPage = pageIndex + 1;
    }
    this.pageSize = pageSize;
    this.loadAudiobooks(this.currentPage - 1, this.pageSize);
  }

  openEditDialog(audiobook: any): void {
    const dialogRef = this.dialog.open(EditAudiobookDialogComponent, {
      width: '500px',
      data: { ...audiobook }, 
    });
  
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.AudiobookUpdate.updateAudiobook(result).subscribe(
          () => {
            alert('Audiobook updated successfully!');
            this.loadAudiobooks(0, this.pageSize); 
          },
          (error) => {
            console.error('Error updating audiobook:', error);
            alert('Failed to update audiobook.');
          }
        );
      }
    });
  }
  
  deleteAudiobook(id: number): void {
    if (confirm('Do you want to delete this audiobook?')) {
      this.AudiobookDelete.deleteAudioBook(id).subscribe(
        () => {
          this.audiobooks = this.audiobooks.filter((a) => a.id !== id);
          alert('Audiobook deleted successfully!');
        },
        (error) => {
          console.error('Error deleting audiobook:', error);
          alert('Error deleting audiobook.');
        }
      );
    }
  }

  toggleRow(id: number): void {
    this.expandedElementId = this.expandedElementId === id ? null : id;
  }
}
