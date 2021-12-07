import {Component, OnInit} from '@angular/core';
import {AuthenticateApiService} from '../authenticate/authenticate-api.service';
import {TranslateService} from '@ngx-translate/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  authFlag: boolean;

  lang;

  ngOnInit() {
    this.lang = localStorage.getItem('lang') || 'en';
    console.log(this.lang);
  }

  constructor(public auth: AuthenticateApiService, private translate: TranslateService) {

  }

  isAuth() {
    this.authFlag = this.auth.isAuthenticated();
    return this.authFlag;
  }

  logout() {
    this.auth.logout();
  }

  changeLang(lang) {
    console.log(lang.value);
    this.translate.setDefaultLang(lang.value);
    localStorage.setItem('lang', lang.value);
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
