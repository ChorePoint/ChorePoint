import { Component } from '@angular/core';
import { AVATARS } from '../../../../core/consts/avatars';

@Component({
  selector: 'app-create-profile',
  templateUrl: './create-profile.html',
  styleUrl: './create-profile.scss',
})
export class CreateProfile {
  avatars = AVATARS;
  selectedAvatar: string = this.avatars[0].emoji;

  selectAvatar(avatar: string) {
    this.selectedAvatar = avatar;
  }

  back() {
    window.history.back();
  }

  submit() {
    console.log('Form submitted');
  }
}
