import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PasswordInput } from './password-input';

describe('PasswordInput', () => {
  let component: PasswordInput;
  let fixture: ComponentFixture<PasswordInput>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PasswordInput]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PasswordInput);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
