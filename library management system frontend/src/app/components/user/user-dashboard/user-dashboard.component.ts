import { Component, OnDestroy, OnInit } from '@angular/core';

import { AudiobookService } from '../../../services/bookservice/audiobook.service'

import { GetbooksService } from '../../../services/bookservice/getbooks.service'

import { UserService } from '../../../services/user-service/user.service';
import { Color, ScaleType } from '@swimlane/ngx-charts';










@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrl: './user-dashboard.component.css'
})
export class UserDashboardComponent implements OnInit, OnDestroy {
  errorMessage:string='';
  audiobookslists: any[]=[];

  constructor(
    private audiobookService: AudiobookService,
    private EbookService: GetbooksService,
    private userService: UserService,


  ) {

  }


  ngOnDestroy(): void {

    if (this.autoSlideInterval) {
      clearInterval(this.autoSlideInterval);  // Clear the interval when the component is destroyed
    }
  }

  audiobooks: any[] = [];
  Ebooks: any[] = [];
  imgageBaseUrl: string = `https://localhost:7261/`

  // \
  

  normalBookCount: number | null = null;
  normalAudioCount: number | null = null;
  normalUserCount: number | null = null;
  normalEBookCount: number | null = null;




  totalLendings:string='';
  pendings:string='';
  ontime:string='';
  later:string='';

  pendingLists:any[]=[];


  ngOnInit(): void {
    this.fetchTopAudiobooks(5)
    this.fetchTopEbooks(5)
    this.startAutoSlide();

    this.loadUserCounts();
    this.fetchTopAudiobooks(3);
    this.fetchTopAudiobookslist(10)
    this.fetchUsers(10);

    this.fetchNormalBookCount()
    this.fetchAudiobookCount()
    this.fetchUserBookCount()
    this.fetchEbookCount()

    // --

    const userId = 1; // Replace with the desired user ID
    this.userService.getLentReportByUserId(userId).subscribe({
      next: (data) => {
        this.totalLendings=data.totalRengings;
        this.pendings=data.totalRengings;
        this.ontime=data.totalRengings;
        this.later=data.totalRengings;


        this.pendingLists=data.pendingLent;


        console.log('Lent Report Data:', data);
        this.lentReport = data; // Assign data to the component property
      },
      error: (err) => {
        console.error('Error fetching Lent Report:', err);
      },
    });
  }

  fetchNormalBookCount(): void {
    this.EbookService.getNormalBookCount().subscribe(
      response => {
        this.normalBookCount = response.Count;
        console.log('Normal book count:', this.normalBookCount);
      },
      error => {
        console.error('Error fetching normal book count:', error);
      }
    );
  }

  fetchAudiobookCount(): void {
    this.audiobookService.getAudiobookCount().subscribe(
      response => {
        this.normalAudioCount = response.count;
        console.log('Audiobook count:', this.normalAudioCount);
      },
      error => {
        console.error('Error fetching audiobook count:', error);
      }
    );
  }

  fetchEbookCount(): void {
    this.EbookService.getEbookCount().subscribe(
      response => {
        this.normalEBookCount = response.count;
        console.log('Ebook count:', this.normalEBookCount);
      },
      error => {
        console.error('Error fetching ebook count:', error);
      }
    );
  }


  fetchUserBookCount(): void {
    this.userService.getUserBookCount().subscribe(
      response => {
        this.normalUserCount = response.count;
        console.log('User book count:', this.normalUserCount);
      },
      error => {
        console.error('Error fetching user book count:', error);
      }
    );
  }

  // fetchTopAudiobooks(count: number): void {
  //   this.audiobookService.getTopAudiobooks(count).subscribe(
  //     (data) => {
  //       this.audiobooks = data;
  //       console.log(this.audiobooks);

  //     },
  //     (error) => {
  //       console.error('Error fetching audiobooks:', error);
  //     }
  //   );

  // }

  // fubctions to fetch top e books
  fetchTopEbooks(count: number): void {
    this.EbookService.getTopEbooks(count).subscribe(
      (data) => {
        this.Ebooks = data;
        console.log("Ebook" + this.Ebooks);

      },
      (error) => {
        console.error('Error fetching audiobooks:', error);
      }
    );

  }

  currentIndex = 0;
  autoSlideInterval: any;

  moveSlide(direction: number) {
    const totalBooks = this.audiobooks.length;
    this.currentIndex = (this.currentIndex + direction + totalBooks) % totalBooks;

    // Update the carousel position (to show the right book)
    const carouselContainer = document.querySelector('.carousel-container') as HTMLElement;
    carouselContainer.style.transform = `translateX(-${this.currentIndex * 100}%)`;  // Move the container based on the current index
  }

  // Function to start automatic sliding
  startAutoSlide() {
    this.autoSlideInterval = setInterval(() => {
      this.moveSlide(1);  // Move to the next slide every 5 seconds
    }, 3000);  // 5000 ms = 5 seconds
  }

  // Pause automatic slide when mouse is over the carousel
  pauseAutoSlide() {
    clearInterval(this.autoSlideInterval);
  }

  // Resume automatic slide when mouse leaves the carousel
  resumeAutoSlide() {
    this.startAutoSlide();
  }


  // --------------------------------

  pieData: { name: string; value: number }[] = [];
  totalUsers: number | null = null;
  activeUsers: number | null = null;
  NonActiveUsers: number | null = null;
  Subscribed: number | null = null;
  nonsubscrivedUser: number | null = null;

  
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


  // ---------------------------------------------
  
  
  lentReport: any;





}
