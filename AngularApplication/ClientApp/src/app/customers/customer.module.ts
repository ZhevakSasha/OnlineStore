import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {CustomerListComponent} from './customer-list/customer-list.component';
import {TranslateModule} from '@ngx-translate/core';
import { SharedModule } from '../Shared/shared.module';
import { RouterModule } from '@angular/router';
import { CustomerCreatingComponent } from './customer-creating/customer-creating.component';
import { CustomerUpdatingComponent } from './customer-updating/customer-updating.component';


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
        TranslateModule,
        SharedModule
    ]
})

export class CustomersModule { }
