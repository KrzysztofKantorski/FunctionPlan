import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingInfo } from './meeting-info';

describe('MeetingInfo', () => {
  let component: MeetingInfo;
  let fixture: ComponentFixture<MeetingInfo>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MeetingInfo],
    }).compileComponents();

    fixture = TestBed.createComponent(MeetingInfo);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
