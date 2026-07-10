import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditKidForm } from './edit-kid-form';

describe('EditKidForm', () => {
  let component: EditKidForm;
  let fixture: ComponentFixture<EditKidForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditKidForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditKidForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
