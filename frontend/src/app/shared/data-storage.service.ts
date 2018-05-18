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
      ''
    ),
    new Post(
      'Creating sick modals using ng-bootstrap',
      'https://ng-bootstrap.github.io/#/components/modal/examples',
      `Bootstrap widgets for Angular: autocomplete, accordion, alert, carousel, dropdown, pagination,
       popover, progressbar, rating, tabset, timepicker, tooltip, typeahead`,
       'https://ng-bootstrap.github.io/img/logo.svg'
    ),
    new Post(
      'Creating mappings with Automapper',
      'https://cpratt.co/using-automapper-creating-mappings/',
      `Posts in this Series Getting Started Creating Mappings Mapping Instances Intro In the previous post,
we looked at how to centralize our AutoMapper mapping definitions in a config class that we can run at application start.
 In this post, we\'ll look at how to create these mappings and how to`,
      ''
    ),
    new Post(
      'Creating sick modals using ng-bootstrap',
      'https://ng-bootstrap.github.io/#/components/modal/examples',
      `Bootstrap widgets for Angular: autocomplete, accordion, alert, carousel, dropdown, pagination,
       popover, progressbar, rating, tabset, timepicker, tooltip, typeahead`,
       'https://ng-bootstrap.github.io/img/logo.svg'
    )
  ];

  constructor() {}

  getPosts() {
    return this.dummyPosts;
  }
}
