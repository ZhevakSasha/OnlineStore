import { SaleModel } from "src/app/sales/Models/sale.model";

export class CustomerModel {
  constructor(
    public firstName: string,
    public lastName: string,
    public address: string,
    public phoneNumber: number,
    public sales: SaleModel[],
    public id?: number ) {
  }
}
