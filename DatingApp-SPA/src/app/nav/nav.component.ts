import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(private authservice: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }
  Login() {
    console.log(this.model);
    this.authservice.login(this.model).subscribe(next => {
      // console.log('Logged in Successfully');
      this.alertify.success('Logged in Successfully');
    }, error => {
      this.alertify.error('Login Failed');

    }
    );
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }
  logout() {
    localStorage.removeItem('token');
    this.alertify.message('logged out');

  }

}
