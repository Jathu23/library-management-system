import { Component, OnInit } from '@angular/core';
import { RentService } from '../../../services/lent-service/rent.service';

@Component({
  selector: 'app-show-lent-rec',
  templateUrl: './show-lent-rec.component.html',
  styleUrls: ['./show-lent-rec.component.css']
})
export class ShowLentRecComponent implements OnInit {
  lentRecords: any[] = [];
  isLoading = false;   
  errorMessage: string = ''; 
  userId: number = 1;      

  constructor(private lentService: RentService) {}

  ngOnInit(): void {
    this.getallrentrecods(); 
  }

 
  getallrentrecods(): void {
    this.isLoading = true;
    this.errorMessage = ''; 

    this.lentService.getallrentrecods().subscribe(
      (response) => {
        if (response && response.data) {
          this.lentRecords = response.data; 
          console.log('Fetched records:', this.lentRecords);
        } else {
          console.error('Unexpected response format:', response);
          this.errorMessage = 'Failed to fetch data. Please try again later.';
        }
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching data:', error);
        this.errorMessage = 'An error occurred while fetching the records.';
        this.isLoading = false; 
      }
    );
  }

 
  loadRecords(): void {
    this.isLoading = true; 
    this.errorMessage = ''; 

    this.lentService.getlentrecByuserid(this.userId).subscribe(
      (response) => {
        if (response && response.data) {
          const result = response.data; 
          console.log('User-specific records:', result);
        } else {
          console.error('Unexpected response format:', response);
          this.errorMessage = 'Failed to fetch user-specific data. Please try again later.';
        }
        this.isLoading = false; 
      },
      (error) => {
        console.error('Error fetching user-specific data:', error);
        this.errorMessage = 'An error occurred while fetching user-specific records.';
        this.isLoading = false; 
      }
    );
  }
}
