import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditCustomerModalComponent } from './create-or-edit-customer-modal.component';

describe('CreateOrEditCustomerModalComponent', () => {
  let component: CreateOrEditCustomerModalComponent;
  let fixture: ComponentFixture<CreateOrEditCustomerModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateOrEditCustomerModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOrEditCustomerModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
