import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingSubtitle } from './meeting-subtitle';

describe('MeetingSubtitle', () => {
  let component: MeetingSubtitle;
  let fixture: ComponentFixture<MeetingSubtitle>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MeetingSubtitle],
    }).compileComponents();

    fixture = TestBed.createComponent(MeetingSubtitle);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
