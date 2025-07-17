import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { CustomerServiceProxy, CustomerDto, PagedResultDtoOfCustomerDto } from '@shared/service-proxies/service-proxies';

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
  
  // Pagination variables
  currentPage: number = 1;
  pageSize: number = 10;

  constructor(
    injector: Injector,
    private _customerService: CustomerServiceProxy
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
    console.log('View users for customer:', customer);
    // TODO: Implement view users functionality
    this.notify.info('View users functionality will be implemented later');
    this.selectedCustomer = null; // Close dropdown
  }

  // Method to edit customer
  editCustomer(customer: CustomerDto): void {
    console.log('Edit customer:', customer);
    // TODO: Implement edit functionality
    this.notify.info('Edit functionality will be implemented later');
    this.selectedCustomer = null; // Close dropdown
  }

  // Method to delete customer
  deleteCustomer(customer: CustomerDto): void {
    console.log('Delete customer:', customer);
    // TODO: Implement delete functionality
    this.notify.info('Delete functionality will be implemented later');
    this.selectedCustomer = null; // Close dropdown
  }

  // Helper method to expose Math.min to template
  Math = Math;
}
