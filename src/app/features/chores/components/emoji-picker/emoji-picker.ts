import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CHORE_EMOJIS } from '../../../../consts/chore-emojis';

@Component({
  selector: 'app-emoji-picker',
  imports: [],
  templateUrl: './emoji-picker.html',
  styleUrl: './emoji-picker.scss',
})
export class EmojiPicker {
  @Input() selectedEmoji: string | undefined;

  @Output() selectedEmojiChange = new EventEmitter<string>();

  choreEmojis = CHORE_EMOJIS;
}
