import { NgModule } from '@angular/core';

import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { SharedModule } from '../shared/shared.module';
import { AppRoutingModule } from '../app-routing.module';
import { AuthService } from '../auth/auth.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { InterestsModule } from '../interests/interests.module';

@NgModule({
  declarations: [
    HeaderComponent,
    HomeComponent
],
  imports: [
    NgbModule,
    SharedModule,
    InterestsModule
  ],
  exports: [
    HeaderComponent
  ],
  providers: [
    AuthService
  ]
})
export class CoreModule {}
