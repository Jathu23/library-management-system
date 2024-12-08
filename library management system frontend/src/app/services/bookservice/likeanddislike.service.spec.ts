import { TestBed } from '@angular/core/testing';

import { LikeanddislikeService } from './likeanddislike.service';

describe('LikeanddislikeService', () => {
  let service: LikeanddislikeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LikeanddislikeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
