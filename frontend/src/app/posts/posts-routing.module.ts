import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PostsComponent } from './posts.component';


const postsRoutes: Routes = [
  { path: 'posts', component: PostsComponent, children: [
  ] },
];

@NgModule({
  imports: [
    RouterModule.forChild(postsRoutes)
  ],
  exports: [RouterModule],
  providers: [
  ]
})
export class PostsRoutingModule {}
