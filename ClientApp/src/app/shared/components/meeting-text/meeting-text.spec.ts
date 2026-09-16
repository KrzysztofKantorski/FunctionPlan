import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingText } from './meeting-text';

describe('MeetingText', () => {
  let component: MeetingText;
  let fixture: ComponentFixture<MeetingText>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MeetingText],
    }).compileComponents();

    fixture = TestBed.createComponent(MeetingText);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
