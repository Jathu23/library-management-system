import { Component, OnInit } from '@angular/core';
import { ViewmembersService } from '../../../services/admin-services/viewmembers.service';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})
export class MembersComponent implements OnInit {
  isLoading = false;
  currentPage = 1;
  pageSize = 2;
  totalItems = 0;
  activeUsers: any[] = [];
  subscribedUsers: any[] = [];
  nonActiveUsers: any[] = [];
  searchResults: any[] = []; 
  heading: string = 'Active Members'; 
  searchQuery: string = ''; 

  constructor(private viewmemberService: ViewmembersService) {}

  ngOnInit(): void {
    
    this.loadActiveUsers();
  }

  
  loadActiveUsers() {
    if (this.isLoading) return;

    
    this.activeUsers = [];
    this.isLoading = true;
    this.heading = 'Active Members'; 

    this.viewmemberService.getActiveUsers(this.currentPage, this.pageSize).subscribe(
      (response) => {
        const result = response.data;
        this.activeUsers = result.items;
        this.totalItems = result.totalCount;
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching active users:', error);
        this.isLoading = false;
      }
    );
  }

  
  loadSubscribedUsers() {
    if (this.isLoading) return;

    this.subscribedUsers = [];
    this.isLoading = true;
    this.heading = 'Subscribed Members';  

    this.viewmemberService.getSubscribeUsers(this.currentPage, this.pageSize).subscribe(
      (response) => {
        const result = response.data;
        this.subscribedUsers = result.items;
        this.totalItems = result.totalCount;
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching subscribed users:', error);
        this.isLoading = false;
      }
    );
  }

  
  loadNonActiveUsers() {
    if (this.isLoading) return;

    this.nonActiveUsers = [];
    this.isLoading = true;
    this.heading = 'Non-Active Members';  

    this.viewmemberService.getNonActiveUsers(this.currentPage, this.pageSize).subscribe(
      (response) => {
        const result = response.data;
        this.nonActiveUsers = result.items;
        this.totalItems = result.totalCount;
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching non-active users:', error);
        this.isLoading = false;
      }
    );
  }



  
 
  searchUsers(searchQuery: string) {
    if (searchQuery.trim() === '') {
      
      this.loadActiveUsers();
    } else {
      
      this.isLoading = true;
      this.heading = 'Search Results';  

      this.viewmemberService.getsearchUsers(this.currentPage, this.pageSize, searchQuery).subscribe(
        (response) => {
          const result = response.data;
          this.searchResults = result.items;
          this.totalItems = result.totalCount;
          this.isLoading = false;
        },
        (error) => {
          console.error('Error searching users:', error);
          this.isLoading = false;
        }
      );
    }
  }

  activateUser(id: number) {
    if (this.isLoading) return;
  
    this.isLoading = false;
    this.viewmemberService.activateUser(id).subscribe(
      () => {
        this.loadNonActiveUsers();
        alert('User activated successfully!');
       
        this.isLoading = true;
      },
      (error) => {
        console.error('Error activating user:', error);
        alert('Failed to activate user. Please try again.');
        this.isLoading = false;
      }
      
    );
  }
  
  
  loadNextPage() {
    if (this.currentPage * this.pageSize < this.totalItems) {
      this.currentPage++;
      if (this.heading === 'Active Members') {
        this.loadActiveUsers();
      } else if (this.heading === 'Subscribed Members') {
        this.loadSubscribedUsers();
      } else if (this.heading === 'Non-Active Members') {
        this.loadNonActiveUsers();
      } else if (this.heading === 'Search Results') {
        this.searchUsers(this.searchQuery); 
      }
    }
  }

  
  loadPreviousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      if (this.heading === 'Active Members') {
        this.loadActiveUsers();
      } else if (this.heading === 'Subscribed Members') {
        this.loadSubscribedUsers();
      } else if (this.heading === 'Non-Active Members') {
        this.loadNonActiveUsers();
      } else if (this.heading === 'Search Results') {
        this.searchUsers(this.searchQuery); 
      }
    }
  }
}
