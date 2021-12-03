import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CustomerApiService} from '../customer-api.service';
import {CustomerModel} from '../Models/customer.model';
import {FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-customer-updating',
  templateUrl: './customer-updating.component.html',
  styleUrls: ['./customer-updating.component.css']
})
export class CustomerUpdatingComponent implements OnInit {

  public id = this.actRoute.snapshot.params['id'];
  public customerData: CustomerModel = { firstName: '', lastName: '', address: '', phoneNumber: null };

  form: FormGroup;

  constructor(public customerApi: CustomerApiService, public router: Router, public actRoute: ActivatedRoute) { }

  ngOnInit() {
    this.customerApi.getCustomer(this.id).subscribe(data => this.customerData = data,
      error => this.customerData = error,
      );

    this.form = new FormGroup({
      firstName: new FormControl(this.customerData.firstName, [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(15)
      ]),
      lastName: new FormControl(this.customerData.lastName, [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(15)
      ]),
      address: new FormControl(this.customerData.address, [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(15)
      ]),
      phoneNumber: new FormControl(this.customerData.phoneNumber, [
        Validators.required, Validators.pattern('^((\\\\+91-?)|0)?[0-9]{10}$')
      ]),
    });

  }

  customerUpdating() {
    if (window.confirm('Are you sure, you want to update?')) {
      this.customerApi.updateCustomer(this.customerData).subscribe((data: {}) => {
        this.router.navigate(['/customers-list']);
      });
    }
  }

}
