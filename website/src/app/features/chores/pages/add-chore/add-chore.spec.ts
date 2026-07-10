import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddChore } from './add-chore';

describe('AddChore', () => {
  let component: AddChore;
  let fixture: ComponentFixture<AddChore>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddChore]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddChore);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
