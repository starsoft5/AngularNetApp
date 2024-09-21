import { TestBed } from '@angular/core/testing';

import { RegisterMemberService } from './register-member.service';

describe('RegisterMemberService', () => {
  let service: RegisterMemberService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RegisterMemberService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
