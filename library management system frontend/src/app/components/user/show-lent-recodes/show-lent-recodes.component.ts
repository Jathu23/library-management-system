import { Component, OnInit } from '@angular/core';
import { RentService } from '../../../services/lent-service/rent.service';

@Component({
  selector: 'app-show-lent-recodes',
  templateUrl: './show-lent-recodes.component.html',
  styleUrls: ['./show-lent-recodes.component.css'],
})
export class ShowLentRecodesComponent implements OnInit {
  lentRecords: any[] = [];
  isLoading: boolean = false;
  errorMessage: string = '';

  constructor(private rentService: RentService) {}

  ngOnInit(): void {
    this.fetchUserLendingRecords(1); // Replace '1' with the actual userId dynamically
  }

  fetchUserLendingRecords(userId: number): void {
    this.isLoading = true;
    this.errorMessage = '';
    this.rentService.getUserLentRecords(userId).subscribe(
      (response) => {
        if (response.success && response.data) {
          this.lentRecords = response.data;
        } else {
          this.errorMessage = 'No records found for the user.';
        }
        this.isLoading = false;
      },
      (error) => {
        this.errorMessage = 'An error occurred while fetching the lending records.';
        this.isLoading = false;
        console.error('Error fetching user lending records:', error);
      }
    );
  }
}
