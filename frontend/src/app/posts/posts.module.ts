import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { PostsComponent } from './posts.component';
import { PostsRoutingModule } from './posts-routing.module';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PreviewPostComponent } from './preview-post/preview-post.component';
import { PostCardComponent } from './post-card/post-card.component';
import { InterestsModule } from '../interests/interests.module';
import { CommentsModule } from '../comments/comments.module';
import { CoreModule } from '../core/core.module';

@NgModule({
  declarations: [PostsComponent, PreviewPostComponent,
    PostCardComponent
],
  imports: [
    CommentsModule,
    CommonModule,
    CoreModule,
    PostsRoutingModule,
    InfiniteScrollModule,
    BrowserModule,
    FormsModule,
    NgbModule
  ],
  exports: [InfiniteScrollModule],
  entryComponents: [PreviewPostComponent]
})
export class PostsModule {}
