import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  public error = false;
  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {}

  onSignup(form: NgForm) {
    const email = form.value.email;
    const password = form.value.password;
    const confirmPassword = form.value.confirmPassword;
    const username = form.value.username;
    console.log('email' + email);
    console.log('password' + password);
    console.log('confirmPassword' + confirmPassword);
    console.log('username' + username);

    if (confirmPassword !== password) {
      console.log('password not match');
      this.error = true;
      return;
    }
    this.authService.signupUser(username, email, password).subscribe(result => {
      this.router.navigate(['/login']);
    }, error => {
      this.error = true;
    });
  }
}
