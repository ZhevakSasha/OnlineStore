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
    ]),
    HttpClientModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ]
})

export class SalesModule { }
