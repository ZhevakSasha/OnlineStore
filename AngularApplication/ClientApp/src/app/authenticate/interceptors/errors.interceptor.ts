import {Injectable} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {Router} from '@angular/router';
import {catchError} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class ErrorsInterceptor implements HttpInterceptor {

  constructor(private router: Router) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(req).pipe(catchError((err: HttpErrorResponse) => {
      switch (err.status) {
        case 401:
          console.log('unauthorized');
          this.router.navigateByUrl('/login');
          break;
        case 403:
          console.log('unauthorized');
          this.router.navigateByUrl('/login');
          break;
      }
        return throwError(err);
    }
    )
    );
  }
}
