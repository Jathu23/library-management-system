import { Component, OnInit } from '@angular/core';
import { RentService } from '../../../services/lent-service/rent.service';
import { environment } from '../../../../environments/environment.testing';

@Component({
  selector: 'app-show-lent-recodes',
  templateUrl: './show-lent-recodes.component.html',
  styleUrls: ['./show-lent-recodes.component.css'],
})
export class ShowLentRecodesComponent implements OnInit {
  lentRecords: any[] = [];
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
