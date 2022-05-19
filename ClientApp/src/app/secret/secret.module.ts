import {
  NgModule
} from '@angular/core';
import {
  CommonModule
} from '@angular/common';
import {
  SecretRoutingModule
} from './secret-routing.module';
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
  FormsModule
} from '@angular/forms';
import {
  UserService
} from './service/user.service';
import {
  RecordService
} from './service/record.service';
import {
  SecretComponent
} from './secret.component';
import {
  DatePipe
} from '@angular/common';
import {
  NavbarModule,
  WavesModule,
  ButtonsModule
} from 'angular-bootstrap-md';
import {
  FilterPipe
} from './service/filter.pipe'
import {
  AuthService
} from '../auth/auth.service';


@NgModule({
  declarations: [
    UserComponent,
    RecordComponent,
    HeaderComponent,
    FooterComponent,
    HeaderComponent,
    FooterComponent,
    SecretComponent,
    FilterPipe
  ],
  imports: [
    CommonModule,
    SecretRoutingModule,
    FormsModule,
    NavbarModule,
    WavesModule,
    ButtonsModule
  ],
  providers: [
    UserService,
    RecordService,
    DatePipe,
    HeaderComponent,
    AuthService
  ],
})
export class SecretModule {}
