import {Component, Input, OnInit} from '@angular/core';
import {ProductModel} from '../Models/product.model';
import {ProductApiService} from '../product-api.service';
import {Router} from '@angular/router';
import {FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-product-creating',
  templateUrl: './product-creating.component.html',
  styleUrls: ['./product-creating.component.css']
})
export class ProductCreatingComponent implements OnInit {

  @Input() productDetails: ProductModel = { productName : '', unitOfMeasurement: '', price: null};

  form: FormGroup;

  constructor(public productApi: ProductApiService, public  router: Router) { }

  ngOnInit() {

    this.form = new FormGroup({
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
  }

  addProduct() {
    this.productApi.createProduct(this.productDetails).subscribe((data: {}) => {
      this.router.navigate(['/products-list']);
    });
  }

}
