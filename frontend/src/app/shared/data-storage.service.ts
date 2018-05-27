import { Injectable } from '@angular/core';
import { Post } from '../posts/post.model';

@Injectable({
  providedIn: 'root'
})
export class DataStorageService {
  linkPreviewApiKey = '5aef00fbec6e53b6e6f6f7299c5133bd073ed1475bd65';

  dummyPosts: Post[] = [
    new Post(
      'Creating mappings with Automapper',
      'https://cpratt.co/using-automapper-creating-mappings/',
      `Posts in this Series Getting Started Creating Mappings Mapping Instances Intro In the previous post,
we looked at how to centralize our AutoMapper mapping definitions in a config class that we can run at application start.
 In this post, we\'ll look at how to create these mappings and how to`,
      '',
      '',
      123,
      true,
      false
    ),
    new Post(
      'Creating sick modals using ng-bootstrap',
      'https://ng-bootstrap.github.io/#/components/modal/examples',
      `Bootstrap widgets for Angular: autocomplete, accordion, alert, carousel, dropdown, pagination,
       popover, progressbar, rating, tabset, timepicker, tooltip, typeahead`,
      'https://ng-bootstrap.github.io/img/logo.svg',
      '',
      19,
      false,
      false
    ),
    new Post(
      'The awsome cpp list',
      'https://github.com/fffaraz/awesome-cpp',
      `awesome-cpp - A curated list of awesome C++ (or C) frameworks, libraries, resources,
and shiny things. Inspired by awesome-... stuff.`,
      'https://avatars2.githubusercontent.com/u/895678?s=400&v=4',
      '',
      10392,
      false,
      true
    )
  ];

  constructor() {
    const auth = '1545-33ee2ca36dd86004edebafe530d2b8e3';
    this.dummyPosts.forEach(post => {
      const url = post.url;
      const imgUrl = '//image.thum.io/get/auth/' + auth + '/' + url;
      post.previewImgUrl = imgUrl;
    });

    for (let index = 0; index < 5; index++) {
      this.dummyPosts = this.dummyPosts.concat(this.dummyPosts.slice());
    }
  }

  getPosts(page?: number, perPage?: number) {
    console.log('getPosts ' + page + ' ' + perPage);
    console.log('this.dummyPosts.length ' + this.dummyPosts.length);
    if (page === null && perPage === null) {
      return this.dummyPosts.slice();
    }
    const start = page * perPage;
    const end = page * perPage + perPage;
    console.log('start: ' + start + ' end: ' + end);
    return this.dummyPosts.slice(start, end);
  }
}
