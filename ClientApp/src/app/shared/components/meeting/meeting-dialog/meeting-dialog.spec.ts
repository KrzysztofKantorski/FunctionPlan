import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingDialog } from './meeting-dialog';

describe('MeetingDialog', () => {
  let component: MeetingDialog;
  let fixture: ComponentFixture<MeetingDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MeetingDialog],
    }).compileComponents();

    fixture = TestBed.createComponent(MeetingDialog);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
