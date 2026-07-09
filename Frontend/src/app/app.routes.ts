import { Routes } from '@angular/router';
import { ComplaintListComponent } from './pages/complaint-list/complaint-list';
import { LoginComponent } from './auth/login/login';
import { authGuard } from './services/auth-guard';

export const routes: Routes = [
  { path: '', component: LoginComponent },

  {
    path: 'complaints',
    component: ComplaintListComponent,
    canActivate: [authGuard],
  },

  { path: '**', redirectTo: '' }, // any wrong URL → login
];
