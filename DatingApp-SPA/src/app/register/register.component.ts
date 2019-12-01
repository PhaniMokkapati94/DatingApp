import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};

  @Output() cancelRegister = new EventEmitter();

  constructor(private authservice: AuthService) { }

  ngOnInit() {
  }
  register() {
    this.authservice.register(this.model).subscribe(response => {
      console.log('Regitered Successfully');
    }, error => {
      console.log(error);

    });
    console.log(this.model);
  }
  cancel() {
    this.cancelRegister.emit(false);
    console.log('Cancelled');
  }

}
