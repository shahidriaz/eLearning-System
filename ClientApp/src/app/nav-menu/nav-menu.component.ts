import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { UserWithKey } from '../_Auth/UserWithKey';
import { AuthService } from '../_Services/AuthServices';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  currentUser$: Observable<UserWithKey>;
  constructor(public authService: AuthService) {}
  ngOnInit(): void
  {
    this.currentUser$ = this.authService.logedInUser$;
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
