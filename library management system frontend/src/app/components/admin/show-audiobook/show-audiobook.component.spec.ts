import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowAudiobookComponent } from './show-audiobook.component';

describe('ShowAudiobookComponent', () => {
  let component: ShowAudiobookComponent;
  let fixture: ComponentFixture<ShowAudiobookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShowAudiobookComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowAudiobookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
