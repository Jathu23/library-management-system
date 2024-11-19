import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminDashboardComponent } from './components/admin/admin-dashboard/admin-dashboard.component';
import { ManageBooksComponent } from './components/admin/manage-books/manage-books.component';
import { ManageUsersComponent } from './components/admin/manage-users/manage-users.component';

import { UserDashboardComponent } from './components/user/user-dashboard/user-dashboard.component';


import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { LandingLayoutComponent } from './layouts/landing-layout/landing-layout.component';
import { UserLayoutComponent } from './layouts/user-layout/user-layout.component';

import { AddbookComponent } from './components/admin/addbook/addbook.component';


const routes: Routes = [
  {
    path: '',
    component: LandingLayoutComponent,
    children: [
      // { path: 'login', component: LoginComponent },
      
      
    ]
  },
  {
    path: 'admin',
    component: AdminLayoutComponent,
    children: [
      { path: '', component: AdminDashboardComponent },
      { path: 'manage-books', component: ManageBooksComponent },
      { path: 'manage-users', component: ManageUsersComponent },
      { path: 'add-normal-book', component: AddbookComponent },
    ]
  },
  {
    path: 'user',
    component: UserLayoutComponent,
    children: [
      { path: '', component: UserDashboardComponent }
   
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
