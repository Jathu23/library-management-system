import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowaudiobooksComponent } from './showaudiobooks.component';

describe('ShowaudiobooksComponent', () => {
  let component: ShowaudiobooksComponent;
  let fixture: ComponentFixture<ShowaudiobooksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShowaudiobooksComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowaudiobooksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
