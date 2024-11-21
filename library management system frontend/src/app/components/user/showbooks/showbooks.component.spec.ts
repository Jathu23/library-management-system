import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowbooksComponent } from './showbooks.component';

describe('ShowbooksComponent', () => {
  let component: ShowbooksComponent;
  let fixture: ComponentFixture<ShowbooksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShowbooksComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowbooksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
