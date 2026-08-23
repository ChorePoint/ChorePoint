import { TitleCasePipe } from '@angular/common';
import {
  AfterViewInit,
  Component,
  ElementRef,
  EventEmitter,
  inject,
  Input,
  Output,
  Renderer2,
  ViewChild,
} from '@angular/core';
import { ShopItemCard } from '../../../core/types/dtos/shop-item';
import { SHOP_ITEM_STATUS_MAP } from '../../../core/types/enums/shop-item-status';
import { LoadingEmoji } from '../loading-emoji/loading-emoji';
import { DEFAULT_DELETE_STATE } from './const';

@Component({
  selector: 'app-shop-card',
  imports: [TitleCasePipe, LoadingEmoji],
  templateUrl: './shop-card.html',
  styleUrl: './shop-card.scss',
})
export class ShopCard implements AfterViewInit {
  private renderer = inject(Renderer2);

  @Input() shopItem!: ShopItemCard;
  @Input() deleteLoading = false;

  @Output() deleteEmitter = new EventEmitter<number>();

  @ViewChild('menu') menu!: ElementRef;
  @ViewChild('toggle') toggle!: ElementRef;

  deleteState = DEFAULT_DELETE_STATE;

  contextMenuOpen = false;
  SHOP_ITEM_STATUS_MAP = SHOP_ITEM_STATUS_MAP;

  ngAfterViewInit() {
    this.renderer.listen('window', 'click', (e: MouseEvent) => {
      const target = e.target as Node;

      if (target == this.toggle.nativeElement) {
        this.contextMenuOpen = !this.contextMenuOpen;
      }

      if (
        this.contextMenuOpen &&
        !this.menu.nativeElement.contains(target) &&
        !this.toggle.nativeElement.contains(target)
      ) {
        this.contextMenuOpen = false;
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
      this.deleteEmitter.emit(this.shopItem.shopItemId);
    }
  }
}
