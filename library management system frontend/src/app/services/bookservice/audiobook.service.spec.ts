import { TestBed } from '@angular/core/testing';

import { AudiobookService } from './audiobook.service';

describe('AudiobookService', () => {
  let service: AudiobookService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AudiobookService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
