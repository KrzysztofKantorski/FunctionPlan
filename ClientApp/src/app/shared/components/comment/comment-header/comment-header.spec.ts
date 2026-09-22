import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommentHeader } from './comment-header';

describe('CommentHeader', () => {
  let component: CommentHeader;
  let fixture: ComponentFixture<CommentHeader>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommentHeader],
    }).compileComponents();

    fixture = TestBed.createComponent(CommentHeader);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
