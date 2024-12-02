import { Component, Input, OnInit } from '@angular/core';
import { RentService } from '../../../services/lent-service/rent.service';
import { UserService } from '../../../services/user-service/user.service';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';
import { environment } from '../../../../environments/environment.testing';

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
  searchQuery: string = '';
  suggestions: string[] = [];
  userInfo: any = null;
  pendingBooks: any[] = [];
  relatedTextArray: string[] = [];
  bookId: any;
  bookInfo: any = null;
  curentAdminId: number= 0;
  selectedDuesday: number = 2;
  duesdays: number[] = [1,2, 3, 4, 5, 6, 7, 10];

  constructor(private lentService: RentService, private userservice: UserService, private bookservice: GetbooksService) { 
    const tokendata = environment.getTokenData();
    this.curentAdminId= Number(tokendata.ID);
    console.log(tokendata);
  }

  onSearch() {
    if (this.searchQuery.trim().length > 0) {
      this.suggestions = [];
      this.userservice.GetUserEmailsByPrefix(this.searchQuery).subscribe(
        (response) => {
          if (response.success) {
            this.suggestions = response.data;
          }
        },
        (error) => {
          console.log("error", error);

        }
      );

    } else {
      this.suggestions = [];
      this.pendingBooks = [];
      this.userInfo = null;
    }

  }

  selectUsername(emailornic: string) {
    this.searchQuery = emailornic;
    this.suggestions = [];
    this.fetchUserInfo(emailornic);
  }
  fetchUserInfo(emailornic: string) {
    this.isLoading = true;
    this.userservice.GetUserByEmailorNic(emailornic).subscribe(
      (response) => {
        if (response.success) {

          this.userInfo = response.data;
          setTimeout(() => {
            this.fetchPendingBooks(this.userInfo.id);
            this.isLoading = false;
          }, 1000)

        } else {
          this.isLoading = false;
        }
      },
      (error) => {
        console.log("error", error);
        this.isLoading = false;
      }
    );
  }

  fetchPendingBooks(userid: number) {
    this.isLoading = true;
    this.lentService.getlentrecByuserid(userid).subscribe(
      (response) => {
        if (response.success) {
          this.isLoading = false;
          this.pendingBooks = response.data;
          console.log(this.pendingBooks);
        } else {
          this.isLoading = false;
        }
      },
      (error) => {
        console.log("error", error);
        this.isLoading = false;
      }
    );
  }

  fetchBookInfo() {
    if (!this.bookId) {
      this.bookInfo = null;
      return;
    } else {
      this.bookservice.getNoramlbookbyId(this.bookId).subscribe(
        (response) => {
          if (response.success) {
            this.bookInfo = response.data
            console.log(this.bookInfo);

          }
        },
        (error) => {
          console.log("error", error);
        }
      );
    }
  }


  onRentClick() {
   
    if (this.userInfo && this.bookId) {
      this.isLoading=true;
      this.lentService.rentnormalbookbycopyid(this.bookId, this.userInfo.id, this.curentAdminId, this.selectedDuesday).subscribe(
        (response:any) => {
          if (response.success) {
            this.isLoading=false;
            alert(response.message);
            this.searchQuery = "";
            this.bookId = "";
            this.bookInfo = '';
            this.userInfo = '';
            this.pendingBooks = []
          }else{
            this.isLoading=false;
            alert(response.message);
          }
        },
        (error) => {
          alert(error.error.message);
          console.log("error: ",error);
          this.isLoading=false;
        }
      );
    } else {
      console.log("fill all information");

    }
  }

  ngOnInit(): void {
    this.getallrentrecods();
    // this.fetchPendingBooks(2);
 
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