import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AuthModule } from './auth/auth.module';
import { CoreModule } from './core/core.module';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { DropdownDirective } from './shared/dropdown.directive';
import { HeaderComponent } from './core/header/header.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PageNotFoundModule } from './pagenotfound/pagenotfound.module';
import { AuthRoutingModule } from './auth/auth-routing.module';
import { PostsRoutingModule } from './posts/posts-routing.module';
import { PostsModule } from './posts/posts.module';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    NgbModule.forRoot(),
    CoreModule,
    HttpClientModule,
    AuthModule,
    AppRoutingModule,
    AuthRoutingModule,
    PostsModule,
    PageNotFoundModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
