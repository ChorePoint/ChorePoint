import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AVATARS } from '../../../../core/consts/avatars';

@Component({
  selector: 'app-create-profile',
  imports: [RouterLink],
  templateUrl: './create-profile.html',
  styleUrl: './create-profile.scss',
})
export class CreateProfile {
  avatars = AVATARS;
  selectedAvatar: string = this.avatars[0].emoji;

  selectAvatar(avatar: string) {
    this.selectedAvatar = avatar;
  }
}
