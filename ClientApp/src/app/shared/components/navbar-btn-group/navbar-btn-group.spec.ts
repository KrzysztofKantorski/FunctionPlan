import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavbarBtnGroup } from './navbar-btn-group';

describe('NavbarBtnGroup', () => {
  let component: NavbarBtnGroup;
  let fixture: ComponentFixture<NavbarBtnGroup>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NavbarBtnGroup],
    }).compileComponents();

    fixture = TestBed.createComponent(NavbarBtnGroup);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
