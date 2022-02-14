import {Component, Input, OnInit, QueryList, ViewChild, ViewChildren} from '@angular/core';
import {SaleApiService} from '../sale-api.service';
import {SaleModel} from '../Models/sale.model';
import {Router} from '@angular/router';
import {SelectModel} from '../Models/select.model';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { SaleWithProductModel } from '../Models/sale-with-product.model';
import { ProductCreatingComponent } from 'src/app/products/product-creating/product-creating.component';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { empty } from 'rxjs';

@Component({
  selector: 'app-sale-creating',
  templateUrl: './sale-creating.component.html',
  styleUrls: ['./sale-creating.component.css']
})
export class SaleCreatingComponent implements OnInit {

  @Input() saleDetails: SaleModel = { products : [], customerName : '', dateOfSale : null, amount: 0 };
  @ViewChildren(ProductCreatingComponent) productCreatingComponents:QueryList<ProductCreatingComponent>;

  productsNames: SelectModel[] = [];
  customersNames: SelectModel[] = [];

  dropdownSettings: IDropdownSettings = {
    singleSelection: false,
    maxHeight : 200,
    itemsShowLimit: 4,
    allowSearchFilter: true,
    idField: 'id',
    textField: 'name',
  };
  
  saleWithProduct: SaleWithProductModel = { products : [], customerName : '', dateOfSale : null, amount: 0};
  saleWithProductFlag: boolean = false;
  
  public form: FormGroup;

  formGroup = this.fb.group({
    products: this.fb.array([])
  });

  get products(){
    return this.formGroup.controls["products"] as FormArray;
  }

  addProduct(){
    const productForm = this.fb.group({
      productName: new FormControl(null, [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(15)
      ]),
      price: new FormControl(null, [
        Validators.required,
        Validators.min(1),
        Validators.max(5000),
        Validators.pattern('^[0-9]*$')
      ]),
      unitOfMeasurement: new FormControl(null, [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(15)
      ])
    });

    this.products.push(productForm);
    console.log(this.products);
  }

  deleteProduct(index: number){
    this.products.removeAt(index);
  }

  constructor(public saleApi: SaleApiService, public  router: Router, private fb : FormBuilder) { }

  ngOnInit() {
    this.saleApi.getProductsNames()
      .subscribe(data => this.productsNames = data,
        error => this.productsNames = error);
    this.saleApi.getCustomersNames()
      .subscribe(data => this.customersNames = data,
        error => this.customersNames = error);
    this.addProduct();

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
    if(!this.saleWithProductFlag){
      console.log(this.saleWithProduct)
      this.saleApi.createSale(this.saleWithProduct).subscribe((data: {}) => {
        this.router.navigate(['/sales-list']);
        });
      } else {
        this.saleWithProduct.products = this.products.value;
        console.log("createdata", this.saleWithProduct);
        this.saleApi.createSaleWithProduct(this.saleWithProduct).subscribe((data: {}) => {
          this.router.navigate(['/sales-list']);});
        }
  }

}
