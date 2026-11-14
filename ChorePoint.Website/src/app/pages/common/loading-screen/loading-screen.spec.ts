import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadingScreen } from './loading-screen';

describe('LoadingScreen', () => {
  let component: LoadingScreen;
  let fixture: ComponentFixture<LoadingScreen>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoadingScreen]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoadingScreen);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
