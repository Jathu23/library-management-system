import { TestBed } from '@angular/core/testing';

import { StaticsService } from './statics.service';

describe('StaticsService', () => {
  let service: StaticsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StaticsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
