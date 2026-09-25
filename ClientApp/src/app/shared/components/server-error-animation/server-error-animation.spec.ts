import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServerErrorAnimation } from './server-error-animation';

describe('ServerErrorAnimation', () => {
  let component: ServerErrorAnimation;
  let fixture: ComponentFixture<ServerErrorAnimation>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ServerErrorAnimation],
    }).compileComponents();

    fixture = TestBed.createComponent(ServerErrorAnimation);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
