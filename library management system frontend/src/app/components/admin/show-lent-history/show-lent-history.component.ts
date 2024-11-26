import { Component, OnInit } from '@angular/core';
import { RentService } from '../../../services/lent-service/rent.service';

@Component({
  selector: 'app-show-lent-history',
  templateUrl: './show-lent-history.component.html',
  styleUrl: './show-lent-history.component.css'
})
export class ShowLentHistoryComponent implements OnInit{
  isLoading = false;
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;
   lenthistorys: any[] = [];
   expandedElementId: number | null = null;

   constructor(private rentservice:RentService) {}
  ngOnInit(): void {
    this.loadEbooks();
  }

   loadEbooks() {

    if (this.isLoading) return;

    this.isLoading = true;
    this.rentservice.getrenthistory(this.currentPage, this.pageSize).subscribe(
      (response) => {
        if (response.success) {
          const result = response.data;

          this.lenthistorys = [...this.lenthistorys, ...result.items];
          this.totalItems = result.totalCount;
          this.currentPage++;
          this.isLoading = false;
          console.log(this.lenthistorys);
          
        }
       
      },
      (error) => {
        console.error('Error fetching recods:', error);
        this.isLoading = false;
      }
    );
  }

  toggleRow(id: number): void {
    this.expandedElementId = this.expandedElementId === id ? null : id;
  }

}
