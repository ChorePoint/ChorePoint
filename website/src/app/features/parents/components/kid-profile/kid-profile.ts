import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { KidDetails } from '../../pages/kids-settings/types';

@Component({
  selector: 'app-kid-profile',
  imports: [RouterLink],
  templateUrl: './kid-profile.html',
  styleUrl: './kid-profile.scss',
})
export class KidProfile {
  @Input() kid!: KidDetails;
}
