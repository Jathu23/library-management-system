import { Component, OnInit } from '@angular/core';
import { StaticsService } from '../../../services/staticsservice/statics.service';

@Component({
  selector: 'app-statics',
  templateUrl: './statics.component.html',
  styleUrls: ['./statics.component.css'] // Note: use styleUrls instead of styleUrl
})
export class StaticsComponent implements OnInit {
  borrowingTrends: any[] = [];
  allBorrowingTrends: any[] = [
    {
        "name": "year 2022",
        "series": [
            {"name": "January", "value": 10},
            {"name": "February", "value": 15},
            {"name": "March", "value": 22},
            {"name": "April", "value": 18},
            {"name": "May", "value": 30},
            {"name": "June", "value": 37},
            {"name": "July", "value": 45},
            {"name": "August", "value": 48},
            {"name": "September", "value": 50},
            {"name": "October", "value": 53},
            {"name": "November", "value": 60},
            {"name": "December", "value": 65}
        ]
    },
    {
        "name": "year 2023",
        "series": [
            {"name": "January", "value": 5},
            {"name": "February", "value": 13},
            {"name": "March", "value": 19},
            {"name": "April", "value": 23},
            {"name": "May", "value": 28},
            {"name": "June", "value": 32},
            {"name": "July", "value": 36},
            {"name": "August", "value": 42},
            {"name": "September", "value": 46},
            {"name": "October", "value": 51},
            {"name": "November", "value": 54},
            {"name": "December", "value": 58}
        ]
    },
    {
        "name": "year 2024",
        "series": [
            {"name": "January", "value": 8},
            {"name": "February", "value": 7},
            {"name": "March", "value": 11},
            {"name": "April", "value": 15},
            {"name": "May", "value": 20},
            {"name": "June", "value": 25},
            {"name": "July", "value": 30},
            {"name": "August", "value": 33},
            {"name": "September", "value": 37},
            {"name": "October", "value": 42},
            {"name": "November", "value": 47},
            {"name": "December", "value": 2}
        ]
    }
]
;

  view: [number, number] = [700, 400]; // Width and height of the chart
  legend: boolean = true;
  showLabels: boolean = true;

  xAxis: boolean = true;
  yAxis: boolean = true;
  showYAxisLabel: boolean = true;
  showXAxisLabel: boolean = true;
  xAxisLabel: string = 'Month';
  yAxisLabel: string = 'Books Borrowed';
  timeline: boolean = true;



  constructor(private staticsService: StaticsService) {}

  ngOnInit(): void {
    this.fetchBorrowingTrends();
    // this.fetchAllBorrowingTrends()
  }

  fetchBorrowingTrends(): void {
    this.staticsService.getBorrowingTrends().subscribe(data => {
      this.borrowingTrends = data;
      console.log('Borrowing trends for current year:', this.borrowingTrends);

    });
  }

  fetchAllBorrowingTrends(): void {
    this.staticsService.getBorrowingTrendsForAllYears().subscribe(data => {
      this.allBorrowingTrends = data;
      console.log('Borrowing trends for all years:', this.allBorrowingTrends);
    });
  }
}
