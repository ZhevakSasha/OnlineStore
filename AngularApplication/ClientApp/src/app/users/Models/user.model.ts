export class UserModel {
  constructor(
    public username: string,
    public email: string,
    public petName: string,
    public roles: string[],
    public id?: number) {
  }
}
