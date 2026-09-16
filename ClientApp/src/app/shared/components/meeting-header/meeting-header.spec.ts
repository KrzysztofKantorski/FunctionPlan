import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingHeader } from './meeting-header';

describe('MeetingHeader', () => {
  let component: MeetingHeader;
  let fixture: ComponentFixture<MeetingHeader>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MeetingHeader],
    }).compileComponents();

    fixture = TestBed.createComponent(MeetingHeader);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
