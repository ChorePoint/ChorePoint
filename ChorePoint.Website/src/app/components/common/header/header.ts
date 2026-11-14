import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { HeaderConfig } from './types';

@Component({
  selector: 'app-header',
  imports: [RouterLink],
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header {
  @Input() config!: HeaderConfig;
}
