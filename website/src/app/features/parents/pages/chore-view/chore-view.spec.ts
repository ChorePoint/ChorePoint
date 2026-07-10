import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChoreView } from './chore-view';

describe('ChoreView', () => {
  let component: ChoreView;
  let fixture: ComponentFixture<ChoreView>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChoreView]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChoreView);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
