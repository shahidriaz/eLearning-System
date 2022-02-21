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
import { empty, Observable, ReplaySubject } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
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
  constructor(private httpClient: HttpClient, private router: Router, private toastr: ToastrService)
  {
  }
  //Method to Login
  Login(signIN: SignIN) {
    localStorage.removeItem("currentUser");
    // Calling the Login API
    return this.httpClient.post(this.baseUrl + 'User/Login', JSON.stringify(signIN), this.HttpOptions)
      .pipe(
        map((result: any) => {
          const user = result;
          if (user) {
            //Store the User in Local Browser Storage
            localStorage.setItem("currentUser", JSON.stringify(user));
            //Add the user in the observable
            this.currentUserSource.next(user);
            this.logedInUser$.pipe(map((r: any) => {
              return r;
            }));
          }
        }));
  }
  //Method to trigger on Logout
  Logout() {
    //remove the user from local storage on logout
    localStorage.removeItem("currentUser");
    //Change the following line to add null or empty
    this.currentUserSource.next();
    //route to the main page after logout
    this.router.navigate(['']);
  }
  //Signup method will be used for signing up the user
  SignUp(user: User) {
    this.httpClient.post<User>(this.baseUrl + 'User/Create', JSON.stringify(user), this.HttpOptions).subscribe(
      result =>
      {
        //Show the message of Success
        this.toastr.success("You have successfully Signed up with us.");
        //route to login page
        this.router.navigate(['login']);
        //console.log(JSON.stringify(result));
      },
      error =>
      {
        this.toastr.error(error.error);
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
  //This method is used to get the token of the logged in user
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
