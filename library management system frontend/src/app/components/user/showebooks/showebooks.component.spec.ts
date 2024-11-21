import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowebooksComponent } from './showebooks.component';

describe('ShowebooksComponent', () => {
  let component: ShowebooksComponent;
  let fixture: ComponentFixture<ShowebooksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShowebooksComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowebooksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
