import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddbookComponent } from './components/admin/addbook/addbook.component';
import { UserDashboardComponent } from './components/user/user-dashboard/user-dashboard.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { LandingLayoutComponent } from './layouts/landing-layout/landing-layout.component';
import { UserLayoutComponent } from './layouts/user-layout/user-layout.component';
import { AboutusComponent } from './layouts/landing-layout/aboutus/aboutus.component';
import { UsersignupComponent } from './components/landing/usersignup/usersignup.component';
import { LoginComponent } from './components/landing/login/login.component';
import { AddebookComponent } from './components/admin/addebook/addebook.component';
import { AddAudiobookComponent } from './components/admin/add-audiobook/add-audiobook.component';
import { ShowbooksComponent } from './components/user/showbooks/showbooks.component';
import { ShowaudiobooksComponent } from './components/user/showaudiobooks/showaudiobooks.component';
import { NewAdminComponent } from './components/admin/new-admin/new-admin.component';
import { ShowebooksComponent } from './components/user/showebooks/showebooks.component';
import { ShowNormalbookComponent } from './components/admin/show-normalbook/show-normalbook.component';
import { CommonModule } from '@angular/common';
import { ShowAudiobookComponent } from './components/admin/show-audiobook/show-audiobook.component';
import { ShowEbookComponent } from './components/admin/show-ebook/show-ebook.component';
import { SubscriptionComponent } from './components/user/subscription/subscription.component';
import { ShowLentRecComponent } from './components/admin/show-lent-rec/show-lent-rec.component';
import { ShowLentHistoryComponent } from './components/admin/show-lent-history/show-lent-history.component';
import { MatPaginatorIntl, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';







@NgModule({
  declarations: [
    AppComponent,
    AddbookComponent,
    UserDashboardComponent,
    AboutusComponent,
    AdminLayoutComponent,
    LandingLayoutComponent,
    UserLayoutComponent,
    AdminLayoutComponent,
    UsersignupComponent,
    LoginComponent,
    AddAudiobookComponent,
    AddebookComponent,
    AddAudiobookComponent,
    ShowbooksComponent,
    ShowaudiobooksComponent,
   NewAdminComponent,
   ShowNormalbookComponent,
   ShowebooksComponent,
    ShowebooksComponent,
    ShowNormalbookComponent,
    ShowAudiobookComponent,
    ShowEbookComponent,
    SubscriptionComponent,
    ShowLentRecComponent,
    ShowLentHistoryComponent

   

  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    MatPaginatorModule,
    MatTableModule,
    

     
  ],
  providers: [MatPaginatorIntl],
  bootstrap: [AppComponent]
})
export class AppModule { }
