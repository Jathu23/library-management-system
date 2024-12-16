import { Component, OnInit } from '@angular/core';
import { RentService } from '../../../services/lent-service/rent.service';

@Component({
  selector: 'app-overdue',
  templateUrl: './overdue.component.html',
  styleUrls: ['./overdue.component.css']
})
export class OverdueComponent implements OnInit {
  overdues: any[] = [];  // Array to store overdue records
  isLoading: boolean = false;  // Loading state
  errorMessage: string = '';   // Error message for failed API call
  expandedRows: Set<number> = new Set<number>();  // Set to track expanded rows

  constructor(private lentService: RentService) {}

  ngOnInit(): void {
    this.getAllRentRecords();  // Fetch overdue records on component load
  }

  getAllRentRecords(): void {
    this.isLoading = true;
    this.errorMessage = '';
    this.lentService.getallrentrecods(true).subscribe(
      (response) => {
        if (response && response.data) {
          this.overdues = response.data;  // Store overdue records in the array
        } else {
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

  toggleDetails(index: number): void {
    if (this.expandedRows.has(index)) {
      this.expandedRows.delete(index);  // Close the row if it's already expanded
    } else {
      this.expandedRows.add(index);  // Open the row if it's not expanded
    }
  }

  isRowExpanded(index: number): boolean {
    return this.expandedRows.has(index);  // Check if the row is expanded
  }
}
