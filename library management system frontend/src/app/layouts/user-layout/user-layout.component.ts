import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-layout',
  templateUrl: './user-layout.component.html',
  styleUrl: './user-layout.component.css'
})
export class UserLayoutComponent {
  isCollapsed = false; 
  menuStates: { [key: string]: boolean } = {}; 

  toggleSidebar() {
    this.isCollapsed = !this.isCollapsed;
  }

  toggleMenu(menuKey: string) {
    this.menuStates[menuKey] = !this.menuStates[menuKey];
  }

  isMenuExpanded(menuKey: string): boolean {
    return this.menuStates[menuKey] || false;
  }
}
