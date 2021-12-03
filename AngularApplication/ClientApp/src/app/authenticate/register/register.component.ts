import {Component, Input, OnInit} from '@angular/core';
import {RegisterModel} from '../Models/register.model';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {AuthenticateApiService} from '../authenticate-api.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Input() registerDetails: RegisterModel = { username: '', password: '', email: '', petName: ''};

  form: FormGroup;

  constructor(public authApi: AuthenticateApiService, public router: Router) { }

  submit() {
    this.authApi.register(this.registerDetails).subscribe((data: { }) => {
      this.router.navigate(['/']);
    });
  }

  ngOnInit() {
    this.form = new FormGroup({
      username: new FormControl(null, [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(15)
      ]),
      password: new FormControl(null, [
        Validators.required,
        Validators.pattern('((?=.*\\d)(?=.*[a-z]).{6,12})')
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

}
