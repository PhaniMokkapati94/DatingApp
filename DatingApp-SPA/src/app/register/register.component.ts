import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};

  @Output() cancelRegister = new EventEmitter();

  constructor(private authservice: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }
  register() {
    this.authservice.register(this.model).subscribe(response => {
      this.alertify.success('Registered Successfully');
    }, error => {
      this.alertify.error(error);

    });
    console.log(this.model);
  }
  cancel() {
    this.cancelRegister.emit(false);
    this.alertify.message('Cancelled');
  }

}
