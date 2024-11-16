import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminDashboardComponent } from './components/admin/admin-dashboard/admin-dashboard.component';
import { ManageBooksComponent } from './components/admin/manage-books/manage-books.component';
import { ManageUsersComponent } from './components/admin/manage-users/manage-users.component';
import { LandingComponent } from './components/landing/landing.component';
import { UserDashboardComponent } from './components/user/user-dashboard/user-dashboard.component';
import { ViewBooksComponent } from './components/user/view-books/view-books.component';
import { ViewOverdueBooksComponent } from './components/user/view-overdue-books/view-overdue-books.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { LandingLayoutComponent } from './layouts/landing-layout/landing-layout.component';
import { UserLayoutComponent } from './layouts/user-layout/user-layout.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  {
    path: '',
    component: LandingLayoutComponent,
    children: [
      { path: 'login', component: LandingComponent },
      {path:'register',component:RegisterComponent},
      {path:'home',component:HomeComponent}
      
    ]
  },
  {
    path: 'admin',
    component: AdminLayoutComponent,
    children: [
      { path: '', component: AdminDashboardComponent },
      { path: 'manage-books', component: ManageBooksComponent },
      { path: 'manage-users', component: ManageUsersComponent }
    ]
  },
  {
    path: 'user',
    component: UserLayoutComponent,
    children: [
      { path: '', component: UserDashboardComponent },
      { path: 'view-books', component: ViewBooksComponent },
      { path: 'view-overdue-books', component: ViewOverdueBooksComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
