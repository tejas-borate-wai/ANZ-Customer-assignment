import { Component, Injector, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/common/app-component-base';
import { CustomerServiceProxy, CreateOrEditCustomerDto, UserServiceProxy, GetUsersInput, UserListDto, CustomerDto } from '@shared/service-proxies/service-proxies';
import { DateTime } from 'luxon';
import { forkJoin } from 'rxjs';

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
  allCustomers: CustomerDto[] = [];
  alreadyAssignedUserIds: number[] = [];
  
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
      this.loadUnassignedUsers();
    }
    // For edit mode, loadUsersAndPreSelect will be called from the parent component
  }
  
  // Get all customers to extract assigned users
  loadAllCustomers(): void {
    this._customerService.getAll('', undefined, 0, 1000).subscribe(result => {
      this.allCustomers = result.items || [];
      
      if (this.allCustomers.length === 0) {
        // No customers yet, so no users are assigned
        this.processAvailableUsers();
      } else {
        this.extractAlreadyAssignedUserIds();
      }
    });
  }
  
  // Extract all user IDs that are already assigned to any customer
  extractAlreadyAssignedUserIds(): void {
    this.alreadyAssignedUserIds = [];
    let loadedCustomers = 0;
    const totalCustomers = this.allCustomers.length;
    
    // If there are no customers, proceed with empty assigned users list
    if (totalCustomers === 0) {
      this.processAvailableUsers();
      return;
    }
    
    this.allCustomers.forEach(customer => {
      // Skip the current customer in edit mode
      if (this.isEditMode && this.customer.id === customer.id) {
        loadedCustomers++;
        // If this was the last customer, we need to process the users
        if (loadedCustomers === totalCustomers) {
          this.processAvailableUsers();
        }
        return;
      }
      
      // Use customer.id to get the full customer data with assigned users
      this._customerService.getCustomerForEdit(customer.id).subscribe(customerDetail => {
        loadedCustomers++;
        
        if (customerDetail.assignedUserIds && customerDetail.assignedUserIds.length > 0) {
          this.alreadyAssignedUserIds = [
            ...this.alreadyAssignedUserIds,
            ...customerDetail.assignedUserIds
          ];
          
          // Remove duplicates
          this.alreadyAssignedUserIds = [...new Set(this.alreadyAssignedUserIds)];
        }
        
        // If all customers have been processed, update the user list
        if (loadedCustomers === totalCustomers) {
          this.processAvailableUsers();
        }
      });
    });
  }
  
  // Process users based on already assigned IDs
  processAvailableUsers(): void {
    console.log('Processing available users with assigned IDs:', this.alreadyAssignedUserIds);
    
    if (this.isEditMode) {
      // In edit mode, filter the users again
      this.filterUsersForEditMode();
    } else {
      // For new customers, just filter out all assigned users
      this.filterOutAssignedUsers();
    }
  }

  loadUsers(): void {
    const getUsersInput = new GetUsersInput();
    getUsersInput.maxResultCount = 1000; // Load all users
    getUsersInput.skipCount = 0;
    
    this._userService.getUsers(getUsersInput).subscribe(result => {
      this.users = result.items || [];
    });
  }
  
  loadUnassignedUsers(): void {
    // First load all customers to get assigned users
    this.loadAllCustomers();
    
    // Then load all users
    const getUsersInput = new GetUsersInput();
    getUsersInput.maxResultCount = 1000;
    getUsersInput.skipCount = 0;
    
    this._userService.getUsers(getUsersInput).subscribe(result => {
      this.users = result.items || [];
      // Initial filter (will be refined as customer data loads)
      this.filterOutAssignedUsers();
    });
  }
  
  filterOutAssignedUsers(): void {
    if (this.alreadyAssignedUserIds.length > 0) {
      // Filter out users who are already assigned to other customers
      this.users = this.users.filter(user => !this.alreadyAssignedUserIds.includes(user.id));
      console.log('Filtered users (available for assignment):', this.users);
    }
  }
  
  filterUsersForEditMode(): void {
    if (this.users.length === 0) {
      return; // No users to filter
    }
    
    // Get current customer's assigned users
    const currentCustomerUserIds = this.customer.assignedUserIds || [];
    
    // For edit mode, only show:
    // 1. Users already assigned to this customer
    // 2. Users not assigned to any customer
    this.users = this.users.filter(user => {
      // Keep users already assigned to this customer
      if (currentCustomerUserIds.includes(user.id)) {
        return true;
      }
      
      // Keep users not assigned to any other customer
      return !this.alreadyAssignedUserIds.includes(user.id);
    });
    
    console.log('Filtered users for edit mode:', this.users);
    console.log('Current customer user IDs:', currentCustomerUserIds);
    console.log('Already assigned to other customers:', this.alreadyAssignedUserIds);
    
    // Set selected users (those currently assigned to this customer)
    if (currentCustomerUserIds.length > 0) {
      this.selectedUsers = this.users.filter(user => 
        currentCustomerUserIds.includes(user.id)
      );
      console.log('Pre-selected users:', this.selectedUsers);
    }
  }

  loadUsersAndPreSelect(): void {
    // Ensure registration date is properly converted to DateTime if it's a string
    if (this.customer.registrationDate && typeof this.customer.registrationDate === 'string') {
      this.customer.registrationDate = DateTime.fromISO(this.customer.registrationDate);
    }
    
    // First load all users
    const getUsersInput = new GetUsersInput();
    getUsersInput.maxResultCount = 1000; // Load all users
    getUsersInput.skipCount = 0;
    
    this._userService.getUsers(getUsersInput).subscribe(result => {
      // Store all users
      this.users = result.items || [];
      
      // Now load all customers to get information about assigned users
      this.loadAllCustomers();
      
      // The filtering will happen in processAvailableUsers after loadAllCustomers completes
    });
  }

  trackByUserId(index: number, user: UserListDto): number {
    return user.id;
  }

  save(): void {
    this.saving = true;
    
    try {
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
    } catch (error) {
      this.notify.error('An error occurred while saving customer data.');
      this.saving = false;
    }
  }
}
