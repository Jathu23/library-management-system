import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowLentRecodesComponent } from './show-lent-recodes.component';

describe('ShowLentRecodesComponent', () => {
  let component: ShowLentRecodesComponent;
  let fixture: ComponentFixture<ShowLentRecodesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShowLentRecodesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowLentRecodesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
