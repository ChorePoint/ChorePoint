import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShopCard } from './shop-card';

describe('ShopCard', () => {
  let component: ShopCard;
  let fixture: ComponentFixture<ShopCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShopCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShopCard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
