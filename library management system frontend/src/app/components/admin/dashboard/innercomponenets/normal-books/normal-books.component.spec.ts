import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NormalBooksComponent } from './normal-books.component';

describe('NormalBooksComponent', () => {
  let component: NormalBooksComponent;
  let fixture: ComponentFixture<NormalBooksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NormalBooksComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NormalBooksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
