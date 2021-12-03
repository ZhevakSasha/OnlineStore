import { Component, OnInit } from '@angular/core';
import {CustomerApiService} from '../customer-api.service';
import {Router} from '@angular/router';
import {CustomerModel} from '../Models/customer.model';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {

  Customers: CustomerModel[] = [];

  constructor(public customerApi: CustomerApiService, router: Router) { }

  ngOnInit() {
    this.loadCustomers();
  }

  loadCustomers() {
    this.customerApi.getCustomers().subscribe(data => this.Customers = data,
      error => this.Customers = error);
  }

  deleteCustomer(id) {
    if (window.confirm('Are you sure, you want to delete?')) {
      this.customerApi.deleteCustomer(id).subscribe(data => {
        this.loadCustomers();
      });
    }
  }

}
