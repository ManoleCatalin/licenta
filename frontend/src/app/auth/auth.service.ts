import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { JwtHelper } from 'angular2-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private helper: JwtHelper) {}

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

      return this.http.post(environment.backendUrl + '/Auth/login', {
        username: username,
        password: password,
      });
  }

  saveToken(token: string) {
    localStorage.setItem('auth-token', token);
  }

  logout() {
    localStorage.removeItem('auth-token');
  }

  getToken() {
    return localStorage.getItem('auth-token');
  }


  getCurrentUserId() {
    const token = this.getToken();
    if (token === null) {
      return null;
    }

    return this.helper.decodeToken(token).id;
  }

}
