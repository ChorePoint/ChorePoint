import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChoreDetails } from './chore-details';

describe('ChoreDetails', () => {
  let component: ChoreDetails;
  let fixture: ComponentFixture<ChoreDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChoreDetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChoreDetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
