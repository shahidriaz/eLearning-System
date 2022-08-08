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
  constructor(public authService: AuthService) {}
  ngOnInit(): void
  {
    this.selectedRole = this.authService.GetSelectedRole();
    console.log(this.selectedRole);
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
