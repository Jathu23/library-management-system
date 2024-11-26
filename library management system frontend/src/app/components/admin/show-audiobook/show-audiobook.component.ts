import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';

@Component({
  selector: 'app-show-audiobook',
  templateUrl: './show-audiobook.component.html',
  styleUrl: './show-audiobook.component.css'
})
export class ShowAudiobookComponent implements OnInit {
  audiobooks: any[] = [];
  isLoading = false;
  totalItems = 0;
  pageSize = 2; // Default page size
  currentPage = 1;
  expandedElementId: number | null = null;
  element: any;
  

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private getbookservice: GetbooksService) {}

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



  toggleRow(id:any): void {
    this.element =id;
    this.expandedElementId = this.expandedElementId === id ? null : id;
  }
}



// import { Component, OnInit } from '@angular/core';
// import { GetbooksService } from '../../../services/bookservice/getbooks.service';

// @Component({
//   selector: 'app-show-audiobook',
//   templateUrl: './show-audiobook.component.html',
//   styleUrl: './show-audiobook.component.css'
// })
// export class ShowAudiobookComponent implements OnInit {
//   audiobooks: any[] = [];
//   isLoading = false;
//   currentPage = 1;
//   pageSize = 17;
//   totalItems = 0;


//   constructor(private getbookservice: GetbooksService) {}

//   ngOnInit() {
//     this.loadAudiobooks(); // Load initial data
//   }

//   loadAudiobooks() {
//     if (this.isLoading) return;

//     this.isLoading = true;
//     this.getbookservice.getaudiobooks(this.currentPage, this.pageSize).subscribe(
//       (response) => {
//         const result = response.data;

//         this.audiobooks = [...this.audiobooks, ...result.items];
//         this.totalItems = result.totalCount;
//         this.currentPage++;
//         this.isLoading = false;
//       },
//       (error) => {
//         console.error('Error fetching audiobooks:', error);
//         this.isLoading = false;
//       }
//     );
//   }

//   expandedElementId: number | null = null;
// element: any;


//   toggleRow(id:any): void {
//     this.element =id;
//     this.expandedElementId = this.expandedElementId === id ? null : id;
//   }
// }
