import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KidSelectorHeader } from './kid-selector-header';

describe('KidSelectorHeader', () => {
  let component: KidSelectorHeader;
  let fixture: ComponentFixture<KidSelectorHeader>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KidSelectorHeader]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KidSelectorHeader);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
