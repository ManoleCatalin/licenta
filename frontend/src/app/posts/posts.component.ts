import { Component, OnInit } from '@angular/core';
import { DataStorageService } from '../shared/data-storage.service';
import { Post } from './post.model';
import { Interest } from '../interests/interest.model';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  public posts: Post[] = [];
  private pagesLoaded = 1;
  orderedBy: string = null;
  ofInterest: string = null;
  selfPosts = false;

  constructor(private dataStorage: DataStorageService, private route: ActivatedRoute) {}

  ngOnInit() {
    this.orderedBy = this.route.snapshot.data['orderedBy'];
    this.selfPosts = this.route.snapshot.data['selfPosts'];

    console.log('orderedBy: ', this.orderedBy);
    console.log('selfPosts: ', this.selfPosts);

    this.route.params.subscribe(params => {
      this.ofInterest = params.id;
      console.log('ofInterest: ', this.ofInterest);
    });

    this.dataStorage
      .getPosts(this.orderedBy, this.selfPosts, this.ofInterest, this.pagesLoaded, 5)
      .subscribe(result => {
        this.parsePostResult(result);
      });
  }

  parsePostResult(result: any) {
    console.log('initPosts: ' + result);

    const newPosts: Post[] = [];
    for (let i = 0; i < result.length; i++) {
      const interests: Interest[] = [];
      const p = result[i];
      for (let j = 0; j < p['interests'].length; j++) {
        const interest = p['interests'][j];
        interests.push(new Interest(interest['id'], interest['name'], interest['thumbnailImgUrl']));
      }

      console.log(interests);
      newPosts.push(
        new Post(
          p['id'],
          p['title'],
          p['sourceUrl'],
          p['description'],
          '',
          '',
          p['likesCount'],
          p['likeId'],
          p['favoriteId'],
          'noName',
          p['createdAt'],
          p[`userId`],
          interests
        )
      );
    }

    newPosts.forEach(post => {
        const url = post.url;
        const auth = '1545-33ee2ca36dd86004edebafe530d2b8e3';
        const imgUrl = '//image.thum.io/get/auth/' + auth + '/' + url;
        post.previewImgUrl = imgUrl;
     });

    this.posts = this.posts.concat(newPosts);
    this.pagesLoaded = this.pagesLoaded + 1;
  }

  onScroll() {
    this.dataStorage
      .getPosts(this.orderedBy, this.selfPosts, this.ofInterest, this.pagesLoaded, 5)
      .subscribe(
        result => {
          console.log('pagesLoaded' + this.pagesLoaded);
          this.parsePostResult(result);
        },
        error => {
          console.log('error' + error);
        }
      );
  }
}
