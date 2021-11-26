import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ProductModel} from './Models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductApiService {

  apiURL = 'https://localhost:44307/serviceApi/Product/';

  public getProducts() {
    return this.httpClient.get<ProductModel[]>(this.apiURL + 'getProducts');
  }

  public getProduct(id: number) {
    return this.httpClient.get<ProductModel>(this.apiURL + `getProduct/${id}`);
  }

  public updateProduct(product: ProductModel) {
    return this.httpClient.put(this.apiURL + 'updateProduct', product);
  }

  public createProduct(product: ProductModel) {
    return this.httpClient.post(this.apiURL + 'createProduct', product);
  }

  public deleteProduct(id: number) {
    return this.httpClient.delete(this.apiURL + `deleteProduct/${id}`);
  }

  constructor(private httpClient: HttpClient) { }
}
