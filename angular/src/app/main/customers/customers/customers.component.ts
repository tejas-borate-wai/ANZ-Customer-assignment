import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { CustomerServiceProxy, CustomerDto, PagedResultDtoOfCustomerDto, CreateOrEditCustomerDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent extends AppComponentBase implements OnInit {

  // Variables to store data
  customers: CustomerDto[] = []; // Array to store all customers
  totalCount: number = 0; // Total number of customers
  loading: boolean = false; // Loading state
  searchText: string = ''; // Search input value
  selectedCustomer: CustomerDto | null = null; // Selected customer for dropdown
  showCustomerModal: boolean = false; // Control customer modal visibility
  showUsersModal: boolean = false; // Control view users modal visibility
  
  // Pagination variables
  currentPage: number = 1;
  pageSize: number = 10;
  
  userList = []; // Will be loaded from API
  assignedUserDetails: any[] = []; // Store assigned user details for edit mode
  customerUsers: any[] = []; // Store users for selected customer
userSearchText: string = '';

// Customer form data
customerForm = {
  id: null, // Add ID for edit mode
  name: '',
  emailId: '',
  address: '',
  registrationDate: '',
  userIds: [] as number[],
  
};

isEditMode: boolean = false; // Track if we're editing


  constructor(
    injector: Injector,
    private _customerService: CustomerServiceProxy,
    private _userService: UserServiceProxy
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.loadCustomers(); // Load customers when component starts
  }

  // Method to load customers from API
  loadCustomers(): void {
    this.loading = true; // Show loading
    
    // Calculate skip count for pagination
    const skipCount = (this.currentPage - 1) * this.pageSize;
    
    // Call the API with parameters
    this._customerService.getAll(
      this.searchText || undefined, // filter
      undefined, // sorting
      skipCount, // skipCount
      this.pageSize // maxResultCount
    ).subscribe({
      next: (result: PagedResultDtoOfCustomerDto) => {
        this.customers = result.items || []; // Store customers
        this.totalCount = result.totalCount || 0; // Store total count
        this.loading = false; // Hide loading
      },
      error: (error) => {
        this.loading = false; // Hide loading on error
        this.notify.error('Failed to load customers');
      }
    });
  }

  // Method to search customers
  searchCustomers(): void {
    this.currentPage = 1; // Reset to first page
    this.loadCustomers(); // Load customers with search
  }

  // Method to refresh/reload customers
  refreshCustomers(): void {
    this.searchText = ''; // Clear search
    this.currentPage = 1; // Reset to first page
    this.loadCustomers();
  }

  // Method to change page size
  onPageSizeChange(event: any): void {
    this.pageSize = parseInt(event.target.value);
    this.currentPage = 1; // Reset to first page
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
      this.selectedCustomer = null; // Close if same customer
    } else {
      this.selectedCustomer = customer; // Open for selected customer
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
  // Method to close view users modal
  closeUsersModal(): void {
    this.showUsersModal = false;
  }

  // Method to edit customer
  editCustomer(customer: CustomerDto): void {
    this.selectedCustomer = null; // Close dropdown
    
    // Try to load customer data for editing
    try {
      this._customerService.getCustomerForEdit(customer.id).subscribe({
        next: (result) => {
          this.isEditMode = true;
          this.customerForm = {
            id: result.id,
            name: result.name || '',
            emailId: result.emailId || '',
            address: result.address || '',
            registrationDate: result.registrationDate ? result.registrationDate.toString().split('T')[0] : '',
            userIds: result.assignedUserIds || []
          };
          
          this.loadAllUsers(); // Load all users for edit mode
          this.showCustomerModal = true; // Open modal
        },
        error: (error) => {
          this.notify.error('Failed to load customer data');
          
          // Fallback: Open modal with basic data
          this.isEditMode = true;
          this.customerForm = {
            id: customer.id,
            name: customer.name || '',
            emailId: customer.emailId || '',
            address: customer.address || '',
            registrationDate: customer.registrationDate ? customer.registrationDate.toString().split('T')[0] : '',
            userIds: []
          };
          this.loadAllUsers();
          this.showCustomerModal = true;
        }
      });
    } catch (error) {
      // Simple fallback if method doesn't exist
      this.isEditMode = true;
      this.customerForm = {
        id: customer.id,
        name: customer.name || '',
        emailId: customer.emailId || '',
        address: customer.address || '',
        registrationDate: customer.registrationDate ? customer.registrationDate.toString().split('T')[0] : '',
        userIds: []
      };
      this.loadAllUsers();
      this.showCustomerModal = true;
    }
  }

  // Method to delete customer
  deleteCustomer(customer: CustomerDto): void {
    this.selectedCustomer = null; // Close dropdown
    
    if (confirm(`Are you sure you want to delete customer "${customer.name}"?`)) {
      this.loading = true;
      this._customerService.delete(customer.id).subscribe({
        next: () => {
          this.loading = false;
          this.notify.success('Customer deleted successfully!');
          this.loadCustomers(); // Reload the table
        },
        error: (error) => {
          this.loading = false;
          this.notify.error('Failed to delete customer');
        }
      });
    }
  }

  // Method to open customer modal
  openCustomerModal(): void {
    this.showCustomerModal = true;
    this.loadUnassignedUsers(); // Load users when modal opens
  }

  // Method to load unassigned users
  loadUnassignedUsers(): void {
    this._customerService.getUnassignedUsers().subscribe({
      next: (result) => {
        this.userList = result.items.map(item => ({
          id: item.value,
          name: item.name
        }));
      },
      error: (error) => {
        this.notify.error('Failed to load users');
      }
    });
  }

  // Method to load all users (for edit mode)
  loadAllUsers(): void {
    // For edit mode, we need to load all users so assigned users show in dropdown
    this._customerService.getUnassignedUsers().subscribe({
      next: (result) => {
        // Start with unassigned users
        this.userList = result.items.map(item => ({
          id: item.value,
          name: item.name
        }));
        
        // If we have assigned userIds, fetch their actual names
        if (this.customerForm.userIds && this.customerForm.userIds.length > 0) {
          // Create array of API calls to get user details
          const userDetailCalls = this.customerForm.userIds.map(userId => {
            const existsInList = this.userList.find(user => user.id === userId);
            if (!existsInList) {
              return this._userService.getUserForEdit(userId);
            }
            return null;
          }).filter(call => call !== null);

          // If we have calls to make, execute them
          if (userDetailCalls.length > 0) {
            forkJoin(userDetailCalls).subscribe({
              next: (userDetails) => {
                // Add the actual user details to the list
                userDetails.forEach(userDetail => {
                  if (userDetail && userDetail.user) {
                    this.userList.push({
                      id: userDetail.user.id,
                      name: userDetail.user.name || userDetail.user.userName || `User ${userDetail.user.id}`
                    });
                  }
                });
              },
              error: (error) => {
                // Fallback: add users with ID-based names
                this.customerForm.userIds.forEach(userId => {
                  const existsInList = this.userList.find(user => user.id === userId);
                  if (!existsInList) {
                    this.userList.push({
                      id: userId,
                      name: `User ${userId}`
                    });
                  }
                });
              }
            });
          }
        }
      },
      error: (error) => {
        this.notify.error('Failed to load users');
      }
    });
  }

  // Method to close customer modal
  closeCustomerModal(): void {
    this.showCustomerModal = false;
    this.isEditMode = false;
    // Reset form when closing
    this.customerForm = {
      id: null,
      name: '',
      emailId: '',
      address: '',
      registrationDate: '',
      userIds: []
    };
  }

  // Method to save customer
  saveCustomer(): void {
    // Simple validation
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

    // Call create or update based on mode
    if (this.isEditMode) {
      // Update existing customer
      this._customerService.update(input).subscribe({
        next: (result) => {
          this.loading = false;
          this.notify.success('Customer updated successfully!');
          this.closeCustomerModal();
          this.loadCustomers(); // Reload the table
        },
        error: (error) => {
          this.loading = false;
          this.notify.error('Failed to update customer');
        }
      });
    } else {
      // Create new customer
      this._customerService.create(input).subscribe({
        next: (result) => {
          this.loading = false;
          this.notify.success('Customer created successfully!');
          this.closeCustomerModal();
          this.loadCustomers(); // Reload the table
        },
        error: (error) => {
          this.loading = false;
          this.notify.error('Failed to create customer');
        }
      });
    }
  }
}
