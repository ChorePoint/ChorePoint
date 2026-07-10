import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KidsSettings } from './kids-settings';

describe('KidsSettings', () => {
  let component: KidsSettings;
  let fixture: ComponentFixture<KidsSettings>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KidsSettings]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KidsSettings);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
