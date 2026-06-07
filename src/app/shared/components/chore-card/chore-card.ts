import { TitleCasePipe } from '@angular/common';
import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  Output,
  Renderer2,
  ViewChild,
} from '@angular/core';
import { RouterLink } from '@angular/router';
import { Chore } from '../../../core/types/dtos/chore';
import { Kid } from '../../../core/types/dtos/kid';
import { LoadingAction } from '../../types/loading-action';
import { LoadingEmoji } from '../loading-emoji/loading-emoji';

@Component({
  selector: 'app-chore-card',
  imports: [TitleCasePipe, RouterLink, LoadingEmoji],
  templateUrl: './chore-card.html',
  styleUrl: './chore-card.scss',
})
export class ChoreCard {
  @ViewChild('menu') menu!: ElementRef;
  @ViewChild('toggle') toggle!: ElementRef;

  @Input() chore!: Chore;
  @Input() kidsDictionary!: Record<number, Kid>;
  @Input() timeframe!: 'daily' | 'weekly' | 'bonus';
  @Input() loadingAction: LoadingAction | null = null;

  @Output() deleteEmitter = new EventEmitter<Chore>();
  @Output() toggleActiveEmitter = new EventEmitter<Chore>();

  menuOpen = -1;

  constructor(private renderer: Renderer2) {
    this.renderer.listen('window', 'click', (e: Event) => {
      if (!this.menu.nativeElement.contains(e.target) && e.target !== this.toggle.nativeElement) {
        this.menuOpen = -1;
      }
    });
  }

  toggleMenu(choreId: number) {
    this.menuOpen = this.menuOpen === choreId ? -1 : choreId;
  }

  toggleActive(chore: Chore) {
    this.toggleActiveEmitter.emit(chore);
  }

  delete(chore: Chore) {
    this.deleteEmitter.emit(chore);
  }

  closeMenu() {
    this.menuOpen = -1;
  }
}
