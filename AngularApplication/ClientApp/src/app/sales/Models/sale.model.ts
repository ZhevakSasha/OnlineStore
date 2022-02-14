import { SelectModel } from "./select.model";

export class SaleModel {
  constructor(
    public products: SelectModel[],
    public customerName: string,
    public dateOfSale: Date,
    public amount: number,
    public customerId?: number,
    public id?: number) {
  }
}
