import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KidProfile } from './kid-profile';

describe('KidProfile', () => {
  let component: KidProfile;
  let fixture: ComponentFixture<KidProfile>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KidProfile]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KidProfile);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
