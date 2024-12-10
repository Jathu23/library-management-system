import { Component,OnInit  } from '@angular/core';
import { Color, LegendPosition, ScaleType } from '@swimlane/ngx-charts';
import { UserService } from '../../../services/user-service/user.service';

import {AudiobookService} from '../../../services/bookservice/audiobook.service'


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'], // Fixed `styleUrl` to `styleUrls`
})
export class DashboardComponent implements OnInit {

  constructor(
    private userService:UserService,
    private audiobookService:AudiobookService
  ) {
    
  }

  pieData: { name: string; value: number }[] = []; 
  totalUsers: number | null = null;
  activeUsers: number | null = null;
  NonActiveUsers: number | null = null;
  Subscribed: number | null = null;
  nonsubscrivedUser: number | null = null;


  // -------------




  imgageBaseUrl:string=`https://localhost:7261/`

  // showing the top books

  audiobooks: any[] = []; 
  audiobookslists: any[] = []; 


 ngOnInit(): void {
  this.loadUserCounts();
  this.fetchTopAudiobooks(3);
  this.fetchTopAudiobookslist(10)
}



loadUserCounts(): void {
  // Fetch total user count
  this.userService.getTotalUserCount().subscribe({
    next: (count) => {
      this.totalUsers = count;
      this.updatePieData(); // Update the pie chart after getting the data
    },
    error: (err) => console.error('Failed to fetch total user count', err),
  });

  // Fetch active user count
  this.userService.getActiveUserCount().subscribe({
    next: (count) => {
      this.activeUsers = count;
      this.updatePieData(); // Update the pie chart after getting the data
    },
    error: (err) => console.error('Failed to fetch active user count', err),
  });

  // Fetch non-active user count
  this.userService.getNonActiveUserCount().subscribe({
    next: (count) => {
      this.NonActiveUsers = count;
      this.updatePieData(); // Update the pie chart after getting the data
    },
    error: (err) => console.error('Failed to fetch non-active user count', err),
  });

  // Fetch subscribed user count
  this.userService.getSubscribedUserCount().subscribe({
    next: (count) => {
      this.Subscribed = count;
      this.updatePieData(); // Update the pie chart after getting the data
    },
    error: (err) => console.error('Failed to fetch subscribed user count', err),
  });
}
  

  // -------------

  // chartData = [
  //   { name: 'January', value: 5000 },
  //   { name: 'February', value: 7200 },
  //   { name: 'March', value: 6700 },
  //   { name: 'April', value: 8000 },
  //   { name: 'May', value: 9000 },
  //   { name: 'June', value: 8500 },
  //   { name: 'July', value: 10000 },
  //   { name: 'August', value: 9500 },
  //   { name: 'September', value: 7800 },
  //   { name: 'October', value: 9100 },
  //   { name: 'November', value: 8800 },
  //   { name: 'December', value: 9800 }
  // ];


  // view: [number, number] = [1200, 500];

  // colorScheme: Color = {
  //   domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA'],
  //   group: ScaleType.Ordinal,
  //   selectable: true,
  //   name: 'custom'
  // };
  //  // Correctly using LegendPosition enum value
  //  legendPosition: LegendPosition = LegendPosition.Right;



  //  view2: [number, number] = [700, 400]; // Width and height of the chart

  // cardData = [
  //   {
  //     "name": "Active Users",
  //     "value": 150
  //   },
  //   {
  //     "name": "Books Borrowed Today",
  //     "value": 45
  //   },
  //   {
  //     "name": "Books Available",
  //     "value": 1200
  //   }
  // ];

  // colorScheme2 = {
  //   domain: [ '#A10A28', '#C7B42C', '#AAAAAA'] // Customize the colors
  // };

  // view3: [number, number] = [700, 400]; // Width and height of the chart

  // // Data for the Line Chart
  // lineChartData = [
  //   {
  //     "name": "year 2023",
  //     "series": [
  //       { "name": "January", "value": 50 },
  //       { "name": "February", "value": 65 },
  //       { "name": "March", "value": 80 },
  //       { "name": "April", "value": 55 },
  //       { "name": "May", "value": 70 },
  //       { "name": "June", "value": 95 },
  //       { "name": "July", "value": 85 },
  //       { "name": "August", "value": 60 },
  //       { "name": "September", "value": 75 },
  //       { "name": "October", "value": 90 },
  //       { "name": "November", "value": 100 },
  //       { "name": "December", "value": 120 }
  //     ]
  //   },
  //   {
  //     "name": "year 2024",
  //     "series": [
  //         { "name": "January", "value": 0 },
  //         { "name": "February", "value": 0 },
  //         { "name": "March", "value": 0 },
  //         { "name": "April", "value": 0 },
  //         { "name": "May", "value": 70 },
  //         { "name": "June", "value": 95 },
  //         { "name": "July", "value": 0 },
  //         { "name": "August", "value": 0 },
  //         { "name": "September", "value": 0 },
  //         { "name": "October", "value": 0 },
  //         { "name": "November", "value": 0 },
  //         { "name": "December", "value": 0 }
  //     ]
  // }
  // ];

  // colorScheme3 = {
  //   domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA'] // Customize chart colors
  // };

  // // Options for animations and additional interactivity
  // gradient = false;
  // showLegend = false;
  // showXAxis = true;
  // showYAxis = true;
  // showXAxisLabel = true;
  // xAxisLabel = 'Month';
  // showYAxisLabel = true;
  // yAxisLabel = 'Books Borrowed';
  // autoScale = true;



  // new dash board -----------------------------------------------------------------------

