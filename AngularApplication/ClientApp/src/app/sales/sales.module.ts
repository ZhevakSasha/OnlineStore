import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {SaleListComponent} from './sale-list/sale-list.component';
import {HttpClientModule} from '@angular/common/http';
import {CommonModule} from '@angular/common';
import {SaleCreatingComponent} from './sale-creating/sale-creating.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { SaleUpdatingComponent } from './sale-updating/sale-updating.component';
import {SearchPipe} from './search.pipe';
import {AdminGuard} from '../authenticate/authenticate-guards/admin.guard';
import { SharedModule } from '../Shared/shared.module';
import { ProductsModule } from '../products/products.module';
import {MatRadioModule} from '@angular/material/radio';
import { BrowserModule } from '@angular/platform-browser';
import { TranslateModule } from '@ngx-translate/core';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';


@NgModule({
  declarations: [
    SaleListComponent,
    SaleCreatingComponent,
    SaleUpdatingComponent,
    SearchPipe
  ],
    imports: [
        RouterModule.forRoot([
            {path: 'sales-list', component: SaleListComponent},
            {path: 'sale-creating', component: SaleCreatingComponent},
            {path: 'sale-updating/:id', component: SaleUpdatingComponent}
        ], {relativeLinkResolution: 'legacy'}),
        HttpClientModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        FormsModule,
        TranslateModule,
        SharedModule,
        ProductsModule,
        MatRadioModule,
        BrowserModule,
        NgMultiSelectDropDownModule
    ]
})

export class SalesModule { }
