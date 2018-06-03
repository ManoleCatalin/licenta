import { Component, OnInit } from '@angular/core';
import { DataStorageService } from '../shared/data-storage.service';
import { Post } from './post.model';
import { PreviewPostComponent } from './preview-post/preview-post.component';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  public posts: Post[] = [];
  private pagesLoaded = 0;
  constructor(private dataStorage: DataStorageService) {}

  ngOnInit() {
    const initPosts = this.dataStorage.getPosts(this.pagesLoaded, 5);
    console.log('initPosts: ' + initPosts);
    this.posts = this.posts.concat(initPosts);
    this.pagesLoaded = this.pagesLoaded + 1;
    console.log(this.posts.length);
  }

  onScroll() {
    console.log('scroll works');
    this.posts = this.posts.concat(this.dataStorage.getPosts(this.pagesLoaded++, 5));
  }
}
