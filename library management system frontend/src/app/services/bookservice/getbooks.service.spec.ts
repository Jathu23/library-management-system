import { TestBed } from '@angular/core/testing';

import { GetbooksService } from './getbooks.service';

describe('GetbooksService', () => {
  let service: GetbooksService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetbooksService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
