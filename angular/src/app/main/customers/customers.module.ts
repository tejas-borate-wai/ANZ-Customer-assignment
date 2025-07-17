import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CustomersRoutingModule } from './customers-routing.module';
import { CustomersComponent } from './customers/customers.component';
import { CreateOrEditCustomerComponent } from './create-or-edit-customer-modal/create-or-edit-customer-modal.component';
import { ViewUsersModalComponent } from './view-users-modal/view-users-modal.component';
import { CustomerServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { MultiSelectModule } from 'primeng/multiselect';
import { CalendarModule } from 'primeng/calendar';


@NgModule({
  declarations: [
    CustomersComponent,
    CreateOrEditCustomerComponent,
    ViewUsersModalComponent
  ],
  providers: [CustomerServiceProxy, UserServiceProxy],
  imports: [
    CommonModule,
    CustomersRoutingModule,
    FormsModule,
    NgbPaginationModule,
    AppSharedModule,
    ModalModule,
    BsDropdownModule,
    MultiSelectModule,
    CalendarModule
  ]
})
export class CustomersModule { }
