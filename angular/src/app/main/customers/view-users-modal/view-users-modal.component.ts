import { Component, Injector, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/common/app-component-base';
import { CustomerServiceProxy, UserListDto, UserServiceProxy, GetUsersInput } from '@shared/service-proxies/service-proxies';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-view-users-modal',
  templateUrl: './view-users-modal.component.html',
  styleUrls: ['./view-users-modal.component.css']
})
export class ViewUsersModalComponent extends AppComponentBase implements OnInit {
  customerId: number;
  customerName: string;
  assignedUsers: UserListDto[] = [];
  isLoading = false;

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    private _customerService: CustomerServiceProxy,
    private _userService: UserServiceProxy,
    private http: HttpClient
  ) {
    super(injector);
  }

  ngOnInit(): void {
    console.log('ViewUsersModalComponent ngOnInit called');
    console.log('Customer ID:', this.customerId);
    console.log('Customer Name:', this.customerName);
    
    // Check network connectivity and backend URL
    console.log('🌐 Current location:', window.location.href);
    console.log('🌐 Origin:', window.location.origin);
    console.log('🌐 Expected backend URL: https://localhost:44301');
    
    // Test if the API endpoint exists by making a simple call
    this.testApiEndpoint();
    
    this.loadAssignedUsers();
  }

  public testApiEndpoint(): void {
    console.log('🧪 Testing API endpoint availability...');
    
    // Test with a simple GET request to the base API
    this.http.get('/api/services/app/Customer/GetCustomers?MaxResultCount=1').subscribe(
      response => {
        console.log('✅ Base Customer API is working:', response);
      },
      error => {
        console.error('❌ Base Customer API error:', error);
      }
    );
    
    // Test the specific endpoint
    if (this.customerId) {
      const testUrl = `/api/services/app/Customer/GetUsersByCustomerId?customerId=${this.customerId}`;
      console.log('🧪 Testing specific endpoint:', testUrl);
      
      this.http.get(testUrl).subscribe(
        response => {
          console.log('✅ Specific endpoint is working:', response);
        },
        error => {
          console.error('❌ Specific endpoint error:', error);
        }
      );
    }
  }

  public testServiceProxy(): void {
    console.log('🧪 Testing service proxy method...');
    
    if (this._customerService && this._customerService.getUsersByCustomerId) {
      console.log('✅ Service proxy method exists');
      
      if (this.customerId) {
        console.log('🚀 Calling service proxy method with ID:', this.customerId);
        this._customerService.getUsersByCustomerId(this.customerId).subscribe(
          result => {
            console.log('✅ Service proxy result:', result);
          },
          error => {
            console.error('❌ Service proxy error:', error);
          }
        );
      }
    } else {
      console.error('❌ Service proxy method does not exist');
    }
  }

  loadAssignedUsers(): void {
    if (!this.customerId) {
      console.error('❌ No customerId provided');
      return;
    }

    console.log('🔍 Loading users for customer ID:', this.customerId);
    console.log('🔍 Customer name:', this.customerName);
    this.isLoading = true;
    
    // Try using the service proxy method first
    console.log('🔍 Checking if getUsersByCustomerId method exists...');
    console.log('🔍 _customerService:', this._customerService);
    console.log('🔍 getUsersByCustomerId method:', this._customerService.getUsersByCustomerId);
    
    if (this._customerService.getUsersByCustomerId) {
      console.log('✅ Using service proxy method');
      console.log('🚀 Making API call via service proxy...');
      
      this._customerService.getUsersByCustomerId(this.customerId).subscribe(
        result => {
          console.log('✅ API Response (Service Proxy):', result);
          console.log('✅ Result items:', result.items);
          console.log('✅ Result items length:', result.items ? result.items.length : 0);
          console.log('✅ Result type:', typeof result);
          this.assignedUsers = result.items || [];
          console.log('✅ Assigned users after assignment:', this.assignedUsers);
          this.isLoading = false;
        }, 
        error => {
          console.error('❌ Error with service proxy:', error);
          console.error('❌ Error status:', error.status);
          console.error('❌ Error message:', error.message);
          console.log('🔄 Trying HTTP client fallback...');
          this.tryHttpClient();
        }
      );
    } else {
      console.log('❌ Service proxy method not found, using HTTP client');
      this.tryHttpClient();
    }
  }

  private tryHttpClient(): void {
    const url = `/api/services/app/Customer/GetUsersByCustomerId?customerId=${this.customerId}`;
    console.log('🚀 Making HTTP request to:', url);
    console.log('🚀 Full URL will be:', window.location.origin + url);
    
    this.http.get<any>(url).subscribe(
      response => {
        console.log('✅ HTTP Response:', response);
        console.log('✅ Response result:', response.result);
        console.log('✅ Response result items:', response.result?.items);
        this.assignedUsers = response.result?.items || [];
        console.log('✅ Assigned users from HTTP:', this.assignedUsers);
        this.isLoading = false;
      }, 
      error => {
        console.error('❌ HTTP Error:', error);
        console.error('❌ HTTP Error status:', error.status);
        console.error('❌ HTTP Error message:', error.message);
        console.error('❌ HTTP Error url:', error.url);
        console.log('🔄 Trying fallback method using customer edit API');
        this.tryFallbackMethod();
      }
    );
  }

  private tryFallbackMethod(): void {
    console.log('Using fallback method - getting customer data first');
    
    this._customerService.getCustomerForEdit(this.customerId).subscribe(customer => {
      console.log('Customer data:', customer);
      console.log('Assigned user IDs:', customer.assignedUserIds);
      
      if (customer.assignedUserIds && customer.assignedUserIds.length > 0) {
        // Get all users and filter by assigned user IDs
        const getUsersInput = new GetUsersInput();
        getUsersInput.maxResultCount = 1000;
        getUsersInput.skipCount = 0;
        
        this._userService.getUsers(getUsersInput).subscribe(result => {
          console.log('All users:', result.items);
          this.assignedUsers = result.items.filter(user => 
            customer.assignedUserIds!.includes(user.id)
          );
          console.log('Filtered assigned users:', this.assignedUsers);
          this.isLoading = false;
        }, error => {
          console.error('Error getting users:', error);
          this.assignedUsers = [];
          this.isLoading = false;
          this.message.error('Failed to load assigned users');
        });
      } else {
        console.log('No assigned user IDs found');
        this.assignedUsers = [];
        this.isLoading = false;
      }
    }, error => {
      console.error('Error getting customer data:', error);
      this.assignedUsers = [];
      this.isLoading = false;
      this.message.error('Failed to load customer data');
    });
  }

  close(): void {
    this.bsModalRef.hide();
  }
}
