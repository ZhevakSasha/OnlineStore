import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {CustomerModel} from './Models/customer.model';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerApiService {

  apiURL = 'https://localhost:44307/serviceApi/Customer/';

  public getCustomers(pageNumber: number, pageSize: number) {
    return this.httpClient.get<CustomerModel[]>(environment.serviceApi + `Customer/getCustomers?PageNumber=${pageNumber}&PageSize=${pageSize}`, {observe: 'response'});
  }

  public getCustomer(id: number) {
    return this.httpClient.get<CustomerModel>(environment.serviceApi + `Customer/getCustomer/${id}`);
  }

  public getCustomerReport(id: number) {
    return this.httpClient.get<CustomerModel>(environment.serviceApi + `Customer/CustomerReport/${id}`);
  }

  public updateCustomer(customer: CustomerModel) {
    return this.httpClient.put(environment.serviceApi + 'Customer/updateCustomer', customer);
  }

  public createCustomer(customer: CustomerModel) {
    return this.httpClient.post(environment.serviceApi + 'Customer/createCustomer', customer);
  }

  public deleteCustomer(id: number) {
    return this.httpClient.delete(environment.serviceApi + `Customer/deleteCustomer/${id}`);
  }

  constructor(private httpClient: HttpClient) { }
}
