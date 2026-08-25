import { TitleCasePipe } from '@angular/common';
import {
  Component,
  ElementRef,
  EventEmitter,
  inject,
  Input,
  Output,
  Renderer2,
  ViewChild,
} from '@angular/core';
import { RouterLink } from '@angular/router';
import { Chore } from '../../../core/types/dtos/chore';
import { Kid } from '../../../core/types/dtos/kid';
import { LoadingAction, LoadingType } from '../../types/loading-action';
import { TimeFrame } from '../../types/timeframe';
import { LoadingEmoji } from '../loading-emoji/loading-emoji';
import { DEFAULT_DELETE_STATE } from '../shop-card/const';

@Component({
  selector: 'app-chore-card',
  imports: [TitleCasePipe, RouterLink, LoadingEmoji],
  templateUrl: './chore-card.html',
  styleUrl: './chore-card.scss',
})
export class ChoreCard {
  private renderer = inject(Renderer2);

  @ViewChild('menu') menu!: ElementRef;
  @ViewChild('toggle') toggle!: ElementRef;

  @Input() chore!: Chore;
  @Input() kidsDictionary!: Record<number, Kid>;
  @Input() timeframe!: TimeFrame;
  @Input() loadingAction: LoadingAction | null = null;

  @Output() deleteEmitter = new EventEmitter<Chore>();
  @Output() toggleActiveEmitter = new EventEmitter<Chore>();

  deleteState = DEFAULT_DELETE_STATE;

  LoadingType = LoadingType;

  menuOpen = false;

  constructor() {
    this.renderer.listen('window', 'click', (e: Event) => {
      if (!this.menu.nativeElement.contains(e.target) && e.target !== this.toggle.nativeElement) {
        this.menuOpen = false;
      }
    });
  }

  toggleMenu() {
    this.menuOpen = !this.menuOpen;
  }

  toggleActive() {
    this.toggleActiveEmitter.emit(this.chore);
  }

  delete() {
    if (!this.deleteState.clicked) {
      this.deleteState = {
        text: 'Are you sure?',
        clicked: true,
      };
    } else {
      this.deleteEmitter.emit(this.chore);
    }
  }

  closeMenu() {
    this.menuOpen = false;
  }

  getAssignedKids() {
    return this.chore.assignedKids.length === 0
      ? 'No kids assigned'
      : this.chore.assignedKids.map((ak) => this.kidsDictionary[ak.kidId].name).join(', ');
  }
}
