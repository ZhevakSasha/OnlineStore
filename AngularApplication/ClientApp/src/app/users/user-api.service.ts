import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {UserModel} from './Models/user.model';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserApiService {

  apiURL = 'https://localhost:44301/api/UsersInfo/';

  public getUsers() {
    return this.httpClient.get<UserModel[]>(environment.identityApi + 'UsersInfo/info');
  }

  public getUser(id: number) {
    return this.httpClient.get<UserModel>(environment.identityApi + `UsersInfo/userInfo/${id}`);
  }

  public updateUser(user: UserModel) {
    return this.httpClient.put(environment.identityApi + 'UsersInfo/userUpdating', user);
  }

  public deleteUser(id: number) {
    return this.httpClient.delete(environment.identityApi + `UsersInfo/userDeleting/${id}`);
  }

  public getRoles() {
    return this.httpClient.get<string[]>(environment.identityApi + 'UsersInfo/rolesInfo');
  }

  constructor(private httpClient: HttpClient) { }
}
