import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ProductModel} from './Models/product.model';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductApiService {

  apiURL = 'https://localhost:44307/serviceApi/Product/';

  public getProducts(pageNumber: number, pageSize: number) {
    return this.httpClient.get<ProductModel[]>(environment.serviceApi + `Product/getProducts?PageNumber=${pageNumber}&PageSize=${pageSize}`,
      {observe: 'response'});
  }

  public getProduct(id: number) {
    return this.httpClient.get<ProductModel>(environment.serviceApi + `Product/getProduct/${id}`);
  }

  public updateProduct(product: ProductModel) {
    return this.httpClient.put(environment.serviceApi + 'Product/updateProduct', product);
  }

  public createProduct(product: ProductModel) {
    return this.httpClient.post(environment.serviceApi + 'Product/createProduct', product);
  }

  public deleteProduct(id: number) {
    return this.httpClient.delete(environment.serviceApi + `Product/deleteProduct/${id}`);
  }

  constructor(private httpClient: HttpClient) { }
}
