import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {SaleModel} from './Models/sale.model';
import {SelectModel} from './Models/select.model';

@Injectable({
  providedIn: 'root'
})

export class SaleApiService {

  apiURL = 'https://localhost:44307/serviceApi/Sale/';

  public getSales() {
    return this.httpClient.get<SaleModel[]>(this.apiURL + 'getSales');
  }

  public getSale(id: number) {
    return this.httpClient.get<SaleModel>(this.apiURL + `getSale/${id}`);
  }

  public getProductsNames() {
    return this.httpClient.get<SelectModel[]>('https://localhost:44307/serviceApi/Product/getProductsNames');
  }

  public getCustomersNames() {
    return this.httpClient.get<SelectModel[]>('https://localhost:44307/serviceApi/Customer/getCustomersNames');
  }

  public updateSale(sale: SaleModel) {
    return this.httpClient.put(this.apiURL + 'updateSale', sale);
  }

  public createSale(sale: SaleModel) {
    return this.httpClient.post(this.apiURL + 'createSale', sale);
  }

  public deleteSale(id: number) {
    return this.httpClient.delete(this.apiURL + `deleteSale/${id}`);
  }

  constructor(private httpClient: HttpClient) { }

}

