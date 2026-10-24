import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingCreate } from './meeting-create';

describe('MeetingCreate', () => {
  let component: MeetingCreate;
  let fixture: ComponentFixture<MeetingCreate>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MeetingCreate],
    }).compileComponents();

    fixture = TestBed.createComponent(MeetingCreate);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
