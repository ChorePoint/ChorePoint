import { AsyncPipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LoadingScreen } from '../../common/loading-screen/loading-screen';
import { KidList } from '../../../components/parent/chores/kid-list/kid-list';
import { Header } from '../../../components/common/header/header';
import { LoginCodeGeneration } from '../../../components/parent/login-code-generation/login-code-generation';
import { KidsService } from '../../../core/services/parent/kids/kids.service';
import { AuthService } from '../../../core/services/common/auth/auth.service';

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

  openLoginCodeModal() {
    this.showLoginCodeModal.set(true);
  }
}
