import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent extends AppComponentBase implements OnInit {

  constructor(injector: Injector) {
    super(injector);
  }

  ngOnInit(): void {
    // Component initialization logic will go here
    console.log('CustomersComponent initialized successfully');
  }
}
