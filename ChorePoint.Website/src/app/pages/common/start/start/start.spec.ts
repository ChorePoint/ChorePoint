import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Start } from './start';

describe('Start', () => {
  let component: Start;
  let fixture: ComponentFixture<Start>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Start]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Start);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
