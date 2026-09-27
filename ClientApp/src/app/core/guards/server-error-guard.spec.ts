import { TestBed } from '@angular/core/testing';
import { CanDeactivateFn } from '@angular/router';

import { serverErrorGuard } from './server-error-guard';

describe('serverErrorGuard', () => {
  const executeGuard: CanDeactivateFn<unknown> = (...guardParameters) =>
    TestBed.runInInjectionContext(() => serverErrorGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
