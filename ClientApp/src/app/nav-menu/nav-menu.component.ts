import { Component } from '@angular/core';
import { AuthService } from '../_Services/AuthServices';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  selectedRole: string;
  constructor(public authService: AuthService) {
  }
  ngAfterContentChecked(): void {
    this.selectedRole = this.authService.GetSelectedRole();
  }
  ngOnInit(): void
  {
    
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  logout() {
    this.authService.Logout();
  }
}
