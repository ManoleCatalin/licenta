import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './pagenotfound.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      { path: 'not-found', component: PageNotFoundComponent },
      { path: '**', redirectTo: '/not-found' }
    ])
  ],
  declarations: [PageNotFoundComponent],
  exports: [RouterModule]
})
export class PageNotFoundModule {}
