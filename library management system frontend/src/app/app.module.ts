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
    ShowNormalbookComponent

   

  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
   

     
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
