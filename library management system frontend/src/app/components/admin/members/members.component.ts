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
  pageSize = 10;
  totalItems = 0;
  users: any[] = [];
  heading: string = 'Active Members';
  selectedOption: string = 'active';
  searchQuery: string = '';

  apiEndpoints: { [key: string]: string } = {
    active: 'getActiveUsers',
    subscribed: 'getSubscribeUsers',
    nonActive: 'getNonActiveUsers',
    search: 'getsearchUsers'
  };

  constructor(private viewmemberService: ViewmembersService) {}

  ngOnInit(): void {
    this.fetchUserDetails();
  }

  fetchUserDetails(): void {
    if (this.isLoading) return;

    this.isLoading = true;

    if (this.selectedOption === 'search' && this.searchQuery.trim() === '') {
      this.isLoading = false;
      return;
    }

    let apiCall;

    switch (this.selectedOption) {
      case 'active':
        apiCall = this.viewmemberService.getActiveUsers(this.currentPage, this.pageSize);
        break;

      case 'subscribed':
        apiCall = this.viewmemberService.getSubscribeUsers(this.currentPage, this.pageSize);
        break;

      case 'nonActive':
        apiCall = this.viewmemberService.getNonActiveUsers(this.currentPage, this.pageSize);
        break;

      case 'search':
        if (this.searchQuery.trim() !== '') {
          apiCall = this.viewmemberService.getsearchUsers(
            this.currentPage,
            this.pageSize,
            this.searchQuery
          );
        }
        break;

      default:
        console.error('Invalid option selected');
        this.isLoading = false;
        return;
    }

    if (apiCall) {
      apiCall.subscribe(
        (response: any) => {
          console.log('API Response:', response); 
          const result = response.data;
          this.users = result.items;
          this.totalItems = result.totalCount;
          this.heading = this.getHeading(this.selectedOption);
          this.isLoading = false;
        },
        (error: any) => {
          console.error('API Error:', error); 
          this.isLoading = false;
        }
      );
    } else {
      this.isLoading = false;
    }
  }

  getHeading(option: string): string {
    switch (option) {
      case 'active': return 'Active Members';
      case 'subscribed': return 'Subscribed Members';
      case 'nonActive': return 'Non-Active Members';
      case 'search': return 'Search Results';
      default: return 'Members';
    }
  }

  onOptionChange(option: string): void {
    this.selectedOption = option;
    this.currentPage = 1;
    this.fetchUserDetails();
  }

  onPageChange(event: any): void {
    const { pageIndex, pageSize } = event;
    this.currentPage = pageIndex + 1;
    this.pageSize = pageSize;
    this.fetchUserDetails();
  }

  activateUser(userId: number): void {
    if (this.isLoading) return;

    this.isLoading = false;
    this.viewmemberService.activateUser(userId).subscribe(
      (response) => {
        console.log('User activation response:', response); 
        alert('User activated successfully!');
        this.fetchUserDetails();
      },
      (error) => {
        console.error('Error activating user:', error); 
        alert('Failed to activate user. Please try again.');
        this.isLoading = false;
      }
    );
  }

  searchUsers(): void {
    this.selectedOption = 'search';
    this.currentPage = 1;
    this.fetchUserDetails();
  }
}
