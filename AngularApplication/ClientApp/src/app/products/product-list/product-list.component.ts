import { Component, OnInit } from '@angular/core';
import {ProductApiService} from '../product-api.service';
import {ProductModel} from '../Models/product.model';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  Products: ProductModel[] = [];

  constructor(public productApi: ProductApiService) { }

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.productApi.getProducts()
      .subscribe(data => this.Products = data,
        error => this.Products = error);
  }

  deleteProduct(id) {
    if (window.confirm('Are you sure, you want to delete?')) {
      this.productApi.deleteProduct(id).subscribe(data => {
        this.loadProducts();
      });
    }
  }

}
