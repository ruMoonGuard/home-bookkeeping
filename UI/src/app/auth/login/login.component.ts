import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Meta, Title } from '@angular/platform-browser';

import { UsersService } from '../../shared/services/users.service';
import { User } from '../../models/user.model';
import { Message } from '../../models/message.model';
import { AuthService } from '../../shared/services/auth.service';
import { fadeStateTrigger } from '../../shared/animations/fade.animation';
import { TokenObject } from '../../models/token-object.model';

@Component({
  selector: 'csw-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  animations: [fadeStateTrigger]
})
export class LoginComponent implements OnInit {

  public form: FormGroup;
  public message: Message;

  constructor(
    private usersService: UsersService,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private title: Title,
    private meta: Meta
  ) {
    title.setTitle('Страница входа');
    meta.addTags([
      { name: 'keywords', content: 'вход, авторизация, логин'},
      { name: 'description', content: 'Страница для входа в систему'}
    ]);
  }

  ngOnInit() {
    this.message = new Message('danger', '');
    this.form = new FormGroup({
      'email': new FormControl(null, [Validators.required, Validators.email]),
      'password': new FormControl(null, [Validators.required, Validators.minLength(6)])
    });

    this.route.queryParams
      .subscribe((params: Params) => {
        if (params['nowCanLogin']) {
          this.showMessage('Вы усешно зарегистрированы! Можете войти.', 'success');
        } else if (params['accessDenied']) {
          this.showMessage('Для просмотра данной страницы необходимо войти в систему', 'warning');
        }
      });
  }

  public onSubmit(): void {
    const { email, password } = this.form.value;

    this.usersService.getToken(email, password)
      .switchMap((token: TokenObject) => {
        this.authService.login(token);
        return this.usersService.getUser();
      })
      .subscribe((user: User) => {
        this.usersService.setUserInStorage(user);
        this.router.navigate(['/system', 'bill']);
      });
/*      .subscribe((token: TokenObject) => {
        console.log(token);
        this.authService.login(token);

        this.usersService.getUser().subscribe((user: User) => {
          this.usersService.setUserInStorage(user);
          this.router.navigate(['/system', 'bill']);
        });
      });*/
/*    this.usersService.getUserByEmail(formData.email)
      .subscribe((user: User) => {
        if (user) {
          if (user.password === formData.password) {
            this.message.text = '';
            this.authService.login(user);
            this.router.navigate(['/system', 'bill']);
          } else {
            this.showMessage('Неправильный пароль!');
          }
        } else {
          this.showMessage('Неправильный логин!');
        }
      });*/
  }

  private showMessage(text: string, type: string = 'danger'): void {
    this.message = new Message(type, text);

    window.setTimeout(() => {
      this.message.text = '';
    }, 5000);
  }
}
