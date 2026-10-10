import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserStatsCard } from './user-stats-card';

describe('UserStatsCard', () => {
  let component: UserStatsCard;
  let fixture: ComponentFixture<UserStatsCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserStatsCard],
    }).compileComponents();

    fixture = TestBed.createComponent(UserStatsCard);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
