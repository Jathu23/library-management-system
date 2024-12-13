import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EBookComponent } from './ebook.component';

describe('EBookComponent', () => {
  let component: EBookComponent;
  let fixture: ComponentFixture<EBookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EBookComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
