export class SaleWithProductModel {
    constructor(
      public productName: string[],
      public customerName: string,
      public dateOfSale: Date,
      public amount: number,
      public price: number,
      public unitOfMeasurement: string,
      public productId?: number,
      public customerId?: number,
      public id?: number) {
    }
  }