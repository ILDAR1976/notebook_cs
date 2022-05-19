import {
  NgModule
} from '@angular/core';
import {
  RouterModule,
  Routes
} from '@angular/router';
import {
  LoginPageComponent
} from './auth/login-page/login-page.component';
import {
  RegisterPageComponent
} from './auth/register-page/register-page.component';
import {
  AuthGuard
} from './auth/auth.guard';
import {
  ErrorPageComponent
} from './error-page/error-page.component';

const routes: Routes = [
  {
    path: 'main',
    canLoad: [AuthGuard],
    loadChildren: () => import('./secret/secret.module').then(m => m.SecretModule)
  },
  {
    path: 'login',
    component: LoginPageComponent,
  },
  {
    path: 'register',
    component: RegisterPageComponent,
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: '**',
    component: ErrorPageComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
