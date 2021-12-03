import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {CustomerModel} from './Models/customer.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerApiService {

  apiURL = 'https://localhost:44307/serviceApi/Customer/';

  public getCustomers() {
    return this.httpClient.get<CustomerModel[]>(this.apiURL + 'getCustomers');
  }

  public getCustomer(id: number) {
    return this.httpClient.get<CustomerModel>(this.apiURL + `getCustomer/${id}`);
  }

  public updateCustomer(customer: CustomerModel) {
    return this.httpClient.put(this.apiURL + 'updateCustomer', customer);
  }

  public createCustomer(customer: CustomerModel) {
    return this.httpClient.post(this.apiURL + 'createCustomer', customer);
  }

  public deleteCustomer(id: number) {
    return this.httpClient.delete(this.apiURL + `deleteCustomer/${id}`);
  }

  constructor(private httpClient: HttpClient) { }
}
