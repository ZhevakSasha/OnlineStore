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
      var byProduct = false;
      console.log(sale.product)
      
      for (var product of sale.product) {
      if(product.name.toLowerCase().includes(search.toLowerCase())) {return true}
      }
      
      const byCustomer = sale.customerName.toLowerCase().includes(search.toLowerCase());
      if (byProduct || byCustomer === true) {return true; }
    });
  }
}
