import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowLendingHistoryComponent } from './show-lending-history.component';

describe('ShowLendingHistoryComponent', () => {
  let component: ShowLendingHistoryComponent;
  let fixture: ComponentFixture<ShowLendingHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShowLendingHistoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowLendingHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
