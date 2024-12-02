import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditEbookDialogComponent } from './edit-ebook-dialog.component';

describe('EditEbookDialogComponent', () => {
  let component: EditEbookDialogComponent;
  let fixture: ComponentFixture<EditEbookDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditEbookDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditEbookDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
