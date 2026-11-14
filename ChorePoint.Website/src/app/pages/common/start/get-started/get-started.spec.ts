import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetStarted } from './get-started';

describe('GetStarted', () => {
  let component: GetStarted;
  let fixture: ComponentFixture<GetStarted>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GetStarted]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetStarted);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
