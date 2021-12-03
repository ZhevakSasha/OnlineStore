import { Component, OnInit } from '@angular/core';
import {SaleModel} from '../Models/sale.model';
import {SaleApiService} from '../sale-api.service';

@Component({
  selector: 'app-sale-list',
  templateUrl: './sale-list.component.html',
  styleUrls: ['./sale-list.component.css']
})
export class SaleListComponent implements OnInit {

  Sales: SaleModel[] = [];
  searchStr = '';

  constructor(public saleApi: SaleApiService) { }

  ngOnInit() {
    this.loadSales();
    }

  loadSales() {
    this.saleApi.getSales()
      .subscribe(data => this.Sales = data,
        error => this.Sales = error);
  }

  deleteSale(id) {
    if (window.confirm('Are you sure, you want to delete?')) {
      this.saleApi.deleteSale(id).subscribe(data => {
        this.loadSales();
      });
    }
  }
}

