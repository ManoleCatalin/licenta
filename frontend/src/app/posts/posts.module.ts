import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostsComponent } from './posts.component';
import { PostsRoutingModule } from './posts-routing.module';

@NgModule({
  declarations: [PostsComponent],
  // exports: [PostsComponent],
  imports: [
    CommonModule,
    PostsRoutingModule
  ],
})
export class PostsModule { }
