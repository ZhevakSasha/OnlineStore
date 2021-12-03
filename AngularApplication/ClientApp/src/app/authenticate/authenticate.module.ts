import {NgModule, Provider} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import {ReactiveFormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {AuthenticateInterceptor} from './interceptors/authenticate.interceptor';
import {ErrorsInterceptor} from './interceptors/errors.interceptor';

const INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  useClass: AuthenticateInterceptor,
  multi: true
};

const INTERCEPTOR_ERROR: Provider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorsInterceptor,
  multi: true
};

@NgModule({
  declarations: [RegisterComponent, LoginComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'login', component: LoginComponent},
      { path: 'register', component: RegisterComponent }
    ])
  ],
  providers: [INTERCEPTOR_PROVIDER, INTERCEPTOR_ERROR]
})
export class AuthenticateModule { }
