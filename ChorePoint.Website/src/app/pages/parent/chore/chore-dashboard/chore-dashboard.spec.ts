import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChoreDashboard } from './chore-dashboard';

describe('ChoreDashboard', () => {
  let component: ChoreDashboard;
  let fixture: ComponentFixture<ChoreDashboard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChoreDashboard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChoreDashboard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
