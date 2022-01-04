import { Component, OnInit } from '@angular/core';
import { UserWithKey } from './_Auth/UserWithKey';
import { AuthService } from './_Services/AuthServices';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  constructor(private accountService: AuthService) {

  }
  ngOnInit(): void {
    this.setCurrentUser();
  }
  setCurrentUser() {
    if (localStorage.getItem("currentUser") != null) {
      //If user exists in localstorage
      const currentUser: UserWithKey = JSON.parse(localStorage.getItem("currentUser") ?? "");
      this.accountService.setCurrentUser(currentUser);
    }
  }
  title = 'app';
}

