import { Component, OnInit } from '@angular/core';
import { CustomerApiService } from '../customers/customer-api.service';
import { CustomerModel } from '../customers/Models/customer.model';
import { SelectModel } from '../sales/Models/select.model';
import { SaleApiService } from '../sales/sale-api.service';

@Component({
  selector: 'app-sales-report',
  templateUrl: './sales-report.component.html',
  styleUrls: ['./sales-report.component.css']
})
export class SalesReportComponent implements OnInit {

  constructor(public customerApi: CustomerApiService, public saleApi: SaleApiService) { }
  customersNames: SelectModel[] = [];
  selectedCustomerId: number;
  public customerData: CustomerModel = { firstName: '', lastName: '', address: '', phoneNumber: null, sales: [] };
  
  ngOnInit(): void {
    this.saleApi.getCustomersNames()
      .subscribe(data => this.customersNames = data,
        error => this.customersNames = error);
  }
  onSelect(value) {
    this.selectedCustomerId = value;
    console.log(this.selectedCustomerId);

    this.customerApi.getCustomerReport(this.selectedCustomerId).subscribe(data => {this.customerData = data, console.log(this.customerData.sales);},
      error => this.customerData = error,
      );
  }
}
