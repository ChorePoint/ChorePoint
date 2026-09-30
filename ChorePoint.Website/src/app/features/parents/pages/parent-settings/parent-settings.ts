import { AsyncPipe } from '@angular/common';
import { Component, inject, signal} from '@angular/core';
import { RouterLink } from '@angular/router';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { Header } from '../../../../shared/components/header/header';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { KidList } from '../../../chores/components/kid-list/kid-list';
import {AuthService} from '../../../auth/services/auth.service';
import {LoginCodeGeneration} from '../../components/login-code-generation/login-code-generation';

@Component({
  selector: 'app-parent-settings',
  imports: [AsyncPipe, LoadingScreen, KidList, Header, RouterLink, LoginCodeGeneration],
  templateUrl: './parent-settings.html',
  styleUrl: './parent-settings.scss',
})
export class ParentSettings {
  private kidService = inject(KidsService);
  private authService = inject(AuthService);

  kids = this.kidService.kids;

  showLoginCodeModal = signal(false);

  openLoginCodeModal() { this.showLoginCodeModal.set(true); }
}
