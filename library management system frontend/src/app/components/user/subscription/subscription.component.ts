import { Component, OnInit } from '@angular/core';
import { SubscriptionService } from '../../../services/subscription-service/subscription.service';
import { ChangeDetectorRef } from '@angular/core';
import { environment } from '../../../../environments/environment.testing';

@Component({
  selector: 'app-subscription',
  templateUrl: './subscription.component.html',
  styleUrls: ['./subscription.component.css'],
})
export class SubscriptionComponent implements OnInit {
  currentSubscription: any = null; // Current subscription details
  subscriptionHistory: any[] = []; // History of subscriptions
  newSubscription: any = {
    planId: null,
    durationId: null,
    method: null,
  }; // Data for the new subscription form
  total: number = 0; // Total cost
  plans: any[] = []; // Available subscription plans
  durations: any[] = []; // Payment durations
  currentUserId: number = 0; // Current user ID

  constructor(
    private subscriptionService: SubscriptionService,
    private cdr: ChangeDetectorRef
  ) {
    const tokendata = environment.getTokenData();
    this.currentUserId= Number(tokendata.ID);
  }

  ngOnInit(): void {
    this.fetchCurrentSubscription();
    this.fetchSubscriptionHistory();
    this.fetchPlansAndDurations();
  }

  fetchCurrentSubscription() {
    this.subscriptionService.getCurrentSubscriptionbyuser(this.currentUserId).subscribe(
      (data: any) => {
        this.currentSubscription = data[0] || null;
      },
      (error) => {
        console.error('Error fetching current subscription:', error);
      }
    );
  }

  fetchSubscriptionHistory() {
    this.subscriptionService.getSubscriptionHistorybyuser(this.currentUserId).subscribe(
      (data: any[]) => {
        this.subscriptionHistory = data || [];
      },
      (error) => {
        console.error('Error fetching subscription history:', error);
      }
    );
  }

  fetchPlansAndDurations() {
    this.subscriptionService.getPlans().subscribe(
      (plans) => {
        this.plans = plans || [];
        plans.shift();
        this.cdr.detectChanges(); // Ensure view updates
      },
      (error) => {
        console.error('Error fetching plans:', error);
      }
    );

    this.subscriptionService.getDurations().subscribe(
      (durations) => {
        this.durations = durations || [];
        this.cdr.detectChanges(); // Ensure view updates
      },
      (error) => {
        console.error('Error fetching durations:', error);
      }
    );
  }

  findTotal(): void {
    const selectedPlan = this.plans.find((plan) => plan.id == this.newSubscription.planId);
    const selectedDuration = this.durations.find(
      (duration) => duration.id == this.newSubscription.durationId
    );

    const basePrice = selectedPlan ? selectedPlan.price : 0;
    const multiplier = selectedDuration ? selectedDuration.multiplier : 0;

    this.total = basePrice * multiplier;
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
        () => {
          alert('Subscription successful!');
          this.fetchCurrentSubscription();
          this.fetchSubscriptionHistory();
        },
        (error) => {
          console.error('Subscription failed:', error);
          alert('Failed to subscribe. Please try again.');
        }
      );
  }
}
