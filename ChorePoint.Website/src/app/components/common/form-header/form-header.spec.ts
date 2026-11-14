import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormHeader } from './form-header';

describe('FormHeader', () => {
  let component: FormHeader;
  let fixture: ComponentFixture<FormHeader>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormHeader]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormHeader);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
