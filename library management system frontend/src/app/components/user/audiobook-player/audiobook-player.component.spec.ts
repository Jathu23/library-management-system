import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AudiobookPlayerComponent } from './audiobook-player.component';

describe('AudiobookPlayerComponent', () => {
  let component: AudiobookPlayerComponent;
  let fixture: ComponentFixture<AudiobookPlayerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AudiobookPlayerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AudiobookPlayerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
