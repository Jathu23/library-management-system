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
import { ShowLentHistoryComponent } from './components/admin/show-lent-history/show-lent-history.component';
import { MembersComponent } from './components/admin/members/members.component';
import { ShowLentRecodesComponent } from './components/user/show-lent-recodes/show-lent-recodes.component';
import { ShowLendingHistoryComponent } from './components/user/show-lending-history/show-lending-history.component';
import { UserprofileComponent } from './components/user/userprofile/userprofile.component';
import { authGuard } from './guards/auth.guard';
import { DashboardComponent } from './components/admin/dashboard/dashboard.component';
import { LockScreenComponent } from './components/lock-screen/lock-screen.component';
import { SubscriptionComponent } from './components/user/subscription/subscription.component';
import { StaticsComponent } from './components/admin/statics/statics.component';
import { NotificationComponent } from './components/user/notification/notification.component';
import { NormalBooksComponent } from './components/admin/dashboard/innercomponenets/normal-books/normal-books.component';
import { AudioBookComponent } from './components/admin/dashboard/innercomponenets/audio-book/audio-book.component';
import { EBookComponent } from './components/admin/dashboard/innercomponenets/ebook/ebook.component';


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
    canActivate:[authGuard],
    data:{roles:["Admin"]},
    children: [
      { path: '', component: DashboardComponent },
      { path: 'lock', component: LockScreenComponent },
      { path: 'add-normal-book', component: AddbookComponent },
      { path: 'add-e-book', component: AddebookComponent },
      { path: 'add-audio-book', component: AddAudiobookComponent },
      {path:'add-admin',component:NewAdminComponent},
      {path:'show-normal-Books',component:ShowNormalbookComponent},
      {path:'show-audio-Books',component:ShowAudiobookComponent},
      {path:'show-e-Books',component:ShowEbookComponent},
      {path:'show-rent-rec',component:ShowLentRecComponent},
      {path:'show-rent-his',component:ShowLentHistoryComponent},
      {path:'members',component:MembersComponent},
      { path: 'dashboard', component: DashboardComponent, children: [
        { path: '', redirectTo: 'audio-books', pathMatch: 'full' },
        { path: 'audio-books', component: AudioBookComponent },
        { path: 'normal-books', component: NormalBooksComponent },
        { path: 'e-books', component: EBookComponent },
      ] 
    },
      {path:'statics',component:StaticsComponent},
    ]
  },
  {
    path: 'user',
    component: UserLayoutComponent,
    canActivate:[authGuard],
    data:{roles:['user']},
    children: [
      { path: '', component: UserDashboardComponent },
      { path: 'view-normal-books', component: ShowbooksComponent },
      { path: 'view-audio-books', component: ShowaudiobooksComponent },
      {path: 'view-e-books' , component:ShowebooksComponent },
      {path: 'view-lend-records' , component:ShowLentRecodesComponent },
      {path: 'view-lend-history' , component:ShowLendingHistoryComponent },
      {path:'user-profile',component:UserprofileComponent},
      {path:'subscription-u',component:SubscriptionComponent},
      {path:'notification-u',component:NotificationComponent}
    ]
  }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
