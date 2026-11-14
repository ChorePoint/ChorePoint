import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddShopItem } from './add-shop-item';

describe('AddShopItem', () => {
  let component: AddShopItem;
  let fixture: ComponentFixture<AddShopItem>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddShopItem]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddShopItem);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
