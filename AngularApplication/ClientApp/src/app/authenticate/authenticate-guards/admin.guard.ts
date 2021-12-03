import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Injectable} from '@angular/core';
import {AuthenticateApiService} from '../authenticate-api.service';
import {Observable} from 'rxjs';
import {AuthenticateGuard} from './authenticate.guard';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  constructor(private auth: AuthenticateApiService, private router: Router, private authGuard: AuthenticateGuard) {
  }

  canActivate(route: ActivatedRouteSnapshot,
              state: RouterStateSnapshot
    ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (!this.auth.isAdmin()) {
      this.router.navigate(['/login']);
      return false;
    }

    if (!this.authGuard.canActivate(route, state)) {
      this.router.navigate(['/login']);
      return false;
    }

    return true;
  }
}
