import { Component, OnInit } from '@angular/core';
import {SaleApiService} from '../sale-api.service';
import {SaleModel} from '../Models/sale.model';
import {ActivatedRoute, Router} from '@angular/router';
import {SelectModel} from '../Models/select.model';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import { IDropdownSettings } from 'ng-multiselect-dropdown';

@Component({
  selector: 'app-sale-updating',
  templateUrl: './sale-updating.component.html',
  styleUrls: ['./sale-updating.component.css']
})
export class SaleUpdatingComponent implements OnInit {

  public id = this.actRoute.snapshot.params['id'];
  public saleData: SaleModel = { product : [], customerName : '', dateOfSale : null, amount: 0, customerId: 0};
  productsNames: SelectModel[] = [];
  customersNames: SelectModel[] = [];
  selectedItems = [];
  dropdownSettings: IDropdownSettings = {
    singleSelection: false,
    maxHeight : 200,
    itemsShowLimit: 4,
    allowSearchFilter: true,
    idField: 'id',
    textField: 'name',
  };

  form: FormGroup;

  constructor(public saleApi: SaleApiService, public router: Router, public actRoute: ActivatedRoute) { }

  ngOnInit() {
    this.saleApi.getSale(this.id)
      .subscribe(data => { this.saleData = data; this.selectedItems = data.product; console.log(this.selectedItems) },
        error => this.saleData = error);
    this.saleApi.getProductsNames()
      .subscribe(data => {this.productsNames = data, console.log(this.productsNames)},
        error => this.productsNames = error);
    this.saleApi.getCustomersNames()
      .subscribe(data => this.customersNames = data,
        error => this.customersNames = error);

        this.selectedItems = this.saleData.product
    this.form = new FormGroup({
      date: new FormControl(this.saleData.dateOfSale, [
        Validators.required
      ]),
      amount: new FormControl(this.saleData.amount, [
        Validators.required,
        Validators.min(1),
        Validators.max(50),
        Validators.pattern('^[0-9]*$')
      ])
    });
  }

  saleUpdating() {
    if (window.confirm('Are you sure, you want to update?')) {
      this.saleApi.updateSale(this.saleData).subscribe((data: {}) => {
        this.router.navigate(['/sales-list']);
      });
    }
  }
}
