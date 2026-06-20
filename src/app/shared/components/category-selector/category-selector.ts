import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CATEGORIES } from '../../../core/consts/categories';
import { Category } from '../../../core/types/category';

@Component({
  selector: 'app-category-selector',
  imports: [],
  templateUrl: './category-selector.html',
  styleUrl: './category-selector.scss',
})
export class CategorySelector {
  @Input() selectedCategory = CATEGORIES[0].name;
  @Input() categories = CATEGORIES;

  @Output() selectedCategoryChanged = new EventEmitter<Category>();
}
