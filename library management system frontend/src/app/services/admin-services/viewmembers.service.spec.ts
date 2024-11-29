import { TestBed } from '@angular/core/testing';

import { ViewmembersService } from './viewmembers.service';

describe('ViewmembersService', () => {
  let service: ViewmembersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ViewmembersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
