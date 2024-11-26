import { Component, OnInit } from '@angular/core';
import { RentService } from '../../../services/lent-service/rent.service';

@Component({
  selector: 'app-show-lent-history',
  templateUrl: './show-lent-history.component.html',
  styleUrl: './show-lent-history.component.css'
})
export class ShowLentHistoryComponent implements OnInit{
  isLoading = false;
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;
   lenthistorys: any[] = [];
   expandedElementId: number | null = null;
   searchQuery: string = ''; 
  suggestions: string[] = [];
  userInfo: any = null; 
  pendingBooks: any[] = []; 
  relatedTextArray: string[] = []; 

   constructor(private rentservice:RentService) {}
  ngOnInit(): void {
    this.loadEbooks();
  }

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
    this.fetchPendingBooks(username);
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
  onReturnClick(rentId:any) {
    console.log(`Rent button clicked for username: ${this.userInfo?.fullName}`);
    console.log(`Rent button clicked for bookid: ${rentId}`);

  }
   loadEbooks() {

    if (this.isLoading) return;

    this.isLoading = true;
    this.rentservice.getrenthistory(this.currentPage, this.pageSize).subscribe(
      (response) => {
        if (response.success) {
          const result = response.data;

          this.lenthistorys = [...this.lenthistorys, ...result.items];
          this.totalItems = result.totalCount;
          this.currentPage++;
          this.isLoading = false;
          console.log(this.lenthistorys);
          
        }
       
      },
      (error) => {
        console.error('Error fetching recods:', error);
        this.isLoading = false;
      }
    );
  }

  toggleRow(id: number): void {
    this.expandedElementId = this.expandedElementId === id ? null : id;
  }

}
