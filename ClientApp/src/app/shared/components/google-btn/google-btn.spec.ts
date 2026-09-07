import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GoogleBtn } from './google-btn';

describe('GoogleBtn', () => {
  let component: GoogleBtn;
  let fixture: ComponentFixture<GoogleBtn>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GoogleBtn],
    }).compileComponents();

    fixture = TestBed.createComponent(GoogleBtn);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
