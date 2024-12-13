import { Component, OnInit } from '@angular/core';
import { ViewmembersService } from '../../../services/admin-services/viewmembers.service';
import { environment } from '../../../../environments/environment.testing';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})
export class MembersComponent implements OnInit {
  isLoading = false;
  currentPage = 1;
  pageSize = 10; // Default page size
  totalItems = 0;
  users: any[] = [];
  heading: string = 'Active Members';
  selectedOption: string = 'active';
  searchQuery: string = '';
resoursBase = environment.resourcBaseUrl;
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
this.users=[];
    // Handle empty search query for search option
    if (this.selectedOption === 'search' && !this.searchQuery.trim()) {
      this.isLoading = false;
      alert('Please enter a valid search query.');
      return;
    }

    const apiCallMap: { [key: string]: () => any } = {
      active: () => this.viewmemberService.getActiveUsers(this.currentPage, this.pageSize),
      subscribed: () => this.viewmemberService.getSubscribeUsers(this.currentPage, this.pageSize),
      nonActive: () => this.viewmemberService.getNonActiveUsers(this.currentPage, this.pageSize),
      search: () =>
        this.viewmemberService.getsearchUsers(this.currentPage, this.pageSize, this.searchQuery)
    };

    const apiCall = apiCallMap[this.selectedOption]();

    if (apiCall) {
      apiCall.subscribe(
        (response: any) => {
          console.log('API Response:', response);
          const result = response.data;
          this.users = result.items || [];
          this.totalItems = result.totalCount || 0;
          this.heading = this.getHeading(this.selectedOption);
          this.isLoading = false;
        },
        (error: any) => {
          console.error('API Error:', error);
          alert('No data or Failed to fetch data. Please try again.');
          this.isLoading = false;
        }
      );
    } else {
      this.isLoading = false;
      console.error('Invalid API call.');
    }
  }

  getHeading(option: string): string {
    switch (option) {
      case 'active':
        return 'Active Members';
      case 'subscribed':
        return 'Subscribed Members';
      case 'nonActive':
        return 'Non-Active Members';
      case 'search':
        return 'Search Results';
      default:
        return 'Members';
    }
  }

  onOptionChange(option: string): void {
    this.selectedOption = option;
    this.currentPage = 1; // Reset to the first page
    this.fetchUserDetails();
  }

  onPageChange(event: any): void {
    const { pageIndex, pageSize } = event;
    this.currentPage = pageIndex + 1; // Angular Material paginator is 0-based
    this.pageSize = pageSize;
    this.fetchUserDetails();
  }

  activateUser(userId: number): void {
    if (this.isLoading) return;

    this.isLoading = true;
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
    this.currentPage = 1; // Reset to the first page for search
    this.fetchUserDetails();
  }
}
