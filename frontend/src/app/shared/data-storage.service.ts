import { Injectable } from '@angular/core';
import { CreatePostModel } from '../posts/post.model';
import { Interest } from '../interests/interest.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { AuthService } from '../auth/auth.service';
import 'rxjs/add/operator/map';


@Injectable({
  providedIn: 'root'
})
export class DataStorageService {
  linkPreviewApiKey = '5aef00fbec6e53b6e6f6f7299c5133bd073ed1475bd65';

  //   dummyPosts: Post[] = [
  //     new Post(
  //       'Creating mappings with Automapper',
  //       'https://cpratt.co/using-automapper-creating-mappings/',
  //       `Posts in this Series Getting Started Creating Mappings Mapping Instances Intro In the previous post,
  // we looked at how to centralize our AutoMapper mapping definitions in a config class that we can run at application start.
  //  In this post, we\'ll look at how to create these mappings and how to`,
  //       '',
  //       '',
  //       123,
  //       true,
  //       false,
  //       'Mark123',
  //       '123',
  //     ),
  //     new Post(
  //       'Creating sick modals using ng-bootstrap',
  //       'https://ng-bootstrap.github.io/#/components/modal/examples',
  //       `Bootstrap widgets for Angular: autocomplete, accordion, alert, carousel, dropdown, pagination,
  //        popover, progressbar, rating, tabset, timepicker, tooltip, typeahead`,
  //       'https://ng-bootstrap.github.io/img/logo.svg',
  //       '',
  //       19,
  //       false,
  //       false,
  //       'DannyTheAuthor',
  //       '124',
  //     ),
  //     new Post(
  //       'The awsome cpp list',
  //       'https://github.com/fffaraz/awesome-cpp',
  //       `awesome-cpp - A curated list of awesome C++ (or C) frameworks, libraries, resources,
  //       and shiny things. Inspired by awesome-... stuff.  `,
  //       'https://avatars2.githubusercontent.com/u/895678?s=400&v=4',
  //       '',
  //       1392,
  //       false,
  //       true,
  //       'IannisMore',
  //       '125',
  //     )
  //  ];

  availableInterests: Interest[] = [
    new Interest(
      '1',
      'aspnet',
      'https://blog.tallan.com/wp-content/uploads/2016/12/ASP.NET-logo.png'
    ),
    new Interest('2', 'automapper', 'https://avatars0.githubusercontent.com/u/890883?s=200&v=4'),
    new Interest(
      '3',
      'cpp',
      'https://raw.githubusercontent.com/mwasplund/Tracer/master/Assets/cpp_icon.png'
    ),
    new Interest(
      '4',
      'artificial Intelligence',
      'https://www.counterpointresearch.com/wp-content/uploads/2017/08/artificial-intelligence-378x225.png'
    )
  ];

  activeInterests: Interest[] = [];

  constructor(private httpClient: HttpClient, private authService: AuthService) {
    const auth = '1545-33ee2ca36dd86004edebafe530d2b8e3';
    // this.dummyPosts.forEach(post => {
    //   const url = post.url;
    //   const imgUrl = '//image.thum.io/get/auth/' + auth + '/' + url;
    //   post.previewImgUrl = imgUrl;
    // });

    for (let index = 0; index < 5; index++) {
      // this.dummyPosts = this.dummyPosts.concat(this.dummyPosts.slice());
    }

    // TODO: Delete this from frontend when implemented in backend
    // tslint:disable-next-line:max-line-length
    // const getImgSearchGoogle = 'https://www.googleapis.com/customsearch/v1?key=' + environment.custopGoogleSearchApiKey + '&cx=010971677492822427252:8_y1rtvjy0u&num=1&start=1&imgSize=medium&searchType=image&q=';
    // this.availableInterests.forEach((interest) => {
    //   httpClient.get(getImgSearchGoogle + interest.name)
    //   .subscribe((imgLink: any) => {
    //     interest.thumbnailImgUrl = imgLink.items[0].link;
    //     console.log(interest.thumbnailImgUrl);
    //   });
    // });
  }
  getPostsOfInterest(interestId: string, page?: number, perPage?: number) {
    const userId = this.authService.getCurrentUserId();
    console.log('uid: ' + userId);

    const headers = {
      Authorization: `Bearer ${this.authService.getToken()}`,
      'Content-Type': 'application/json'
    };
    const httpParams = new HttpParams()
      .set('userId', userId)
      .set('selfPosts', 'false')
      .set('pageIndex', page.toString())
      .set('pageSize', perPage.toString())
      .set('interestId', interestId);

    return this.httpClient.get(environment.backendUrl + '/posts', {
      headers: headers,
      params: httpParams
    });
  }

