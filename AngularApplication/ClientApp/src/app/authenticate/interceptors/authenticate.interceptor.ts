import {Injectable} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {AuthenticateApiService} from './authenticate-api.service';

@Injectable({
  providedIn: 'root'
})

export class AuthenticateInterceptor implements HttpInterceptor {

  constructor(private auth: AuthenticateApiService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    req = req.clone({
      headers: req.headers.append('Authorization', 'Bearer ' + this.auth.token)
    });
    return next.handle(req);
  }
}
