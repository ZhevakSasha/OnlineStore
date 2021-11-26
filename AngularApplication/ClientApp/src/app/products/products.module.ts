import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import { ProductListComponent } from './product-list/product-list.component';



@NgModule({
  declarations: [ProductListComponent],
  imports: [
    RouterModule.forRoot([
      {path: 'products-list', component: ProductListComponent},
    ]),
    HttpClientModule,
    CommonModule,
    FormsModule
  ]
})
export class ProductsModule { }
