import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PostsComponent } from './posts.component';
import { CreatePostComponent } from './create-post/create-post.component';


const postsRoutes: Routes = [
  { path: 'createPost', component: CreatePostComponent},
  { path: 'posts/popularity', component: PostsComponent, data : {orderedBy : 'popularity'} },
  { path: 'posts/freshness', component: PostsComponent, data : {orderedBy : 'freshness'} },
  { path: 'posts/favorite', component: PostsComponent, data : {orderedBy : 'favorite'} },
  { path: 'posts/own', component: PostsComponent, data : {selfPosts : true} },
  { path: 'posts/interest/:id', component: PostsComponent }
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
