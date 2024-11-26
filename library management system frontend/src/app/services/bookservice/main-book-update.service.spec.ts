import { TestBed } from '@angular/core/testing';

import { MainBookUpdateService } from './main-book-update.service';

describe('MainBookUpdateService', () => {
  let service: MainBookUpdateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MainBookUpdateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
