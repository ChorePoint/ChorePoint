import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KidsLayout } from './kids-layout';

describe('KidsLayout', () => {
  let component: KidsLayout;
  let fixture: ComponentFixture<KidsLayout>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KidsLayout]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KidsLayout);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
