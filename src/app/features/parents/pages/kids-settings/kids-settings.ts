import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { UserService } from '../../../../core/services/user/users.service';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { User } from '../../../kids/models/user';
import { Leaderboard } from '../../components/leaderboard/leaderboard';

@Component({
  selector: 'app-kids-settings',
  imports: [AsyncPipe, LoadingScreen, RouterLink, Leaderboard],
  templateUrl: './kids-settings.html',
  styleUrl: './kids-settings.scss',
})
export class KidsSettings {
  private userService = inject(UserService);

  vm$!: Observable<{
    kids: User[];
  }>;

  ngOnInit() {
    this.vm$ = this.userService.getKids().pipe(map((kids) => ({ kids })));
  }
}
