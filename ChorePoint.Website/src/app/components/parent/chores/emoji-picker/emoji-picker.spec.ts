import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmojiPicker } from './emoji-picker';

describe('EmojiPicker', () => {
  let component: EmojiPicker;
  let fixture: ComponentFixture<EmojiPicker>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmojiPicker]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmojiPicker);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
