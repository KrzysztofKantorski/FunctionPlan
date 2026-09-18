import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingAccepted } from './meeting-accepted';

describe('MeetingAccepted', () => {
  let component: MeetingAccepted;
  let fixture: ComponentFixture<MeetingAccepted>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MeetingAccepted],
    }).compileComponents();

    fixture = TestBed.createComponent(MeetingAccepted);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
