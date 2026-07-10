import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhoneFrame } from './phone-frame';

describe('PhoneFrame', () => {
  let component: PhoneFrame;
  let fixture: ComponentFixture<PhoneFrame>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PhoneFrame]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhoneFrame);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
