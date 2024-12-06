import { Component, OnInit } from '@angular/core';
import { SubscriptionService } from '../../../services/subscription-service/subscription.service';


@Component({
  selector: 'app-subscription',
  templateUrl: './subscription.component.html',
  styleUrls: ['./subscription.component.css']
})
export class SubscriptionComponent implements OnInit {
  currentSubscription: any = null; // Current subscription details
  subscriptionHistory: any[] = []; // History of subscriptions
  remainingTime: string = ''; // Remaining time for the current subscription
  showSubscriptionForm: boolean = false; // Toggle for subscription form
  newSubscription: any = {
    planId: null,
    durationId: null
  }; // Data for the new subscription form
  plans: any[] = []; // Available subscription plans
  durations: any[] = []; // Payment durations

  constructor(private subscriptionService: SubscriptionService) {}

  ngOnInit(): void {
    // this.fetchCurrentSubscription();
    // this.fetchSubscriptionHistory();
    this.fetchPlansAndDurations();
  }

  fetchCurrentSubscription() {
    this.subscriptionService.getCurrentSubscription().subscribe((data: any) => {
      this.currentSubscription = data;
      if (data) {
        const endDate = new Date(data.endDate);
        const now = new Date();
        const diff = endDate.getTime() - now.getTime();
        if (diff > 0) {
          const days = Math.floor(diff / (1000 * 60 * 60 * 24));
          const hours = Math.floor((diff % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
          this.remainingTime = `${days} days ${hours} hours remaining`;
        } else {
          this.remainingTime = 'Expired';
        }
      }
    });
  }

  fetchSubscriptionHistory() {
    this.subscriptionService.getSubscriptionHistory().subscribe((data: any[]) => {
      this.subscriptionHistory = data;
    });
  }

  fetchPlansAndDurations() {
    this.subscriptionService.getPlans().subscribe((plans) => {
      this.plans = plans;
    
    });
    this.subscriptionService.getDurations().subscribe((durations) => {
      this.durations = durations;
    });
  }

  toggleSubscriptionForm() {
    this.showSubscriptionForm = !this.showSubscriptionForm;
  }

  subscribe() {
    if (this.newSubscription.planId && this.newSubscription.durationId) {
      this.subscriptionService
        .subscribe(this.newSubscription.planId, this.newSubscription.durationId)
        .subscribe(() => {
          this.fetchCurrentSubscription();
          this.fetchSubscriptionHistory();
          this.showSubscriptionForm = false;
          alert('Subscription successful!');
        });
    } else {
      alert('Please select a plan and duration.');
    }
  }
}
