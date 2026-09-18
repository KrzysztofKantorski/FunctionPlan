import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingOrganized } from './meeting-organized';

describe('MeetingOrganized', () => {
  let component: MeetingOrganized;
  let fixture: ComponentFixture<MeetingOrganized>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MeetingOrganized],
    }).compileComponents();

    fixture = TestBed.createComponent(MeetingOrganized);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
