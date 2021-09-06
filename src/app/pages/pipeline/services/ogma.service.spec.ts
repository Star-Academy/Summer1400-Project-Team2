import { TestBed } from '@angular/core/testing';

import { OgmaService } from './ogma.service';

describe('OgmaService', () => {
  let service: OgmaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OgmaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
