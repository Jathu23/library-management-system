import { Component, OnInit } from '@angular/core';
import { environment } from '../../../../environments/environment.testing';
import { RentService } from '../../../services/lent-service/rent.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.css'
})
export class NotificationComponent implements OnInit {
  notifications: any[] = [];
  isLoading: boolean = false;
  errorMessage: string = '';
  curentUserId:number =0;

  constructor(private rentService: RentService) {
    const tokendata = environment.getTokenData();
    this.curentUserId= Number(tokendata.ID);
  }

  ngOnInit(): void {
    this.fetchUserLendingRecords(this.curentUserId); // Replace '1' with the actual userId dynamically
  }

  fetchUserLendingRecords(userId: number): void {
    this.isLoading = true;
    this.errorMessage = '';
    this.rentService.getUserLentRecords(userId,true).subscribe(
      (response) => {
        if (response.success && response.data) {
          this.notifications = response.data;
        } else {
          this.errorMessage = 'No Notification found for the user.';
        }
        this.isLoading = false;
      },
      (error) => {
        this.errorMessage = 'No Notification found for the user or An error occurred while fetching the Notification.';
        this.isLoading = false;
        console.error('Error fetching user Notification:', error);
      }
    );
  }
}
