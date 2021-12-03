export class CustomerModel {
  constructor(
    public firstName: string,
    public lastName: string,
    public address: string,
    public phoneNumber: number,
    public id?: number ) {
  }
}
