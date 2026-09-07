import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoardBackground } from './board-background';

describe('BoardBackground', () => {
  let component: BoardBackground;
  let fixture: ComponentFixture<BoardBackground>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BoardBackground],
    }).compileComponents();

    fixture = TestBed.createComponent(BoardBackground);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