  barData = [
    { name: 'Jan', value: 1000 },
    { name: 'Feb', value: 1500 },
    { name: 'March', value: 1700 },
    { name: 'Apirl', value: 1600 },
    { name: 'May', value: 1900 },
    { name: 'June', value: 1000 },
    { name: 'July', value: 1500 },
    { name: 'Aug', value: 1700 },
    { name: 'Sep', value: 1900 },
    { name: 'Oct', value: 1600 },
    { name: 'Nov', value: 1000 },
    { name: 'Dec', value: 5000 }
  ];

  // Data for Line Chart
  lineData = [
    {
      name: 'Revenue',
      series: [
        { name: 'Jan', value: 1000 },
        { name: 'Feb', value: 1500 },
        { name: 'March', value: 1700 },
        { name: 'Apirl', value: 1600 },
        { name: 'May', value: 1900 },
        { name: 'June', value: 1000 },
        { name: 'July', value: 1500 },
        { name: 'Aug', value: 1700 },
        { name: 'Sep', value: 1900 },
        { name: 'Oct', value: 1600 },
        { name: 'Nov', value: 1000 },
        { name: 'Dec', value: 5000 }
      ]
    }
  ];

  // Data for Pie Chart

  

  // pieData = [
  //   { name: 'All Users', value: 5000 },
  //   { name: 'Active Users', value: 3000 },
  //   { name: 'Non Active Users', value: 2000 },
  //   { name: 'Subscribed USers', value: 1000 }
  // ];

  // pieData = [
  //   { name: 'All Users', value: this.totalUsers },
  //   { name: 'Active Users', value: this.activeUsers },
  //   { name: 'Non Active Users', value: this.NonActiveUsers },
  //   { name: 'Subscribed USers', value: this.Subscribed }
  // ];


  updatePieData(): void {
    if (
      this.totalUsers !== null &&
      this.activeUsers !== null &&
      this.NonActiveUsers !== null &&
      this.Subscribed !== null
    )
     {

      this.nonsubscrivedUser=this.totalUsers-this.Subscribed


      this.pieData = [
        // { name: 'All Users', value: this.totalUsers },
        // { name: 'Active Users', value: this.activeUsers },
        { name: 'Non Subscribed Users', value: this.nonsubscrivedUser },
        { name: 'Subscribed Users', value: this.Subscribed },
      ];
    }
  }

  // range

  
  view: [number, number] = [700, 400];
  colorScheme: Color = {
    name: 'customScheme',
    selectable: true,
    group: ScaleType.Ordinal,
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA', '#1F77B4'],
  };

  // Sample Top 10 data


  top10Data = [
    { name: 'Item 1', value: 70 },
    { name: 'Item 2', value: 85 },
    { name: 'Item 3', value: 55 },
    { name: 'Item 4', value: 90 },
    { name: 'Item 5', value: 40 },
    { name: 'Item 6', value: 60 },
    { name: 'Item 7', value: 75 },
    { name: 'Item 8', value: 80 },
    { name: 'Item 9', value: 65 },
    { name: 'Item 10', value: 50 },
  ];

  // Chart data
  chartData = [...this.top10Data];

  // Update chart when range slider changes
  updateChart(): void {
    // Re-sort the data based on updated values
    this.chartData = [...this.top10Data].sort((a, b) => b.value - a.value);
  }


  // showing top books

  fetchTopAudiobooks(count: number): void {
    this.audiobookService.getTopAudiobooks(count).subscribe(
      (data) => {
        this.audiobooks = data; 
        console.log(this.audiobooks);
        
      },
      (error) => {
        console.error('Error fetching audiobooks:', error);
      }
    );
    
  }
  fetchTopAudiobookslist(count: number): void {
    this.audiobookService.getTopAudiobooks(count).subscribe(
      (data) => {
        this.audiobookslists=data
        console.log(this.audiobooks);
        
      },
      (error) => {
        console.error('Error fetching audiobooks:', error);
      }
    );
    
  }




  // books graphs

  appointmentData = [
    {
      name: 'No of Books',
      series: [
        { name: 'January', value: 230 },
        { name: 'February', value: 50 },
        { name: 'March', value: 40 },
        { name: 'April', value: 60 },
        { name: 'May', value: 80 },
        { name: 'June', value: 70 },
        { name: 'July', value: 60 },
        { name: 'August', value: 40 },
      ],
    },
    // {
    //   name: 'DATA TWO',
    //   series: [
    //     { name: 'January', value: 20 },
    //     { name: 'February', value: 40 },
    //     { name: 'March', value: 30 },
    //     { name: 'April', value: 50 },
    //     { name: 'May', value: 70 },
    //     { name: 'June', value: 60 },
    //     { name: 'July', value: 50 },
    //     { name: 'August', value: 30 },
    //   ],
    // },
  ];

  colorSchemes = {
    domain: ['#5AA454', '#C7B42C'],
  };

  // -----------------

  // data = [
  //   { name: 'All Audio Books', value: 40 },
  //   { name: 'Commonly Used Books', value: 35 },
  //   { name: 'Non-Used Books', value: 25 }
  // ];

  // views: [number, number] = [700, 400];
  // gradient: boolean = true;
  // showLegend: boolean = true;
  // showLabels: boolean = true;
  // isDoughnut: boolean = false;

  // -----------
  data = [
    { name: 'Frequently Listened', value: 40 },
    { name: 'Commonly Used Books', value: 35 },
    { name: 'Non-Used Books', value: 25 }
  ];

  viewed: [number, number] = [250, 200]; // Chart size

  // Chart options
  gradient: boolean = true;
  showLegend: boolean = false;
  showLabels: boolean = true;
  isDoughnut: boolean = true;

  
  
}
  


