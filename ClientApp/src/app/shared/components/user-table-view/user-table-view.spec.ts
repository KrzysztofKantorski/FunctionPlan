import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserTableView } from './user-table-view';

describe('UserTableView', () => {
  let component: UserTableView;
  let fixture: ComponentFixture<UserTableView>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserTableView],
    }).compileComponents();

    fixture = TestBed.createComponent(UserTableView);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
