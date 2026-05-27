import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { map, Observable } from 'rxjs';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { Kid } from '../../../../core/types/dtos/kid';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { KidSelectorHeader } from '../../../chores/components/kid-selector-header/kid-selector-header';

@Component({
  selector: 'app-chore-view',
  imports: [KidSelectorHeader, LoadingScreen, AsyncPipe],
  templateUrl: './chore-view.html',
  styleUrl: './chore-view.scss',
})
export class ChoreView {
  private kidService = inject(KidsService);

  vm$!: Observable<{
    kids: Kid[];
    selectedKid: Kid | null;
  }>;

  ngOnInit() {
    this.vm$ = this.kidService.getKids$().pipe(
      map((kids) => ({
        kids,
        selectedKid: kids[0] ?? null,
      })),
    );
  }
}
