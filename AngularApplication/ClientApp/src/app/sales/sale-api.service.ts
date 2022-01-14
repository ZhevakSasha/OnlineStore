import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {SaleModel} from './Models/sale.model';
import {SelectModel} from './Models/select.model';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class SaleApiService {

  apiURL = 'https://localhost:44307/serviceApi/Sale/';

  public getSales() {
    return this.httpClient.get<SaleModel[]>(environment.serviceApi + 'Sale/getSales');
  }

  public getSale(id: number) {
    return this.httpClient.get<SaleModel>(environment.serviceApi + `Sale/getSale/${id}`);
  }

  public getProductsNames() {
    return this.httpClient.get<SelectModel[]>(environment.serviceApi + 'Product/getProductsNames');
  }

  public getCustomersNames() {
    return this.httpClient.get<SelectModel[]>(environment.serviceApi + 'Customer/getCustomersNames');
  }

  public updateSale(sale: SaleModel) {
    return this.httpClient.put(environment.serviceApi + 'Sale/updateSale', sale);
  }

  public createSale(sale: SaleModel) {
    return this.httpClient.post(environment.serviceApi + 'Sale/createSale', sale);
  }

  public deleteSale(id: number) {
    return this.httpClient.delete(environment.serviceApi + `Sale/deleteSale/${id}`);
  }

  constructor(private httpClient: HttpClient) { }

}

