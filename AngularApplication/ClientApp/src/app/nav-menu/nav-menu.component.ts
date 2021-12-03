import { Component } from '@angular/core';
import {AuthenticateApiService} from '../authenticate/authenticate-api.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  authFlag: boolean;

  constructor(public auth: AuthenticateApiService) {

  }

  isAuth() {
    this.authFlag = this.auth.isAuthenticated();
    return this.authFlag;
  }

  logout() {
    this.auth.logout();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
