import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditChore } from './edit-chore';

describe('EditChore', () => {
  let component: EditChore;
  let fixture: ComponentFixture<EditChore>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditChore]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditChore);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
