import { Component, Injector, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/common/app-component-base';
import { CustomerServiceProxy, CreateOrEditCustomerDto, UserServiceProxy, GetUsersInput, UserListDto } from '@shared/service-proxies/service-proxies';
import { DateTime } from 'luxon';

@Component({
  selector: 'app-create-or-edit-customer',
  templateUrl: './create-or-edit-customer-modal.component.html',
  styleUrls: ['./create-or-edit-customer-modal.component.css']
})
export class CreateOrEditCustomerComponent extends AppComponentBase implements OnInit {
  saving = false;
  customer = new CreateOrEditCustomerDto();
  onSave: () => void;
  isEditMode = false;
  
  users: UserListDto[] = [];
  selectedUsers: UserListDto[] = [];
  
  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    private _customerService: CustomerServiceProxy,
    private _userService: UserServiceProxy
  ) {
    super(injector);
  }

  ngOnInit(): void {
    // Initialize registration date to today if not in edit mode
    if (!this.isEditMode) {
      this.customer.registrationDate = DateTime.now();
      this.loadUsers();
    }
    // For edit mode, loadUsersAndPreSelect will be called from the parent component
  }

  loadUsers(): void {
    const getUsersInput = new GetUsersInput();
    getUsersInput.maxResultCount = 1000; // Load all users
    getUsersInput.skipCount = 0;
    
    this._userService.getUsers(getUsersInput).subscribe(result => {
      this.users = result.items || [];
    });
  }

  loadUsersAndPreSelect(): void {
    const getUsersInput = new GetUsersInput();
    getUsersInput.maxResultCount = 1000; // Load all users
    getUsersInput.skipCount = 0;
    
    this._userService.getUsers(getUsersInput).subscribe(result => {
      this.users = result.items || [];
      
      // Ensure registration date is properly converted to DateTime if it's a string
      if (this.customer.registrationDate && typeof this.customer.registrationDate === 'string') {
        this.customer.registrationDate = DateTime.fromISO(this.customer.registrationDate);
      }
      
      // Pre-select assigned users in edit mode
      if (this.isEditMode && this.customer.assignedUserIds && this.customer.assignedUserIds.length > 0) {
        this.selectedUsers = this.users.filter(user => 
          this.customer.assignedUserIds!.includes(user.id)
        );
        console.log('Pre-selected users:', this.selectedUsers);
        console.log('Customer assigned user IDs:', this.customer.assignedUserIds);
        console.log('All users:', this.users);
      }
    });
  }

  trackByUserId(index: number, user: UserListDto): number {
    return user.id;
  }

  save(): void {
    this.saving = true;
    
    // Map selected users to IDs
    this.customer.assignedUserIds = this.selectedUsers.map(user => user.id);
    
    const saveOperation = this.isEditMode 
      ? this._customerService.update(this.customer)
      : this._customerService.create(this.customer);
    
    saveOperation.subscribe(() => {
      this.notify.info(this.l('SavedSuccessfully'));
      this.bsModalRef.hide();
      if (this.onSave) {
        this.onSave();
      }
    }, () => {
      this.saving = false;
    });
  }
}
