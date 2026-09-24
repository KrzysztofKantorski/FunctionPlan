import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingInfoDialog } from './meeting-info-dialog';

describe('MeetingInfoDialog', () => {
  let component: MeetingInfoDialog;
  let fixture: ComponentFixture<MeetingInfoDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MeetingInfoDialog],
    }).compileComponents();

    fixture = TestBed.createComponent(MeetingInfoDialog);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
