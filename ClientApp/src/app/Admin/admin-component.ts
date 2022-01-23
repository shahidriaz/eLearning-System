import { Component} from "@angular/core";
import { AuthService } from "../_Services/AuthServices";

import { Router } from '@angular/router';
import { ToastrService } from "ngx-toastr";
import { Category } from "./Category";
import { AdminService } from "../_Services/AdminServices";
/*
 * Created by Shahid Riaz Bhatti
 * www.argumentexception.com
 * This is ts file for the admin dashboard
 * It has injectable service of AuthService which is used to call api
 */
@Component({
  selector: 'app-admin-component',
  templateUrl: './admin.component.html',
/*  styleUrls: ['./register.component.css']*/
})
export class adminComponent {
  constructor(private adminService: AdminService, private router: Router, private toastr: ToastrService) {
  }
  model: Category = new Category();
  CreateCategory() {
    this.adminService.CreateCategory(this.model);
  }
}
