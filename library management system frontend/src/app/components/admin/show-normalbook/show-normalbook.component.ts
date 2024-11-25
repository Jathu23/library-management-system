import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';

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



  constructor(private getbookservice: GetbooksService) { }

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

}
