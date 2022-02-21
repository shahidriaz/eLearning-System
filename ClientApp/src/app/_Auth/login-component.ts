import { Component} from "@angular/core";
import { AuthService } from "../_Services/AuthServices";
import { SignIN } from "./SignIN";
import { Router } from '@angular/router';
import { ToastrService } from "ngx-toastr";
import { UserWithKey } from "./UserWithKey";
/*
 * Created by Shahid Riaz Bhatti
 * www.argumentexception.com
 * This is ts file for the Login
 * It has injectable service of AuthService which is used to call api
 */
@Component({
  selector: 'app-login-component',
  templateUrl: './login.component.html',
/*  styleUrls: ['./register.component.css']*/
})
export class LoginComponent {
  /*
   * Constructor and AuthService is Injected into it
   * */
  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService) {
  }
  /*
   * Declare the SignIN object which will hold the UserName and Password for Signing In
   */
  model: SignIN = new SignIN();
  /*Login Method*/
  login() {
    /*Call the Login Method of the AuthService*/
    this.authService.Login(this.model).subscribe(
      result => {
        // Login is Invoked Successfully
        var token = this.authService.GetToken();
        this.router.navigate(['']);
      },
      error => {
        console.log(error.error);
        this.toastr.success(error.error);
      });
  }
  //This method is used to logout from the application.
  //It calls the logout method of the Authentication Service
  logout() {
    this.authService.Logout();
  }
}
