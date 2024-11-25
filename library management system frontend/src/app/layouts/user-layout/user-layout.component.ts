import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-layout',
  templateUrl: './user-layout.component.html',
  styleUrl: './user-layout.component.css'
})
export class UserLayoutComponent {
  constructor() { }

  
  // ngAfterViewInit(): void {
    
  //   $(".menu > ul > li > a").click(function (e) {
  //     e.preventDefault();
  //     const parentLi = $(this).parent();
  //     parentLi.toggleClass("active").siblings().removeClass("active");
  //     parentLi.find(".sub-menu").slideToggle();
  //     parentLi.siblings().find(".sub-menu").slideUp();
  //   });

  //   $(".menu-btn").click(function () {
  //     $(".sidebar").toggleClass("active");
  //   });
  // }

   isSidebarActive: boolean = false;

  // Allow any string as a key with a boolean value
  dropdowns: { [key: string]: boolean } = {
    booksDropdown: false,
  };

  toggleSidebar() {
    this.isSidebarActive = !this.isSidebarActive;
  }

  toggleDropdown(dropdown: string) {
    this.dropdowns[dropdown] = !this.dropdowns[dropdown];
  }
}
