import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowLentRecComponent } from './show-lent-rec.component';

describe('ShowLentRecComponent', () => {
  let component: ShowLentRecComponent;
  let fixture: ComponentFixture<ShowLentRecComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShowLentRecComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowLentRecComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
