import {
  NgModule
} from '@angular/core';
import {
  Routes,
  RouterModule
} from '@angular/router';
import {
  UserComponent
} from './components/user/user.component';
import {
  RecordComponent
} from './components/record/record.component';
import {
  HeaderComponent
} from './components/fragments/header/header.component';
import {
  FooterComponent
} from './components/fragments/footer/footer.component';
import {
  SecretComponent
} from './secret.component';

const routes: Routes = [
  {
    path: 'users',
    component: UserComponent
  },
  {
    path: 'records',
    component: RecordComponent
  },
  {
    path: 'header',
    component: HeaderComponent
  },
  {
    path: 'footer',
    component: FooterComponent
  },
  {
    path: 'secret',
    component: SecretComponent
  }

  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SecretRoutingModule {}
