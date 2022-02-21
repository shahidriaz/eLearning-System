/*
 * Created by Shahid Riaz Bhatti
 * www.argumentexception.com
 * This is ts file for the Login
 * It has injectable service of AuthService which is used to call api
 */
// Importing all desired directive
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from '../Admin/Category'; // SignIN

import { Router } from '@angular/router';
import { map, retry } from 'rxjs/operators';
import { Observable, ReplaySubject } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from './AuthServices';
// Making it Injectable class
@Injectable({
  providedIn: "root"
})
//AuthServicelocal
export class AdminService
{
  //BaseURL
  //TODO: Shahid, Need to read it from config
  private baseUrl = "http://localhost:65122/";
  //It is used to make post call and pass it in header
  private HttpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer '+this.GetToken()
    })
  };
  // Since this service needs to call the APIs, so need to inject HttpClient in constructir
  constructor(private httpClient: HttpClient, private router: Router, private toastr: ToastrService, private authService: AuthService)
  {
  }
  //Signup method will be used for signing up the user
  CreateCategory(category: Category) {
    this.HttpOptions.headers.set("Authorization", 'Bearer ' +this.GetToken());
    debugger;
    this.httpClient.post<Category>(this.baseUrl + 'Admin/Create', JSON.stringify(category), this.HttpOptions).subscribe(
      result =>
      {
        debugger;
        this.toastr.success("Category [' + " + category.CategoryName + "' ] is created successfully.");
        
      },
      error =>
      {
        this.toastr.error(error.message);
      });
  }
  GetToken(): string {
    var token = this.authService.GetToken();
    return token;
  }

}
