import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KidSummary } from './kid-summary';

describe('KidSummary', () => {
  let component: KidSummary;
  let fixture: ComponentFixture<KidSummary>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KidSummary]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KidSummary);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
