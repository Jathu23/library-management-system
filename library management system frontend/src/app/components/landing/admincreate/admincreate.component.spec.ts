import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdmincreateComponent } from './admincreate.component';

describe('AdmincreateComponent', () => {
  let component: AdmincreateComponent;
  let fixture: ComponentFixture<AdmincreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdmincreateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdmincreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
