import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChoreForm } from './chore-form';

describe('ChoreForm', () => {
  let component: ChoreForm;
  let fixture: ComponentFixture<ChoreForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChoreForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChoreForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
