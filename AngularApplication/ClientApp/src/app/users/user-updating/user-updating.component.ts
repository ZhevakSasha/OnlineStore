import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {UserApiService} from '../user-api.service';
import {UserModel} from '../Models/user.model';
import {FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-user-updating',
  templateUrl: './user-updating.component.html',
  styleUrls: ['./user-updating.component.css']
})
export class UserUpdatingComponent implements OnInit {

  public id = this.actRoute.snapshot.params['id'];
  public userData: UserModel = { username: '', roles: [] , email: '', petName: '' };

  form: FormGroup;

  dropdownList: string[] = [];
  selectedItems = [];
  dropdownSettings = {};

  constructor(public userApi: UserApiService, public router: Router, public actRoute: ActivatedRoute) { }

  ngOnInit() {
    this.userApi.getUser(this.id).subscribe(data => this.userData = data,
      error => this.userData = error);

    this.userApi.getRoles().subscribe(data => this.dropdownList = data,
      error => this.dropdownList = error);

    this.selectedItems = this.userData.roles;

    this.form = new FormGroup({
      username: new FormControl(null, [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(15)
      ]),
      roles: new FormControl(null, [
        Validators.required
      ]),
      email: new FormControl(null, [
        Validators.required,
        Validators.email
      ]),
      petName: new FormControl(null, [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(15)
      ]),
    });

  }

  userUpdating() {
    if (window.confirm('Are you sure, you want to update?')) {
      this.userApi.updateUser(this.userData).subscribe((data: {}) => {
        this.router.navigate(['/users-list']);
      });
    }
  }

}
