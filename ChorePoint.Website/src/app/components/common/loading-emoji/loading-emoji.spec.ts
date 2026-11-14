import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadingEmoji } from './loading-emoji';

describe('LoadingEmoji', () => {
  let component: LoadingEmoji;
  let fixture: ComponentFixture<LoadingEmoji>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoadingEmoji]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoadingEmoji);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
