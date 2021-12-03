import {Pipe, PipeTransform} from '@angular/core';
import {SaleModel} from './Models/sale.model';

@Pipe({
  name: 'searchSale'
})
export class SearchPipe implements PipeTransform {
  transform(sales: SaleModel[], search = ''): SaleModel[] {
    if (!search.trim()) {
      return sales;
    }

    return sales.filter(sale => {
      const byProduct = sale.productName.toLowerCase().includes(search.toLowerCase());
      const byCustomer = sale.customerName.toLowerCase().includes(search.toLowerCase());
      if (byProduct || byCustomer === true) {return true; }
    });
  }
}
