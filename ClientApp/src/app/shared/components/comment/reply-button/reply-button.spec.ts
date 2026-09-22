import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReplyButton } from './reply-button';

describe('ReplyButton', () => {
  let component: ReplyButton;
  let fixture: ComponentFixture<ReplyButton>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReplyButton],
    }).compileComponents();

    fixture = TestBed.createComponent(ReplyButton);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
