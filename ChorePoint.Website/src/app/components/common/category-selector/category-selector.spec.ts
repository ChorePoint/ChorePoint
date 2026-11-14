import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategorySelector } from './category-selector';

describe('CategorySelector', () => {
  let component: CategorySelector;
  let fixture: ComponentFixture<CategorySelector>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CategorySelector]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CategorySelector);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
