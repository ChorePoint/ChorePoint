import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParentLogin } from './parent-login.component';

describe('Login', () => {
  let component: ParentLogin;
  let fixture: ComponentFixture<ParentLogin>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ParentLogin],
    }).compileComponents();

    fixture = TestBed.createComponent(ParentLogin);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
