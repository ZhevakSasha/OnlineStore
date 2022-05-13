import { Component, OnInit } from '@angular/core';
import {ProductApiService} from '../product-api.service';
import {ProductModel} from '../Models/product.model';
import { PageChangeService } from 'src/app/Shared/paginator/page-change.service';
import { Subscription } from 'rxjs';
import { PageModel } from 'src/app/Shared/paginator/page.model';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  Products: ProductModel[] = [];
  public PageNumber: number = 0;
  public PageSize: number = 10;
  public PaginationData: PageModel;

  constructor(public productApi: ProductApiService, private readonly pageChangeService: PageChangeService) { }

  ngOnInit() {
    this.pageChangeService.pageNumber$.subscribe((pageNumber) => this.PageNumber = pageNumber);
    this.pageChangeService.pageSize$.subscribe((pageSize) => this.PageSize = pageSize);
    this.loadProducts();
  }

  loadProducts = () => {
    this.productApi.getProducts(this.PageNumber, this.PageSize)
      .subscribe(data => {const header = data.headers.get('x-pagination');
      this.PaginationData = JSON.parse(header);
        this.Products = data.body,
        error => this.Products = error;
        });
    console.log("this ",this);
  }

  deleteProduct(id) {
    if (window.confirm('Are you sure, you want to delete?')) {
      this.productApi.deleteProduct(id).subscribe(data => {
        this.loadProducts();
      });
    }
  }

}
