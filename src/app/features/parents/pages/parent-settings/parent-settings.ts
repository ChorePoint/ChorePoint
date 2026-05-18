import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/internal/operators/map';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { Kid } from '../../../../core/types/dtos/kid';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { KidList } from '../../../chores/components/kid-list/kid-list';

@Component({
  selector: 'app-parent-settings',
  imports: [AsyncPipe, LoadingScreen, KidList],
  templateUrl: './parent-settings.html',
  styleUrl: './parent-settings.scss',
})
export class ParentSettings {
  private userService = inject(KidsService);

  vm$!: Observable<{
    kids: Kid[];
  }>;

  ngOnInit() {
    this.vm$ = this.userService.getKids().pipe(
      map((kids) => {
        return { kids };
      }),
    );
  }
}
