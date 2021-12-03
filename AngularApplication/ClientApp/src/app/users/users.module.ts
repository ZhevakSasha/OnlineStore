import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from '@angular/router';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { UserListComponent } from './user-list/user-list.component';
import { UserUpdatingComponent } from './user-updating/user-updating.component';
import {NgMultiSelectDropDownModule} from 'ng-multiselect-dropdown';
import {AdminGuard} from "../authenticate/authenticate-guards/admin.guard";




@NgModule({
  declarations: [UserListComponent, UserUpdatingComponent],
    imports: [
        NgMultiSelectDropDownModule.forRoot(),
        RouterModule.forRoot([
            {path: 'users-list', component: UserListComponent, canActivate: [AdminGuard]},
            {path: 'user-updating/:id', component: UserUpdatingComponent, canActivate: [AdminGuard]}
        ]),
        HttpClientModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule
    ]
})
export class UsersModule { }
