import { Component, OnInit } from '@angular/core';
import {CustomerApiService} from '../customer-api.service';
import {Router} from '@angular/router';
import {CustomerModel} from '../Models/customer.model';
import { PageModel } from 'src/app/Shared/paginator/page.model';
import { PageChangeService } from 'src/app/Shared/paginator/page-change.service';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {

  Customers: CustomerModel[];
  public PageNumber: number = 0;
  public PageSize: number = 10;
  public PaginationData:PageModel;

  constructor(public customerApi: CustomerApiService, private readonly pageChangeService: PageChangeService) { }

  ngOnInit() {
    this.pageChangeService.pageNumber$.subscribe((pageNumber) => this.PageNumber = pageNumber);
    this.pageChangeService.pageSize$.subscribe((pageSize) => this.PageSize = pageSize);
    this.loadCustomers();
  }

  loadCustomers() {
    this.customerApi.getCustomers(this.PageNumber, this.PageSize).subscribe(data => {const header = data.headers.get('x-pagination');
    this.PaginationData = JSON.parse(header);
      this.Customers = data.body,
      error => this.Customers = error; 
      });
  }

  deleteCustomer(id) {
    if (window.confirm('Are you sure, you want to delete?')) {
      this.customerApi.deleteCustomer(id).subscribe(data => {
        this.loadCustomers();
      });
    }
  }

}
