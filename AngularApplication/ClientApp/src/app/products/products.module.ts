import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductUpdatingComponent } from './product-updating/product-updating.component';
import { ProductCreatingComponent } from './product-creating/product-creating.component';
import {AdminGuard} from '../authenticate/authenticate-guards/admin.guard';
import {TranslateModule} from "@ngx-translate/core";
import { PaginatorComponent } from '../Shared/paginator/paginator.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { SharedModule } from '../Shared/shared.module';




@NgModule({
  declarations: [ProductListComponent, ProductUpdatingComponent, ProductCreatingComponent],
    imports: [
        RouterModule.forRoot([
            {path: 'products-list', component: ProductListComponent},
            {path: 'product-updating/:id', component: ProductUpdatingComponent},
            {path: 'product-creating', component: ProductCreatingComponent}
        ], {relativeLinkResolution: 'legacy'}),
        HttpClientModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        TranslateModule,
        SharedModule
    ]
})
export class ProductsModule { }
