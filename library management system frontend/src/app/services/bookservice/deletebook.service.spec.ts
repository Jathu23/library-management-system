import { TestBed } from '@angular/core/testing';

import { BookDeleteServicesService } from './deletebook.service';

describe('BookDeleteServicesService', () => {
  let service: BookDeleteServicesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BookDeleteServicesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
