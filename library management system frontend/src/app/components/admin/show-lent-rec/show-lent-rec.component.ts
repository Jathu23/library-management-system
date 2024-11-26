import { Component, OnInit } from '@angular/core';
import { RentService } from '../../../services/lent-service/rent.service';

@Component({
  selector: 'app-show-lent-rec',
  templateUrl: './show-lent-rec.component.html',
  styleUrls: ['./show-lent-rec.component.css']
})
export class ShowLentRecComponent implements OnInit {
  lentRecords: any[] = [];
  isLoading: boolean = false; 
  errorMessage: string = '';
  expandedElementId: number | null = null;
  selectedRecord: any | null = null;
  searchQuery: string = ''; // Query entered by the user
  suggestions: string[] = []; // Array of suggestions
  userInfo: any = null; // Selected user information
  pendingBooks: any[] = []; // Pending books information
  relatedTextArray: string[] = []; // Array of related usernames fetched from the API

  constructor(private lentService: RentService) {}

  onSearch(){
    
    if(this.searchQuery.trim().length > 1){
      this.suggestions=[];
      this.suggestions.push("jathu","thuva")
    }else
    {
      this.suggestions = [];
      this.pendingBooks = []; 
      this.userInfo = null; 
    }
   
  }
  selectUsername(username: string) {
    this.searchQuery = username;
    this.suggestions = []; 
    this.fetchUserInfo(username); 
    this.fetchPendingBooks(username);
  }
  fetchUserInfo(username: string) {
    this.userInfo =  {
      "id": 1,
      "userNic": "1",
      "firstName": "Esvaran",
      "lastName": "Jathushan",
      "fullName": "Esvaran Jathushan",
      "email": "jathushanj@gmail.com",
      "phoneNumber": "0769155204",
      "address": "No-298 punnaineeravi visuvamadu",
      "profileImage": "defaultimg.jpg",
      "registrationDate": "2024-11-25T21:38:06.1939206",
      "isActive": true,
      "isSubscribed": false
    }
}
fetchPendingBooks(username: string) {
  this.isLoading = true; // Show loading spinner

setTimeout(() => {
  this.pendingBooks = [
    {
      "bookId": 101,
      "title": "The Great Gatsby",
      "dueDate": "2024-12-05T10:00:00Z",
      "author": "F. Scott Fitzgerald",
      "status": "Overdue"
    },
    {
      "bookId": 102,
      "title": "To Kill a Mockingbird",
      "dueDate": "2024-11-30T15:00:00Z",
      "author": "Harper Lee",
      "status": "Due Soon"
    },
    {
      "bookId": 103,
      "title": "1984",
      "dueDate": "2024-12-01T09:00:00Z",
      "author": "George Orwell",
      "status": "Due Soon"
    }
  ];

  this.isLoading=false;
},2000)

}


onRentClick() {
  console.log(`Rent button clicked for username: ${this.userInfo?.fullName}`);
}

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

  toggleRow(recordId: number): void {
    this.expandedElementId = this.expandedElementId === recordId ? null : recordId;
  }

  openModal(record: any): void {
    this.selectedRecord = record;
  }

  closeModal(): void {
    this.selectedRecord = null;
  }
}
