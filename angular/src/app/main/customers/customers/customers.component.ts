import { Component, OnInit, Injector } from '@angular/core';
import Swal from 'sweetalert2';
import { AppComponentBase } from '@shared/common/app-component-base';
import { CustomerServiceProxy, CustomerDto, PagedResultDtoOfCustomerDto, CreateOrEditCustomerDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent extends AppComponentBase implements OnInit {

  customers: CustomerDto[] = []; 
  totalCount: number = 0; 
  loading: boolean = false; 
  searchText: string = ''; 
  selectedCustomer: CustomerDto | null = null; 
  showCustomerModal: boolean = false; 
  showUsersModal: boolean = false; 
  
  currentPage: number = 1;
  pageSize: number = 5;
  
  userList = []; 
  customerUsers: any[] = [];
  userSearchText: string = '';

customerForm = {
  id: null, 
  name: '',
  emailId: '',
  address: '',
  registrationDate: '',
  userIds: [] as number[],
  
};

isEditMode: boolean = false; 


  constructor(
    injector: Injector,
    private _customerService: CustomerServiceProxy,
    private _userService: UserServiceProxy
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.loadCustomers(); 
  }

  loadCustomers(): void {
    this.loading = true; 
    
    const skipCount = (this.currentPage - 1) * this.pageSize;
    
    this._customerService.getAll(
      this.searchText || undefined, 
      undefined, 
      skipCount, 
      this.pageSize 
    ).subscribe((result: PagedResultDtoOfCustomerDto) => {
      this.customers = result.items || [];
      this.totalCount = result.totalCount || 0;
      this.loading = false;
    });
  }

  // search customers
  searchCustomers(): void {
    this.currentPage = 1; 
    this.loadCustomers(); 
  }

  //refresh customers
  refreshCustomers(): void {
    this.searchText = '';
    this.currentPage = 1;
    this.loadCustomers();
  }

  // change page size
  onPageSizeChange(event: any): void {
    this.pageSize = parseInt(event.target.value);
    this.currentPage = 1;
    this.loadCustomers();
  }

  // Method to change page
  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadCustomers();
  }

  // Method to get total pages
  get totalPages(): number {
    return Math.ceil(this.totalCount / this.pageSize);
  }

  // Method to get page numbers for pagination
  get pageNumbers(): number[] {
    const pages = [];
    const totalPages = this.totalPages;
    const startPage = Math.max(1, this.currentPage - 2);
    const endPage = Math.min(totalPages, startPage + 4);
    
    for (let i = startPage; i <= endPage; i++) {
      pages.push(i);
    }
    return pages;
  }

  // Method to toggle dropdown
  toggleDropdown(customer: CustomerDto): void {
    if (this.selectedCustomer?.id === customer.id) {
      this.selectedCustomer = null; 
    } else {
      this.selectedCustomer = customer;
    }
  }

  // Method to view users for customer
viewUsers(customer: CustomerDto): void {
  this.selectedCustomer = customer;
  this.userSearchText = '';
  this.loadCustomerUsers();
  this.showUsersModal = true;
}

loadCustomerUsers(): void {
  this._customerService.getUsersByCustomerId(this.selectedCustomer.id, this.userSearchText).subscribe({
    next: (result) => {
      this.customerUsers = result.items || [];
    }
  });
}
  closeUsersModal(): void {
    this.showUsersModal = false;
  }

  editCustomer(customer: CustomerDto): void {
    this.selectedCustomer = null;
    
    this._customerService.getCustomerForEdit(customer.id).subscribe((result) => {
      this.isEditMode = true;
      this.customerForm = {
        id: result.id,
        name: result.name || '',
        emailId: result.emailId || '',
        address: result.address || '',
        registrationDate: result.registrationDate ? result.registrationDate.toString().split('T')[0] : '',
        userIds: result.assignedUserIds || []
      };
      this.loadCustomerUserDropdown(result.id); 
      this.showCustomerModal = true; 
    });
  }

  //delete customer
  deleteCustomer(customer: CustomerDto): void {
    this.selectedCustomer = null; // Close dropdown
    // Use SweetAlert2 for confirmation
    Swal.fire({
      title: 'Are you sure?',
      text: `Are you sure you want to delete customer "${customer.name}"?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'Cancel'
    }).then((result: any) => {
      if (result.isConfirmed) {
        this.loading = true;
        this._customerService.delete(customer.id).subscribe(() => {
          this.loading = false;
          this.notify.success('Customer deleted successfully!');
          this.loadCustomers(); 
        });
      }
    });
  }

  openCustomerModal(): void {
    this.showCustomerModal = true;
    this.loadUnassignedUsers(); 
  }

  // Method to load unassigned users
  loadUnassignedUsers(): void {
    this._customerService.getUnassignedUsers().subscribe((result) => {
      this.userList = result.items.map(item => ({
        id: item.value,
        name: item.name
      }));
    });
  }

  // Method to load all users dropdown (for edit mode)
  loadCustomerUserDropdown(customerId: number): void {
  this._customerService.getCustomerUserDropdown(customerId).subscribe(result => {
    const allUsers = [...result.assignedUsers, ...result.unassignedUsers];

    this.userList = allUsers.map(u => ({
      id: u.id,
      name: u.name
    }));

    // Pre-select assigned users
    this.customerForm.userIds = result.assignedUsers.map(u => u.id);
  });
}


  // Method to close customer modal
  closeCustomerModal(): void {
    this.showCustomerModal = false;
    this.isEditMode = false;
    this.customerForm = {
      id: null,
      name: '',
      emailId: '',
      address: '',
      registrationDate: '',
      userIds: []
    };
  }

  //save customer
  saveCustomer(): void {
    if (!this.customerForm.name || !this.customerForm.emailId) {
      this.notify.warn('Please fill required fields');
      return;
    }

    this.loading = true;

    // Create DTO for both create and update
    const input = new CreateOrEditCustomerDto();
    input.id = this.customerForm.id; // Will be null for create, ID for update
    input.name = this.customerForm.name;
    input.emailId = this.customerForm.emailId;
    input.address = this.customerForm.address;
    input.registrationDate = this.customerForm.registrationDate ? new Date(this.customerForm.registrationDate) as any : undefined;
    input.assignedUserIds = this.customerForm.userIds;

    if (this.isEditMode) {
      this._customerService.update(input).subscribe((result) => {
        this.loading = false;
        this.notify.success('Customer updated successfully!');
        this.closeCustomerModal();
        this.loadCustomers(); 
      });
    } else {
      this._customerService.create(input).subscribe((result) => {
        this.loading = false;
        this.notify.success('Customer created successfully!');
        this.closeCustomerModal();
        this.loadCustomers();
      });
    }
  }
}
