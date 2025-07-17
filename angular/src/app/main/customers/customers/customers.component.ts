import { Component, OnInit, Injector } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CustomerServiceProxy, CustomerDto } from '@shared/service-proxies/service-proxies';
import { CreateOrEditCustomerComponent } from '../create-or-edit-customer-modal/create-or-edit-customer-modal.component';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent extends AppComponentBase implements OnInit {
  customers: CustomerDto[] = [];
  keyword = '';
  isLoading = false;
  Math = Math;

  pageIndex = 1;
  pageSize = 5;
  totalCount = 0;

  constructor(
    injector: Injector,
    private _customerService: CustomerServiceProxy,
    private modalService: BsModalService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.getCustomers();
  }

  getCustomers(): void {
    this.isLoading = true;

    const skipCount = (this.pageIndex - 1) * this.pageSize;

    this._customerService
      .getAll(this.keyword, undefined, skipCount, this.pageSize)
      .subscribe((result) => {
        this.customers = result.items;
        this.totalCount = result.totalCount;
        this.isLoading = false;
      });
  }

  search(): void {
    this.pageIndex = 1; // reset to first page on search
    this.getCustomers();
  }

  // Stub methods to prevent errors
  createOrEditCustomer(customerId?: number): void {
    const modalRef: BsModalRef = this.modalService.show(CreateOrEditCustomerComponent, {
      class: 'modal-lg'
    });

    if (customerId) {
      // Edit mode - load existing customer data
      modalRef.content.isEditMode = true;
      this._customerService.getCustomerForEdit(customerId).subscribe(customer => {
        modalRef.content.customer = customer;
        modalRef.content.loadUsersAndPreSelect(); // Load users and pre-select after customer data is loaded
      });
    } else {
      // Create mode
      modalRef.content.isEditMode = false;
    }

    modalRef.content.onSave = () => {
      this.getCustomers(); // refresh the list after modal is closed
    };
  }

  editCustomer(id: number): void {
    this.createOrEditCustomer(id);
  }

  deleteCustomer(id: number): void {
    this.message.confirm(
      'Are you sure you want to delete this customer?',
      'Delete Customer',
      (isConfirmed) => {
        if (isConfirmed) {
          this._customerService.delete(id).subscribe(() => {
            this.notify.success('Customer deleted successfully');
            this.getCustomers(); // refresh the list
          });
        }
      }
    );
  }

  viewUsers(customerId: number): void {
    // This method can be implemented to show assigned users
    console.log('View users for customer ID:', customerId);
  }
}
