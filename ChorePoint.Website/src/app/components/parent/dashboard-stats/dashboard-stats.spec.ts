import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardStats } from './dashboard-stats';

describe('DashboardStats', () => {
  let component: DashboardStats;
  let fixture: ComponentFixture<DashboardStats>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DashboardStats]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DashboardStats);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
