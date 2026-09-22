import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmptyComments } from './empty-comments';

describe('EmptyComments', () => {
  let component: EmptyComments;
  let fixture: ComponentFixture<EmptyComments>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmptyComments],
    }).compileComponents();

    fixture = TestBed.createComponent(EmptyComments);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
