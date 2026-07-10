import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardFooterMenu } from './dashboard-footer-menu';

describe('DashboardFooterMenu', () => {
  let component: DashboardFooterMenu;
  let fixture: ComponentFixture<DashboardFooterMenu>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DashboardFooterMenu]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DashboardFooterMenu);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
