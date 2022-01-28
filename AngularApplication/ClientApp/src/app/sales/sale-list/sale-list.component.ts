import { Component, OnInit } from '@angular/core';
import { PageChangeService } from 'src/app/Shared/paginator/page-change.service';
import { PageModel } from 'src/app/Shared/paginator/page.model';
import {SaleModel} from '../Models/sale.model';
import {SaleApiService} from '../sale-api.service';

@Component({
  selector: 'app-sale-list',
  templateUrl: './sale-list.component.html',
  styleUrls: ['./sale-list.component.css']
})
export class SaleListComponent implements OnInit {

  Sales: SaleModel[];
  searchStr = '';
  public PageNumber: number = 0;
  public PageSize: number = 10;
  public PaginationData:PageModel;

  constructor(public saleApi: SaleApiService, private readonly pageChangeService: PageChangeService) { }

  ngOnInit() {
    this.pageChangeService.pageNumber$.subscribe((pageNumber) => this.PageNumber = pageNumber);
    this.pageChangeService.pageSize$.subscribe((pageSize) => this.PageSize = pageSize);
    this.loadSales();
    }

  loadSales() {
    this.saleApi.getSales(this.PageNumber, this.PageSize)
      .subscribe(data => {const header = data.headers.get('x-pagination');
      this.PaginationData = JSON.parse(header);
        this.Sales = data.body,
        error => this.Sales = error; 
        });
  }

  deleteSale(id) {
    if (window.confirm('Are you sure, you want to delete?')) {
      this.saleApi.deleteSale(id).subscribe(data => {
        this.loadSales();
      });
    }
  }
}

