import { Component, Input } from '@angular/core';
import { Kid } from '../../../../core/types/dtos/kid';

@Component({
  selector: 'app-kid-list',
  imports: [],
  templateUrl: './kid-list.html',
  styleUrl: './kid-list.scss',
})
export class KidList {
  @Input() kids!: Kid[];
}
