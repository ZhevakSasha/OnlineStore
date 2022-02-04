export class SaleModel {
  constructor(
    public productName: string[],
    public customerName: string,
    public dateOfSale: Date,
    public amount: number,
    public productId?: number,
    public customerId?: number,
    public id?: number) {
  }
}
