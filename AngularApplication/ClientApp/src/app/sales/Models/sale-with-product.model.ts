import { ProductModel } from "src/app/products/Models/product.model";
import { SelectModel } from "./select.model";

export class SaleWithProductModel {
    constructor(
      public products: ProductModel[],
      public customerName: string,
      public dateOfSale: Date,
      public amount: number,
      public customerId?: number,
      public id?: number) {
    }
  }