import { SelectModel } from "./select.model";

export class SaleWithProductModel {
    constructor(
      public product: SelectModel[],
      public customerName: string,
      public dateOfSale: Date,
      public amount: number,
      public price: number,
      public unitOfMeasurement: string,
      public customerId?: number,
      public id?: number) {
    }
  }