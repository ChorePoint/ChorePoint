import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KidList } from './kid-list';

describe('KidList', () => {
  let component: KidList;
  let fixture: ComponentFixture<KidList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KidList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KidList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
