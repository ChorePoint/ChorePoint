import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParentSettings } from './parent-settings';

describe('ParentSettings', () => {
  let component: ParentSettings;
  let fixture: ComponentFixture<ParentSettings>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ParentSettings]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParentSettings);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
