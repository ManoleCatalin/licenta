import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './pagenotfound.component';
import { CoreModule } from '../core/core.module';

@NgModule({
  imports: [
    CoreModule,
    RouterModule.forChild([
      { path: 'not-found', component: PageNotFoundComponent },
      { path: '**', redirectTo: '/not-found' }
    ])
  ],
  declarations: [PageNotFoundComponent],
  exports: [RouterModule]
})
export class PageNotFoundModule {}
