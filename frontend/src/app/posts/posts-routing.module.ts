import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PostsComponent } from './posts.component';
import { CreatePostComponent } from './create-post/create-post.component';


const postsRoutes: Routes = [
  { path: 'createPost', component: CreatePostComponent},
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
