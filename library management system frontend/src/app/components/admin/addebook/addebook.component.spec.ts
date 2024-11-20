import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddebookComponent } from './addebook.component';

describe('AddebookComponent', () => {
  let component: AddebookComponent;
  let fixture: ComponentFixture<AddebookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddebookComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddebookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
