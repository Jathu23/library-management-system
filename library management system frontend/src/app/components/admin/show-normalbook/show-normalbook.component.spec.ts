import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowNormalbookComponent } from './show-normalbook.component';

describe('ShowNormalbookComponent', () => {
  let component: ShowNormalbookComponent;
  let fixture: ComponentFixture<ShowNormalbookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShowNormalbookComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowNormalbookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
