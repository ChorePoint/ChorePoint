import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PendingApprovals } from './pending-approval';

describe('PendingApprovals', () => {
  let component: PendingApprovals;
  let fixture: ComponentFixture<PendingApprovals>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PendingApprovals],
    }).compileComponents();

    fixture = TestBed.createComponent(PendingApprovals);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
