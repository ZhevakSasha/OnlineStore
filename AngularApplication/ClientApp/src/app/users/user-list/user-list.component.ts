import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {UserApiService} from '../user-api.service';
import {UserModel} from '../Models/user.model';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  Users: UserModel[] = [];

  constructor(public userApi: UserApiService, router: Router) { }

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers() {
    this.userApi.getUsers().subscribe(data => this.Users = data,
      error => this.Users = error);
  }

  deleteUser(id) {
    if (window.confirm('Are you sure, you want to delete?')) {
      this.userApi.deleteUser(id).subscribe(data => {
        this.loadUsers();
      });
    }
  }

}
