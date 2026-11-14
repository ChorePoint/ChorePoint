import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChoreCardWrapper } from './chore-card-wrapper';

describe('ChoreCardWrapper', () => {
  let component: ChoreCardWrapper;
  let fixture: ComponentFixture<ChoreCardWrapper>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChoreCardWrapper]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChoreCardWrapper);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
