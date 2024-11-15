import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { HttpClientModule } from '@angular/common/http';
import { LandingLayoutComponent } from './layouts/landing-layout/landing-layout.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { UserLayoutComponent } from './layouts/user-layout/user-layout.component';
import { LandingComponent } from './components/landing/landing.component';
import { AdminDashboardComponent } from './components/admin/admin-dashboard/admin-dashboard.component';
import { ManageBooksComponent } from './components/admin/manage-books/manage-books.component';
import { ManageUsersComponent } from './components/admin/manage-users/manage-users.component';
import { UserDashboardComponent } from './components/user/user-dashboard/user-dashboard.component';
import { ViewBooksComponent } from './components/user/view-books/view-books.component';
import { ViewOverdueBooksComponent } from './components/user/view-overdue-books/view-overdue-books.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    LandingLayoutComponent,
    AdminLayoutComponent,
    UserLayoutComponent,
    LandingComponent,
    AdminDashboardComponent,
    ManageBooksComponent,
    ManageUsersComponent,
    UserDashboardComponent,
    ViewBooksComponent,
    ViewOverdueBooksComponent,
   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
