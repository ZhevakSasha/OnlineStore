import {Component, Input, OnInit} from '@angular/core';
import {SaleApiService} from '../sale-api.service';
import {SaleModel} from '../Models/sale.model';
import {Router} from '@angular/router';
import {SelectModel} from '../Models/select.model';

@Component({
  selector: 'app-sale-creating',
  templateUrl: './sale-creating.component.html',
  styleUrls: ['./sale-creating.component.css']
})
export class SaleCreatingComponent implements OnInit {

  @Input() saleDetails: SaleModel = { productName : '', customerName : '', dateOfSale : null, amount: 0, customerId: 0, productId: 0 };

  productsNames: SelectModel[] = [];
  customersNames: SelectModel[] = [];

  constructor(public saleApi: SaleApiService, public  router: Router) { }

  ngOnInit() {
    this.saleApi.getProductsNames()
      .subscribe(data => this.productsNames = data,
        error => this.productsNames = error);
    this.saleApi.getCustomersNames()
      .subscribe(data => this.customersNames = data,
        error => this.customersNames = error);
  }

  addSale() {
    this.saleApi.createSale(this.saleDetails).subscribe((data: {}) => {
      this.router.navigate(['/sales-list']);
  });
  }
}
