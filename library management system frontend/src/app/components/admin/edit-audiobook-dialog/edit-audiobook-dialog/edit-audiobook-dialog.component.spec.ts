import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditAudiobookDialogComponent } from './edit-audiobook-dialog.component';

describe('EditAudiobookDialogComponent', () => {
  let component: EditAudiobookDialogComponent;
  let fixture: ComponentFixture<EditAudiobookDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditAudiobookDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditAudiobookDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
