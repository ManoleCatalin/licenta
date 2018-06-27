import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { RequestOptions } from '@angular/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  token: string;

  constructor(private router: Router, private http: HttpClient) {}

  signupUser(username: string, email: string, password: string) {

   const firstName = '-';
   const lastName = '-';

   return this.http.post(environment.backendUrl + '/Auth/register', {
    username: username,
    email: email,
    password: password,
    firstName: firstName,
    lastName: lastName
  });
  }

  signinUser(username: string, password: string) {
      this.token = 'dummy-string';

      return this.http.post(environment.backendUrl + '/Auth/login', {
        username: username,
        password: password,
      });
  }

  logout() {
    this.token = null;
  }

  getToken() {
    return this.token;
  }

  isAuthenticated() {
    return this.token != null;
  }

}
