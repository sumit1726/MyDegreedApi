import { TestBed } from '@angular/core/testing';

import { HumourService } from './humour.service';

describe('HumourService', () => {
  let service: HumourService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HumourService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
