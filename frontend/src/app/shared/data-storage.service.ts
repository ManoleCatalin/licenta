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

  activeInterests: Interest[] = [];

  constructor(private httpClient: HttpClient, private authService: AuthService) {
  }
  getPostsOfInterest(interestId: string, page?: number, perPage?: number) {
    const userId = this.authService.getCurrentUserId();

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

    const userId = this.authService.getCurrentUserId();

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

    const headers = {
      Authorization: `Bearer ${this.authService.getToken()}`,
      'Content-Type': 'application/json'
    };

    return this.httpClient.post(environment.backendUrl + '/Interests/' + this.authService.getCurrentUserId() + '/' + id,
      {headers: headers});
  }

  removeActiveInterest(id: string) {

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

  likePost(postId: string, userId: string) {
    const headers = {
      Authorization: `Bearer ${this.authService.getToken()}`,
      'Content-Type': 'application/json'
    };

    return this.httpClient.post(environment.backendUrl + 'Likes', {'postId': postId, 'userId': userId}, { headers: headers });
  }

  unlikePost(likeId: string) {
    const headers = {
      Authorization: `Bearer ${this.authService.getToken()}`,
      'Content-Type': 'application/json'
    };

    return this.httpClient.delete(environment.backendUrl + 'Likes/' + likeId, { headers: headers });
  }

  favoritePost(postId: string, userId: string) {
    const headers = {
      Authorization: `Bearer ${this.authService.getToken()}`,
      'Content-Type': 'application/json'
    };

    return this.httpClient.post(environment.backendUrl + 'Favorites', {'postId': postId, 'userId': userId}, { headers: headers });
  }

  unfavoritePost(favoriteId: string) {
    const headers = {
      Authorization: `Bearer ${this.authService.getToken()}`,
      'Content-Type': 'application/json'
    };

    return this.httpClient.delete(environment.backendUrl + 'Favorites/' + favoriteId, { headers: headers });
  }
}
