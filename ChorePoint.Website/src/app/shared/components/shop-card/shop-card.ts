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
import { ShopItem } from '../../../core/types/dtos/shop-item';
import { SHOP_ITEM_STATUS_MAP } from '../../../core/types/enums/shop-item-status';
import { LoadingEmoji } from '../loading-emoji/loading-emoji';
import { DEFAULT_DELETE_STATE } from './const';

@Component({
  selector: 'app-shop-card',
  imports: [TitleCasePipe, LoadingEmoji],
  templateUrl: './shop-card.html',
  styleUrl: './shop-card.scss',
})
export class ShopCard {
  private renderer = inject(Renderer2);

  @Input() shopItem!: ShopItem;
  @Input() deleteLoading = false;

  @Output() deleteEmitter = new EventEmitter<number>();

  @ViewChild('menu') menu!: ElementRef;
  @ViewChild('toggle') toggle!: ElementRef;

  deleteState = DEFAULT_DELETE_STATE;

  openContextMenuId = -1;
  SHOP_ITEM_STATUS_MAP = SHOP_ITEM_STATUS_MAP;

  constructor() {
    this.renderer.listen('window', 'click', (e: Event) => {
      if (!this.menu.nativeElement.contains(e.target) && e.target !== this.toggle.nativeElement) {
        this.openContextMenuId = -1;
      }
    });
  }

  delete() {
    if (!this.deleteState.clicked) {
      this.deleteState = {
        text: 'Are you sure?',
        clicked: true,
      };
    } else {
      this.deleteState = DEFAULT_DELETE_STATE;
      this.deleteEmitter.emit(this.shopItem.id);
    }
  }
}
