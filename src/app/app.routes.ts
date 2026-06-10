import { Routes } from '@angular/router';
import { authGuard } from './features/auth/guards/auth-guard';
import { CreateAccount } from './features/auth/pages/create-account/create-account';
import { Login } from './features/auth/pages/login/login';
import { AddChore } from './features/chores/pages/add-chore/add-chore';
import { ChoreDetails } from './features/chores/pages/chore-details/chore-details';
import { EditChore } from './features/chores/pages/edit-chore/edit-chore';
import { DashboardLayout } from './features/parents/dashboard-layout/dashboard-layout';
import { ChoreView } from './features/parents/pages/chore-view/chore-view';
import { DashboardHome } from './features/parents/pages/dashboard-home/dashboard-home';
import { KidsSettings } from './features/parents/pages/kids-settings/kids-settings';
import { ParentSettings } from './features/parents/pages/parent-settings/parent-settings';
import { CreateProfile } from './features/start/pages/create-profile/create-profile';
import { Start } from './features/start/pages/start/start';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: Start, canActivate: [authGuard] },
  {
    path: 'dashboard',
    component: DashboardLayout,
    canActivate: [authGuard],
    children: [
      { path: 'home', component: DashboardHome, pathMatch: 'full' },
      { path: 'chores', component: ChoreView },
      { path: 'chores/add', component: AddChore },
      { path: 'chores/edit/:id', component: EditChore },
      {
        path: 'kids',
        component: KidsSettings,
      },
      { path: 'kids/add', component: CreateProfile },
      { path: 'settings', component: ParentSettings },
      { path: '', redirectTo: 'home', pathMatch: 'full' },
    ],
  },
  { path: 'chore/:id', component: ChoreDetails, canActivate: [authGuard] },
  { path: 'login', component: Login },
  { path: 'create-account', component: CreateAccount },
];