  getPosts(orderBy: string, selfPosts: boolean, ofInterest: string, page?: number, perPage?: number) {
    // console.log('getPosts ' + page + ' ' + perPage);
    // console.log('this.dummyPosts.length ' + this.dummyPosts.length);
    // if (page === null && perPage === null) {
    //   return this.dummyPosts.slice();
    // }

    // const start = page * perPage;
    // const end = page * perPage + perPage;
    // console.log('start: ' + start + ' end: ' + end);
    // return this.dummyPosts.slice(start, end);

    const userId = this.authService.getCurrentUserId();
    console.log('uid: ' + userId);

    const headers = {
      Authorization: `Bearer ${this.authService.getToken()}`,
      'Content-Type': 'application/json'
    };
    let httpParams = new HttpParams()
      .set('userId', userId);

      if (orderBy) {
        httpParams = httpParams.set('orderBy', orderBy);
      }

      httpParams = httpParams.set('selfPosts', selfPosts ? 'true' : 'false');

      httpParams = httpParams
      .set('pageIndex', page.toString())
      .set('pageSize', perPage.toString());


    if (ofInterest) {
      httpParams = httpParams.set('interestId', ofInterest);
    }

    console.log(httpParams.toString());

    return this.httpClient.get(environment.backendUrl + '/posts', {
      headers: headers,
      params: httpParams
    });
  }

  public getActiveInterests() {
    const headers = {
      Authorization: `Bearer ${this.authService.getToken()}`,
      'Content-Type': 'application/json'
    };

    return this.httpClient.get<Interest[]>(environment.backendUrl + '/Interests/' + this.authService.getCurrentUserId(), {
      headers: headers
    });
  }

  public getAvailableInterests(page?: number, perPage?: number) {
    const headers = {
      Authorization: `Bearer ${this.authService.getToken()}`,
      'Content-Type': 'application/json'
    };

    const httpParams = new HttpParams()
      .set('pageIndex', page.toString())
      .set('pageSize', perPage.toString());

    return this.httpClient.get<Interest[]>(environment.backendUrl + '/Interests', {
      headers: headers,
      params: httpParams
    });
  }

  addActiveInterest(id: string) {
    console.log('addActiveInterest id = ', id);
    // this.activeInterests.push(
    //   this.availableInterests.find(interest => {
    //     return interest.id === id;
    //   })
    // );
    // this.availableInterests = this.availableInterests.filter(interest => {
    //   return interest.id !== id;
    // });
    const headers = {
      Authorization: `Bearer ${this.authService.getToken()}`,
      'Content-Type': 'application/json'
    };

    return this.httpClient.post(environment.backendUrl + '/Interests/' + this.authService.getCurrentUserId() + '/' + id,
      {headers: headers});
  }

  removeActiveInterest(id: string) {
    // this.availableInterests.push(
    //   this.activeInterests.find(interest => {
    //     return interest.id === id;
    //   })
    // );

    // this.activeInterests = this.activeInterests.filter(interest => {
    //   return interest.id !== id;
    // });
    const headers = {
      Authorization: `Bearer ${this.authService.getToken()}`,
      'Content-Type': 'application/json'
    };

    return this.httpClient.delete(environment.backendUrl + '/Interests/' + this.authService.getCurrentUserId() + '/' + id,
      {headers: headers});
  }

  createPost(createPost: CreatePostModel) {
    const headers = {
      Authorization: `Bearer ${this.authService.getToken()}`,
      'Content-Type': 'application/json'
    };

    return this.httpClient.post(environment.backendUrl + 'Posts', createPost, { headers: headers });
  }
}
