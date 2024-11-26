import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowEbookComponent } from './show-ebook.component';

describe('ShowEbookComponent', () => {
  let component: ShowEbookComponent;
  let fixture: ComponentFixture<ShowEbookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShowEbookComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowEbookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
