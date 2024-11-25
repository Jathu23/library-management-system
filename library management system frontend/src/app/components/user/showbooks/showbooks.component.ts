import { Component } from '@angular/core';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';

@Component({
  selector: 'app-showbooks',
  templateUrl: './showbooks.component.html',
  styleUrl: './showbooks.component.css'
})
export class ShowbooksComponent {
  isLoading = false;
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;
  Normalbooks: any[] = [];

  constructor(private getbookservice: GetbooksService) { }
  
  ngOnInit(): void {
    this.loadnormalbooks()
  }

  loadnormalbooks() {
    if (this.isLoading) return;

    this.isLoading = true;
    this.getbookservice.getNoramlbooks(this.currentPage, this.pageSize).subscribe(
      (response) => {
        if (response.success) {
          const result = response.data;

          this.Normalbooks = [...this.Normalbooks, ...result.items];
          this.totalItems = result.totalCount;
          this.currentPage++;
          this.isLoading = false;
        }
       console.log(this.Normalbooks);
       
      },
      (error) => {
        console.error('Error fetching normalbooks:', error);
        this.isLoading = false;
      }
    );

  }

}
