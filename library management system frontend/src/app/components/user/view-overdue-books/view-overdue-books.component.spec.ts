import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewOverdueBooksComponent } from './view-overdue-books.component';

describe('ViewOverdueBooksComponent', () => {
  let component: ViewOverdueBooksComponent;
  let fixture: ComponentFixture<ViewOverdueBooksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ViewOverdueBooksComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewOverdueBooksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
