import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowLentHistoryComponent } from './show-lent-history.component';

describe('ShowLentHistoryComponent', () => {
  let component: ShowLentHistoryComponent;
  let fixture: ComponentFixture<ShowLentHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShowLentHistoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowLentHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
