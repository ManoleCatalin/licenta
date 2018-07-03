import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';


@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {

  error = false;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
  }

  onSignIn(form: NgForm) {

    const password = form.value.password;
    const username = form.value.username;

    this.authService.signinUser(username, password).subscribe(result => {

      this.authService.saveToken(result['auth_token']);

      this.router.navigate(['/posts/freshness']);
    }, error => {
      this.error = true;
    });
  }
}
