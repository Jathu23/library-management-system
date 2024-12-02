import { Component, OnInit } from '@angular/core';
import { RentService } from '../../../services/lent-service/rent.service';

@Component({
  selector: 'app-show-lending-history',
  templateUrl: './show-lending-history.component.html',
  styleUrls: ['./show-lending-history.component.css'],
})
export class ShowLendingHistoryComponent implements OnInit {
  lendingHistory: any[] = [];
  isLoading: boolean = false;
  errorMessage: string = '';

  constructor(private rentService: RentService) {}

  ngOnInit(): void {
    this.fetchUserLendingHistory(1); 
  }

  fetchUserLendingHistory(userId: number): void {
    this.isLoading = true;
    this.errorMessage = '';
    this.rentService.getUserLendingHistory(userId).subscribe(
      (response) => {
        if (response.success && response.data) {
          this.lendingHistory = response.data;
        } else {
          this.errorMessage = 'No lending history found for the user.';
        }
        this.isLoading = false;
      },
      (error) => {
        this.errorMessage = 'An error occurred while fetching the lending history.';
        this.isLoading = false; 
        console.error('Error fetching user lending history:', error);
      }
    );
  } 
}
