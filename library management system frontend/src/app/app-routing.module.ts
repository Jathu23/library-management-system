import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserDashboardComponent } from './components/user/user-dashboard/user-dashboard.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { LandingLayoutComponent } from './layouts/landing-layout/landing-layout.component';
import { UserLayoutComponent } from './layouts/user-layout/user-layout.component';
import { AddbookComponent } from './components/admin/addbook/addbook.component';
import { LoginComponent } from './components/landing/login/login.component';
import { AddebookComponent } from './components/admin/addebook/addebook.component';
import { AddAudiobookComponent } from './components/admin/add-audiobook/add-audiobook.component';
import { ShowbooksComponent } from './components/user/showbooks/showbooks.component';
import { ShowaudiobooksComponent } from './components/user/showaudiobooks/showaudiobooks.component';
import { ShowebooksComponent } from './components/user/showebooks/showebooks.component';
import { NewAdminComponent } from './components/admin/new-admin/new-admin.component';
import { ShowNormalbookComponent } from './components/admin/show-normalbook/show-normalbook.component';
import { ShowAudiobookComponent } from './components/admin/show-audiobook/show-audiobook.component';
import { ShowEbookComponent } from './components/admin/show-ebook/show-ebook.component';
import { ShowLentRecComponent } from './components/admin/show-lent-rec/show-lent-rec.component';



const routes: Routes = [
  {
    path: '',
    component: LandingLayoutComponent,
    children: [
      { path: 'login', component: LoginComponent },
    ]
  },
  {
    path: 'admin',
    component: AdminLayoutComponent,
    children: [
     
      { path: 'add-normal-book', component: AddbookComponent },
      { path: 'add-e-book', component: AddebookComponent },
      { path: 'add-audio-book', component: AddAudiobookComponent },
      {path:'add-admin',component:NewAdminComponent},
      {path:'show-normal-Books',component:ShowNormalbookComponent},
      {path:'show-audio-Books',component:ShowAudiobookComponent},
      {path:'show-e-Books',component:ShowEbookComponent},
      {path:'show-rent-rec',component:ShowLentRecComponent},

    ]
  },
  {
    path: 'user',
    component: UserLayoutComponent,
    children: [
      { path: '', component: UserDashboardComponent },
      { path: 'view-normal-books', component: ShowbooksComponent },
      { path: 'view-audio-books', component: ShowaudiobooksComponent },
      {path: 'view-e-books' , component:ShowebooksComponent }
   
    ]
  }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
