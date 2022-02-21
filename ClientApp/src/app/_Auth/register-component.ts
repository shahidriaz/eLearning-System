import { HttpClient, HttpHeaders } from "@angular/common/http";
import { error } from "@angular/compiler/src/util";
import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { AuthService } from "../_Services/AuthServices";
import { User } from './user';
/*
 * Created by Shahid Riaz Bhatti
 * www.argumentexception.com
 * This is ts file for the Login
 * It has injectable service of AuthService which is used to call api
 */
@Component({
  selector: 'app-register-component',
  templateUrl: './register.component.html',
/*  styleUrls: ['./register.component.css'],*/
})
export class RegisterComponent implements OnInit {
  //Base URL, need to change it and read it from the config file
  baseURL: "http://localhost:65122/";
  availableRoles: any = []; // declare the array of any type to hold the roles
  selectedRole: string; // selected role by the user
  User: User = new User(); //User object
  confirmPassword: string; // This property will be used to check if the enetered password and confirm password are matched
  // Constructor injected with HttpClient and AuthService since it needs to use both
  //TODO: Shahid, need to remove the HttpClient and move the LoadApplicationRoles in a seperate Service
  constructor(private http: HttpClient, private authService: AuthService) {
    this.User = new User(); // Declared the instance of User
    this.User.SelectedRole = "Students"; // preselected Role
  }
  ngOnInit() {
    //Load all roles on initilization
    this.LoadApplicationRoles();
  }
  //Following method load all available Roles from the db
  LoadApplicationRoles() {
    //debugger;
    this.http.get("http://localhost:65122/Role/GetRoles").subscribe(result => {
      this.availableRoles = result;
    },
      error => {
        //log any error
        console.log(error);
      });
  }
  //SignUp Method is used to register a new user
  SignUp() {
    //Call the SignUp method of the AuthService in order to Signup
    this.authService.SignUp(this.User);
  }
}
