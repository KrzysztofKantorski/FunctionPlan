import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingTableLayout } from './meeting-table-layout';

describe('MeetingTableLayout', () => {
  let component: MeetingTableLayout;
  let fixture: ComponentFixture<MeetingTableLayout>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MeetingTableLayout],
    }).compileComponents();

    fixture = TestBed.createComponent(MeetingTableLayout);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
