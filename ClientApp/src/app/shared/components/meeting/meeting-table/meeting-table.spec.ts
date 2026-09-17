import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingTable } from './meeting-table';

describe('MeetingTable', () => {
  let component: MeetingTable;
  let fixture: ComponentFixture<MeetingTable>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MeetingTable],
    }).compileComponents();

    fixture = TestBed.createComponent(MeetingTable);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
