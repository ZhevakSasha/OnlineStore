import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from '@angular/router';
import {HttpClientModule} from '@angular/common/http';
import { UserListComponent } from './user-list/user-list.component';
import { UserUpdatingComponent } from './user-updating/user-updating.component';
import {NgMultiSelectDropDownModule} from 'ng-multiselect-dropdown';
import {AdminGuard} from '../authenticate/authenticate-guards/admin.guard';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {TranslateModule} from "@ngx-translate/core";




@NgModule({
  declarations: [UserListComponent, UserUpdatingComponent],
    imports: [
        NgMultiSelectDropDownModule.forRoot(),
        RouterModule.forRoot([
            {path: 'users-list', component: UserListComponent},
            {path: 'user-updating/:id', component: UserUpdatingComponent}
        ], {relativeLinkResolution: 'legacy'}),
        HttpClientModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        TranslateModule,
    ]
})
export class UsersModule { }
