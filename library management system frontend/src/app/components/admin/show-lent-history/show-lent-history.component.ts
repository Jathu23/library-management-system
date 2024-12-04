import { Component, OnInit } from '@angular/core';
import { RentService } from '../../../services/lent-service/rent.service';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';
import { UserService } from '../../../services/user-service/user.service';
import { environment } from '../../../../environments/environment.testing';

@Component({
  selector: 'app-show-lent-history',
  templateUrl: './show-lent-history.component.html',
  styleUrl: './show-lent-history.component.css'
})
export class ShowLentHistoryComponent implements OnInit {
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
  curentAdminId: number = 0;

  constructor(private rentservice: RentService, private lentService: RentService, private userservice: UserService, private bookservice: GetbooksService) { 
    const tokendata = environment.getTokenData();
    this.curentAdminId= Number(tokendata.ID);
    console.log(tokendata);
  }
  ngOnInit(): void {
    this.loadrecods();
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

  onReturnClick(rentId: any) {
    if (rentId && this.curentAdminId > 0) {
      this.isLoading = true;
      this.lentService.returnNormalbook(rentId, this.curentAdminId).subscribe(
        (response: any) => {
          if (response.success) {
            this.isLoading = false;
            alert(response.message);
            // this.searchQuery = "";
            // this.userInfo = '';
            // this.pendingBooks = []
          } else {
            this.isLoading = false;
            alert(response.message);
          }
        },
        (error) => {
          alert(error.error.message);
          console.log("error: ", error);
          this.isLoading = false;
        }
      );
    } else {
      console.log("fill all information");

    }
  }




  loadrecods() {

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
