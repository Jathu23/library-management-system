import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AudiobookDetailsComponent } from './audiobook-details.component';

describe('AudiobookDetailsComponent', () => {
  let component: AudiobookDetailsComponent;
  let fixture: ComponentFixture<AudiobookDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AudiobookDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AudiobookDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
