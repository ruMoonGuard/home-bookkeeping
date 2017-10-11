import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { UsersService } from '../../shared/services/users.service';
import { User } from '../../models/user.model';
import { Message } from '../../models/message.model';
import { AuthService } from '../../shared/services/auth.service';

@Component({
  selector: 'csw-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public form: FormGroup;
  public message: Message;

  constructor(
    private usersService: UsersService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    this.message = new Message('danger', '');
    this.form = new FormGroup({
      'email': new FormControl(null, [Validators.required, Validators.email]),
      'password': new FormControl(null, [Validators.required, Validators.minLength(6)])
    });
  }

  public onSubmit(): void {
    const formData = this.form.value;

    this.usersService.getUserByEmail(formData.email)
      .subscribe((user: User) => {
        if (user) {
          if (user.password === formData.password) {
            this.message.text = '';
            this.authService.login(user);
            // this.router.navigate([]);
          } else {
            this.showMessage('Неправильный пароль!');
          }
        } else {
          this.showMessage('Неправильный логин!');
        }
      });
  }

  private showMessage(text: string, type: string = 'danger'): void {
    this.message = new Message(type, text);

    window.setTimeout(() => {
      this.message.text = '';
    }, 5000);
  }
}
