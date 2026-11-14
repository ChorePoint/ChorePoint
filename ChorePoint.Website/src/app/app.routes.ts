import { Routes } from '@angular/router';
import { Start } from './pages/common/start/start/start';
import { authGuard } from './core/guards/common/auth-guard';
import { Welcome } from './pages/parent/welcome/welcome';
import { DashboardLayout } from './layout/parent/dashboard-layout/dashboard-layout';
import { HasKidsGuard } from './core/guards/parent/has-kids-guard';
import { DashboardHome } from './pages/parent/dashboard-home/dashboard-home';
import { ChoreView } from './pages/parent/chore/chore-view/chore-view';
import { AddChore } from './pages/parent/chore/add-chore/add-chore';
import { EditChore } from './pages/parent/chore/edit-chore/edit-chore';
import { Shop } from './pages/parent/shop/shop';
import { AddShopItem } from './components/parent/shop/add-shop-item/add-shop-item';
import { EditShopItem } from './components/parent/shop/edit-shop-item/edit-shop-item';
import { ParentSettings } from './pages/parent/parent-settings/parent-settings';
import { KidsLayout } from './layout/parent/kids-layout/kids-layout';
import { KidsSettings } from './pages/parent/kids-settings/kids-settings';
import { EditKidForm } from './pages/parent/edit-kid-form/edit-kid-form';
import { KidLogin } from './pages/kid/login/kid-login';
import { KidDashboardHome } from './pages/kid/dashboard-home/dashboard-home';
import { ChoreDashboard } from './pages/parent/chore/chore-dashboard/chore-dashboard';
import { CreateProfile } from './pages/common/start/create-profile/create-profile';
import { ChoreDetails } from './pages/parent/chore/chore-details/chore-details';
import { Login } from './pages/parent/login/login';
import { CreateAccount } from './pages/parent/create-account/create-account';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: Start, canActivate: [authGuard] },
  { path: 'welcome', component: Welcome, canActivate: [authGuard] },
  {
    path: 'dashboard',
    component: DashboardLayout,
    canActivate: [authGuard, HasKidsGuard],
    children: [
      { path: 'home', component: DashboardHome, pathMatch: 'full' },
      { path: 'chores', component: ChoreView },
      { path: 'chores/add', component: AddChore },
      { path: 'chores/edit/:id', component: EditChore },
      { path: 'shop', component: Shop },
      { path: 'shop/add', component: AddShopItem },
      { path: 'shop/edit/:id', component: EditShopItem },
      { path: 'settings', component: ParentSettings },
      { path: '', redirectTo: 'home', pathMatch: 'full' },
    ],
  },
  {
    path: 'kids',
    component: KidsLayout,
    canActivate: [authGuard, HasKidsGuard],
    children: [
      { path: 'home', component: KidsSettings, pathMatch: 'full' },
      { path: 'edit/:id', component: EditKidForm },
    ],
  },
  {
    path: 'kid/login',
    component: KidLogin,
  },
  { path: 'kid/test', component: KidDashboardHome },
  { path: 'test', component: ChoreDashboard },
  { path: 'kids/add', component: CreateProfile, canActivate: [authGuard] },
  { path: 'chore/:id', component: ChoreDetails, canActivate: [authGuard, HasKidsGuard] },
  { path: 'login', component: Login },
  { path: 'create-account', component: CreateAccount },
];
