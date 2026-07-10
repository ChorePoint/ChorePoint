import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChoreCard } from './chore-card';

describe('ChoreCard', () => {
  let component: ChoreCard;
  let fixture: ComponentFixture<ChoreCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChoreCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChoreCard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
