import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LoginModel} from './Models/login.model';
import {RegisterModel} from './Models/register.model';
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateApiService {

  apiURL = 'https://localhost:44301/api/Authenticate/';

  constructor(public httpClient: HttpClient) { }

  get token(): string {
    const expDate = new Date(localStorage.getItem('jwt-expiration'));
    if (new Date() > expDate) {
      this.logout();
      return null;
    }
    return localStorage.getItem('jwt-token');
  }

  login(login: LoginModel) {
    return this.httpClient.post(this.apiURL + 'login', login).subscribe((data: any) => {
      localStorage.setItem('jwt-token', data.token);
      localStorage.setItem('jwt-expiration', data.expiration);
    });
  }

  logout() {
    localStorage.removeItem('jwt-token');
    localStorage.removeItem('jwt-expiration');
  }

  isAuthenticated(): boolean {
    // console.log('auth' , !!this.token);
    return !!this.token;
  }

  isAdmin(): boolean {
    const decodedToken = jwt_decode(this.token);
    const roles = decodedToken['https://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    return roles.includes('Admin');
  }

  register(register: RegisterModel) {
    return this.httpClient.post(this.apiURL + 'register', register);
  }
}
