import { Component, OnInit } from '@angular/core';
import { DataStorageService } from '../shared/data-storage.service';
import { Post } from './post.model';
import { DomSanitizer } from '@angular/platform-browser';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PreviewPostComponent } from './preview-post/preview-post.component';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  public posts: Post[] = [];
  private pagesLoaded = 0;
  constructor(private dataStorage: DataStorageService, public domSanitizer: DomSanitizer, private modalService: NgbModal) {}

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

  displayPreviewModal(index: number) {
    const modalRef = this.modalService.open(PreviewPostComponent, {size: 'lg', backdropClass: 'light-blue-backdrop'});
    console.log(index);
    modalRef.componentInstance.title = this.posts[index].title;
    modalRef.componentInstance.imageUrl = this.posts[index].previewImgUrl;
    modalRef.componentInstance.postUrl = this.posts[index].url;
  }

  markLiked(index: number) {
    this.posts[index].liked = true;
  }

  markFavorite(index: number) {
    this.posts[index].favorite = true;
  }
}
