import { Component, OnInit } from '@angular/core';
import { SubscriptionService } from '../../../services/subscription-service/subscription.service';
import { environment } from '../../../../environments/environment.testing';

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
    durationId: null,
    method: ''
  }; // Data for the new subscription form
  total: number = 1888; // Total cost
  plans: any[] = []; // Available subscription plans
  durations: any[] = []; // Payment durations
  currentUserId: number = 1; // Current user ID

  constructor(private subscriptionService: SubscriptionService) {
    const tokendata = environment.getTokenData();
    this.currentUserId= Number(tokendata.ID);
    this.fetchPlansAndDurations();
  }

  ngOnInit(): void {
    this.fetchCurrentSubscription();
    this.fetchSubscriptionHistory();
   
  }

  fetchCurrentSubscription() {
    this.subscriptionService.getCurrentSubscriptionbyuser(this.currentUserId).subscribe(
      (data: any) => {
        this.currentSubscription = data[0] || null;
       console.log(this.currentSubscription);
       
      },
      (error) => {
        console.error('Error fetching current subscription:');
      }
    );
  }

  fetchSubscriptionHistory() {
    this.subscriptionService.getSubscriptionHistorybyuser(this.currentUserId).subscribe(
      (data: any[]) => {
        this.subscriptionHistory = data || [];
      },
      (error) => {
        console.error('Error fetching subscription history:');
      }
    );
  }

  fetchPlansAndDurations() {
    this.subscriptionService.getPlans().subscribe(
      (plans) => {
        this.plans = plans || [];
      },
      (error) => {
        console.error('Error fetching plans:', error);
      }
    );

    this.subscriptionService.getDurations().subscribe(
      (durations) => {
        this.durations = durations || [];
      },
      (error) => {
        console.error('Error fetching durations:', error);
      }
    );
  }

  findTotal(): void {
    let basePrice = 0;
    let multiplier = 0;

    // Find the selected plan price
    const selectedPlan = this.plans.find(plan => plan.id == this.newSubscription.planId);
    if (selectedPlan) {
      basePrice = selectedPlan.price;
    }
    // Find the selected duration multiplier
    const selectedDuration = this.durations.find(duration => duration.id == this.newSubscription.durationId);
    if (selectedDuration) {
      multiplier = selectedDuration.multiplier;
    }
  
    // Calculate the total price
setTimeout(() => {
  this.total = basePrice * multiplier;

}, 100);

  
  }
  
  subscribe() {
    if (!this.newSubscription.planId || !this.newSubscription.durationId || !this.newSubscription.method) {
      alert('Please select a plan, duration, and payment method.');
      return;
    }
    this.subscriptionService
      .subscribeplan(
        this.currentUserId,
        this.newSubscription.planId,
        this.newSubscription.durationId,
        this.newSubscription.method
      )
      .subscribe(
        (response) => {
          alert('Subscription successful!');
          this.fetchCurrentSubscription(); // Refresh current subscription
          this.fetchSubscriptionHistory(); // Refresh history
        },
        (error) => {
          console.error('Subscription failed:', error);
          alert('Failed to subscribe. Please try again.');
        }
      );
  }
}
