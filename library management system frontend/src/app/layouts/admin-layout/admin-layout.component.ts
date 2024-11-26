import { Component, AfterViewInit } from '@angular/core';
import $ from 'jquery';  

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.css']
})
export class AdminLayoutComponent  {
  //isSidebarCollapsed: boolean = false;

  constructor() {}

  // ngAfterViewInit(): void {
  //   $(".menu > ul > li > a").click(function (e) {
  //     e.preventDefault();
  //     const parentLi = $(this).parent();
  //     parentLi.toggleClass("active").siblings().removeClass("active");
  //     parentLi.find(".sub-menu").slideToggle();
  //     parentLi.siblings().find(".sub-menu").slideUp();
  //   });

  //   $(".menu-btn").click(() => {
  //     $(".sidebar").toggleClass("active");
  //     this.isSidebarCollapsed = $(".sidebar").hasClass("active");
  //   });
  // }

  isExpanded = false;

  toggleSidebar() {
    this.isExpanded = !this.isExpanded;
  }
}
