export class ProductModel {
  constructor(
    public productName: string,
    public price: number,
    public unitOfMeasurement: string,
    public id?: number ) {
  }
}
