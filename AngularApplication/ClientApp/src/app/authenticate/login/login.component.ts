import {Component, Input, OnInit} from '@angular/core';
import {LoginModel} from '../Models/login.model';
import {AuthenticateApiService} from '../authenticate-api.service';
import {Router} from '@angular/router';
import {FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  @Input() loginDetails: LoginModel = { username: '', password: ''};

  form: FormGroup;

  constructor(public authApi: AuthenticateApiService, public router: Router) { }

  submit() {
    this.authApi.login(this.loginDetails);
    this.router.navigate(['/sales-list']);

  }

  ngOnInit() {
    this.form = new FormGroup({
      username: new FormControl(null, [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(15)
        ]),
      password: new FormControl(null, [
        Validators.required
      ])
    });
  }

}
