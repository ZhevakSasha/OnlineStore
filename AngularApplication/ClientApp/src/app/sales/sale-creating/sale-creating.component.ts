import {Component, Input, OnInit} from '@angular/core';
import {SaleApiService} from '../sale-api.service';
import {SaleModel} from '../Models/sale.model';
import {Router} from '@angular/router';
import {SelectModel} from '../Models/select.model';
import {FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-sale-creating',
  templateUrl: './sale-creating.component.html',
  styleUrls: ['./sale-creating.component.css']
})
export class SaleCreatingComponent implements OnInit {

  @Input() saleDetails: SaleModel = { productName : '', customerName : '', dateOfSale : null, amount: 0 };

  productsNames: SelectModel[] = [];
  customersNames: SelectModel[] = [];

  form: FormGroup;

  constructor(public saleApi: SaleApiService, public  router: Router) { }

  ngOnInit() {
    this.saleApi.getProductsNames()
      .subscribe(data => this.productsNames = data,
        error => this.productsNames = error);
    this.saleApi.getCustomersNames()
      .subscribe(data => this.customersNames = data,
        error => this.customersNames = error);

    this.form = new FormGroup({
      productName: new FormControl(null, [
        Validators.required
      ]),
      customerName: new FormControl(null, [
        Validators.required
      ]),
      date: new FormControl(null, [
        Validators.required
      ]),
      amount: new FormControl(null, [
        Validators.required,
        Validators.min(1),
        Validators.max(50),
        Validators.pattern('^[0-9]*$')
      ])
    });
  }

  addSale() {
    this.saleApi.createSale(this.saleDetails).subscribe((data: {}) => {
      this.router.navigate(['/sales-list']);
  });
  }
}
