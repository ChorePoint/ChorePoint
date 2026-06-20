import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Shop } from './shop';

describe('Shop', () => {
  let component: Shop;
  let fixture: ComponentFixture<Shop>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Shop]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Shop);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
