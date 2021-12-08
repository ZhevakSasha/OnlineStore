import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import {CustomerListComponent} from './customer-list/customer-list.component';
import { CustomerCreatingComponent } from './customer-creating/customer-creating.component';
import { CustomerUpdatingComponent } from './customer-updating/customer-updating.component';
import {AdminGuard} from '../authenticate/authenticate-guards/admin.guard';
import {TranslateModule} from "@ngx-translate/core";


@NgModule({
  declarations: [CustomerListComponent, CustomerCreatingComponent, CustomerUpdatingComponent],
    imports: [
        RouterModule.forRoot([
            {path: 'customers-list', component: CustomerListComponent},
            {path: 'customer-creating', component: CustomerCreatingComponent},
            {path: 'customer-updating/:id', component: CustomerUpdatingComponent}
        ], {relativeLinkResolution: 'legacy'}),
        HttpClientModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        TranslateModule
    ]
})

export class CustomersModule { }
