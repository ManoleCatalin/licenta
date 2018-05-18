import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  token: string;
  email: string;
  password: string;

  constructor(private router: Router) {}

  signupUser(email: string, password: string) {
    this.email = email;
    this.password = password;
  }

  signinUser(email: string, password: string) {
      this.token = 'dummy-string';
      this.router.navigate(['/']);
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
