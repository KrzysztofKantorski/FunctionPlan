import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingMap } from './meeting-map';

describe('MeetingMap', () => {
  let component: MeetingMap;
  let fixture: ComponentFixture<MeetingMap>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MeetingMap],
    }).compileComponents();

    fixture = TestBed.createComponent(MeetingMap);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
