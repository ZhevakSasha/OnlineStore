import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {SaleApiService} from '../sale-api.service';
import {SaleModel} from '../Models/sale.model';
import {Router} from '@angular/router';
import {SelectModel} from '../Models/select.model';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import { SaleWithProductModel } from '../Models/sale-with-product.model';
import { ProductCreatingComponent } from 'src/app/products/product-creating/product-creating.component';

@Component({
  selector: 'app-sale-creating',
  templateUrl: './sale-creating.component.html',
  styleUrls: ['./sale-creating.component.css']
})
export class SaleCreatingComponent implements OnInit {

  @Input() saleDetails: SaleModel = { productName : '', customerName : '', dateOfSale : null, amount: 0 };
  @ViewChild(ProductCreatingComponent) productCreatingComponent:ProductCreatingComponent;

  productsNames: SelectModel[] = [];
  customersNames: SelectModel[] = [];

  
  saleWithProduct: SaleWithProductModel = { productName : '', customerName : '', dateOfSale : null, amount: 0, price: 0, unitOfMeasurement: ''};
  saleWithProductFlag: boolean = false;
  
  public form: FormGroup;

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
  
  radioChange(event){
    console.log(this.saleWithProductFlag);
    if(!this.saleWithProductFlag){
      this.form.get('productName').clearValidators();
      this.form.get('productName').updateValueAndValidity();
    } else {
      this.form.get('productName').setValidators([Validators.required]);
      this.form.get('productName').updateValueAndValidity();
    }
  }

  addSale() {
    console.log(this.saleWithProductFlag);
    if(!this.saleWithProductFlag){
      this.saleApi.createSale(this.saleWithProduct).subscribe((data: {}) => {
        this.router.navigate(['/sales-list']);
        });
      } else {
        var a = this.productCreatingComponent.addSaleWithProduct();
        this.saleWithProduct.price = a.price;
        this.saleWithProduct.unitOfMeasurement = a.unitOfMeasurement;
        this.saleWithProduct.productName = a.productName;
        console.log("createdata", this.saleWithProduct);
        this.saleApi.createSaleWithProduct(this.saleWithProduct).subscribe((data: {}) => {
          this.router.navigate(['/sales-list']);});
        }
  }

}
