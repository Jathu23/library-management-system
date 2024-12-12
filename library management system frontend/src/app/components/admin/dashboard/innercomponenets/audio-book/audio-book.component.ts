import { Component,Input,OnInit  } from '@angular/core';
import { Color, LegendPosition, ScaleType } from '@swimlane/ngx-charts';
// import { UserService } from '../../.././../services/user-service/user.service';
import { UserService } from '../../.././../../services/user-service/user.service';


// import {AudiobookService} from '../../../services/bookservice/audiobook.service'
import {AudiobookService} from '../../../../../services/bookservice/audiobook.service'




@Component({
  selector: 'app-audio-book',
  templateUrl: './audio-book.component.html',
  styleUrl: './audio-book.component.css'
})
export class AudioBookComponent {
  errorMessage: string = '';
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

  // showing the top books....

  audiobooks: any[] = []; 
  audiobookslists: any[] = []; 


 ngOnInit(): void {
  this.loadUserCounts();
  this.fetchTopAudiobooks(3);
  this.fetchTopAudiobookslist(10)
  this.fetchUsers(10);
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

  ];

  colorSchemes = {
    domain: ['#5AA454', '#C7B42C'],
  };



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


  // -------------------------------------------------------------------------------------------
  
  chartView: [number, number] = [150, 150]; // Chart size (width x height)
  
  // Corrected color scheme
  chartColorScheme: Color = {
    name: 'customScheme',
    selectable: true,
    group: ScaleType.Ordinal,
    domain: ['#5AA454', '#C7B42C', '#A10A28'], // Colors
  };

  // Product statistics data
  productStatistics = [
    {
      name: 'Electronic',
      value: 2487,
      color: '#5AA454',
      change: 1.8, // Percentage change
    },
    {
      name: 'Games',
      value: 1828,
      color: '#C7B42C',
      change: 2.3,
    },
    {
      name: 'Furniture',
      value: 1463,
      color: '#A10A28',
      change: -1.04,
    },
  ];

  // ----- SubAnd Bset

  users: any[] = [];

  fetchUsers(count: number): void {
    this.userService.getSubscribedAndBestUsers(count).subscribe(
      (response) => {
        this.users = response;
        console.log("ados");
        
        console.log(this.users);
        
      },
      (error) => {
        this.errorMessage = 'Failed to load users';
        console.error('Error fetching users:', error);
      }
    );
  }



}
