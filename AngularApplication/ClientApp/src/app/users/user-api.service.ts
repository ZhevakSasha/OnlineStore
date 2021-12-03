import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {UserModel} from './Models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserApiService {

  apiURL = 'https://localhost:44301/api/UsersInfo/';

  public getUsers() {
    return this.httpClient.get<UserModel[]>(this.apiURL + 'info');
  }

  public getUser(id: number) {
    return this.httpClient.get<UserModel>(this.apiURL + `userInfo/${id}`);
  }

  public updateUser(user: UserModel) {
    return this.httpClient.put(this.apiURL + 'userUpdating', user);
  }

  public deleteUser(id: number) {
    return this.httpClient.delete(this.apiURL + `userDeleting/${id}`);
  }

  public getRoles() {
    return this.httpClient.get<string[]>(this.apiURL + 'rolesInfo');
  }

  constructor(private httpClient: HttpClient) { }
}
