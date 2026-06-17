import { AsyncPipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/internal/operators/map';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { Kid } from '../../../../core/types/dtos/kid';
import { Header } from '../../../../shared/components/header/header';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { KidList } from '../../../chores/components/kid-list/kid-list';

@Component({
  selector: 'app-parent-settings',
  imports: [AsyncPipe, LoadingScreen, KidList, Header],
  templateUrl: './parent-settings.html',
  styleUrl: './parent-settings.scss',
})
export class ParentSettings implements OnInit {
  private kidService = inject(KidsService);

  vm$!: Observable<{
    kids: Kid[];
  }>;

  ngOnInit() {
    this.vm$ = this.kidService.getKids$().pipe(
      map((kids) => {
        if (kids != null) {
          return { kids };
        }
        return { kids: [] };
      }),
    );
  }
}
