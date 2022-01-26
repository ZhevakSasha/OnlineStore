import { Component, OnInit } from '@angular/core';
import {ProductApiService} from '../product-api.service';
import {ActivatedRoute, Router} from '@angular/router';
import {ProductModel} from '../Models/product.model';
import {FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-product-updating',
  templateUrl: './product-updating.component.html',
  styleUrls: ['./product-updating.component.css']
})
export class ProductUpdatingComponent implements OnInit {

  public id = this.actRoute.snapshot.params['id'];
  public productData: ProductModel = { productName: '',
    price: null,
    unitOfMeasurement: ''};

  form: FormGroup;

  constructor(public productApi: ProductApiService, public router: Router, public actRoute: ActivatedRoute) { }

  ngOnInit() {
    this.productApi.getProduct(this.id)
      .subscribe(data => this.productData = data,
        error => this.productData = error);
    this.form = new FormGroup({
      productName: new FormControl(this.productData.productName, [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(15)
      ]),
      price: new FormControl(this.productData.price, [
        Validators.required,
        Validators.min(1),
        Validators.max(5000),
        Validators.pattern('^[0-9]*$')
      ]),
      unitOfMeasurement: new FormControl(this.productData.unitOfMeasurement, [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(15)
      ])
    });
  }

  productUpdating() {
    if (window.confirm('Are you sure, you want to update?')) {
      this.productApi.updateProduct(this.productData).subscribe((data: {}) => {
        this.router.navigate(['/products-list']);
      });
    }
  }

}
