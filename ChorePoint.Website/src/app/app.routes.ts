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
import { KidsSettings } from './pages/parent/kids-settings/kids-settings';
import { EditKidForm } from './pages/parent/edit-kid-form/edit-kid-form';
import { KidLogin } from './pages/kid/login/kid-login';
import { KidDashboardHome } from './pages/kid/dashboard-home/dashboard-home';
import { ChoreDashboard } from './pages/parent/chore/chore-dashboard/chore-dashboard';
import { CreateProfile } from './pages/common/start/create-profile/create-profile';
import { ChoreDetails } from './pages/parent/chore/chore-details/chore-details';
import { ParentLogin } from './pages/parent/login/parent-login.component';
import { CreateAccount } from './pages/parent/create-account/create-account';

export const routes: Routes = [
  { path: '', redirectTo: 'parent/home', pathMatch: 'full' },
  { path: 'home', component: Start, canActivate: [authGuard] },
  { path: 'welcome', component: Welcome, canActivate: [authGuard] },

  // Auth
  { path: 'kid/login', component: KidLogin },
  { path: 'parent/login', component: ParentLogin },
  { path: 'create-account', component: CreateAccount },
  // Parent
  {
    path: 'parent',
    component: DashboardLayout,
    canActivate: [authGuard, HasKidsGuard],
    children: [
      { path: 'home', component: DashboardHome, pathMatch: 'full' },
      // Chores
      { path: 'chores', component: ChoreView },
      { path: 'chores/add', component: AddChore },
      { path: 'chores/edit/:id', component: EditChore },
      // Shop
      { path: 'shop', component: Shop },
      { path: 'shop/add', component: AddShopItem },
      { path: 'shop/edit/:id', component: EditShopItem },
      // Kids
      { path: 'kids', component: KidsSettings },
      { path: 'kids/add', component: CreateProfile },
      { path: 'kids/edit/:id', component: EditKidForm },
      // Other
      { path: 'settings', component: ParentSettings },
      { path: '', redirectTo: 'parent/home', pathMatch: 'full' },
    ],
  },
  // Kid
  {
    path: 'kid',
    component: KidDashboardHome,
  },
];
