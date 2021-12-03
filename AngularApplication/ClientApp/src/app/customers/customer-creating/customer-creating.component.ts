import {Component, Input, OnInit} from '@angular/core';
import {CustomerModel} from '../Models/customer.model';
import {CustomerApiService} from '../customer-api.service';
import {Router} from '@angular/router';
import {FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-customer-creating',
  templateUrl: './customer-creating.component.html',
  styleUrls: ['./customer-creating.component.css']
})
export class CustomerCreatingComponent implements OnInit {

  constructor(public customerApi: CustomerApiService, public router: Router) { }

  form: FormGroup;

  @Input() customerDetails: CustomerModel = {firstName: '', lastName: '', address: '', phoneNumber: null};

  ngOnInit() {
    this.form = new FormGroup({
      firstName: new FormControl(null, [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(15)
      ]),
      lastName: new FormControl(null, [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(15)
      ]),
      address: new FormControl(null, [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(15)
      ]),
      phoneNumber: new FormControl(null, [
        Validators.required, Validators.pattern('^((\\\\+91-?)|0)?[0-9]{10}$')
      ]),
    });
  }

  addCustomer() {
  this.customerApi.createCustomer(this.customerDetails).subscribe((data: {}) => {
  this.router.navigate(['/customers-list']);
    });
  }

}
