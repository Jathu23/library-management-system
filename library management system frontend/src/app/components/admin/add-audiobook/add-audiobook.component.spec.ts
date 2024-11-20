import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAudiobookComponent } from './add-audiobook.component';

describe('AddAudiobookComponent', () => {
  let component: AddAudiobookComponent;
  let fixture: ComponentFixture<AddAudiobookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddAudiobookComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddAudiobookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
