/*
 * Created by Shahid Riaz Bhatti
 * www.argumentexception.com
 * This is ts file for the Login
 * It has injectable service of AuthService which is used to call api
 */
// Importing all desired directive
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SignIN } from '../_Auth/SignIN'; // SignIN
import { User } from '../_Auth/user'; //User
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { UserWithKey } from '../_Auth/UserWithKey';
import { Observable, ReplaySubject } from 'rxjs';
// Making it Injectable class
@Injectable({
  providedIn: "root"
})
//AuthService
export class AuthService
{
  //BaseURL
  //TODO: Shahid, Need to read it from config
  private baseUrl = "http://localhost:65122/";
  //It is used to make post call and pass it in header
  private HttpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': this.GetToken()
    })
  };
  private currentUserSource = new ReplaySubject<UserWithKey>(1);
  logedInUser$ = this.currentUserSource.asObservable();
  // Since this service needs to call the APIs, so need to inject HttpClient in constructir
  constructor(private httpClient: HttpClient, private router: Router)
  {
  }
  //Method to Login
  Login(signIN: SignIN) {
    debugger;
    // Calling the Login API
    return this.httpClient.post(this.baseUrl + 'User/Login', JSON.stringify(signIN), this.HttpOptions)
      .pipe(
        map((result: any) =>
            {
              const user = result;
              if (user) {
                localStorage.setItem("currentUser", JSON.stringify(user));
                this.currentUserSource.next(user);
              }
            }));
  }
  //Signup method will be used for signing up the user
  SignUp(user: User) {
    this.httpClient.post<User>(this.baseUrl + 'User/Create', JSON.stringify(user), this.HttpOptions).subscribe(
      result =>
      {
        //console.log(JSON.stringify(result));
      },
      error =>
      {
        console.log(error);
      });
  }
  //This method will set the currentUser along with user name and token in the currentUser replaysubject
  setCurrentUser(currentUser: UserWithKey) {
    this.currentUserSource.next(currentUser);
  }
  //This method is used to get the current Logged in User
  private getCurrentUser() {
    if (this.currentUserSource) {
      return this.logedInUser$.pipe(map((r: any) => {
        return r;
      }));
    }
    return null;
  }
  GetToken(): string {
    var user: string = "";
    if (this.currentUserSource && this.getCurrentUser() != null) {
      this.getCurrentUser()?.subscribe(res => {
        user = res.token;
      });
    }
    return user;
  } 
}
