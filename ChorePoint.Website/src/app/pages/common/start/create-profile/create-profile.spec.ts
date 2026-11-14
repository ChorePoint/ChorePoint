import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateProfile } from './create-profile';

describe('CreateProfile', () => {
  let component: CreateProfile;
  let fixture: ComponentFixture<CreateProfile>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateProfile]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateProfile);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
