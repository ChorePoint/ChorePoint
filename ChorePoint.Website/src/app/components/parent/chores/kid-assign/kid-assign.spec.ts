import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KidAssign } from './kid-assign';

describe('KidAssign', () => {
  let component: KidAssign;
  let fixture: ComponentFixture<KidAssign>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KidAssign]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KidAssign);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
