import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { Header } from '../../../../shared/components/header/header';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { KidList } from '../../../chores/components/kid-list/kid-list';

@Component({
  selector: 'app-parent-settings',
  imports: [AsyncPipe, LoadingScreen, KidList, Header, RouterLink],
  templateUrl: './parent-settings.html',
  styleUrl: './parent-settings.scss',
})
export class ParentSettings {
  private kidService = inject(KidsService);

  kids = this.kidService.kids$;
}
