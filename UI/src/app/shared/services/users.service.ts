import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { User } from '../../models/user.model';
import { BaseNewApi } from '../helpers/base-api';
import { HttpClient } from '@angular/common/http';
import { TokenObject } from '../../models/token-object.model';

@Injectable()
export class UsersService extends BaseNewApi {

  constructor(public http: HttpClient) {
    super(http);
  }

  getUserByEmail(email: string): Observable<User> {
    return this.get(`users?email=${email}`, 'http://localhost:3000/')
      .map((user: User[]) => user[0] ? user[0] : undefined);
  }

  getToken(username: string, password: string): Observable<TokenObject> {
    return this.post('token/create', { username, password });
  }

  getUser(): Observable<User> {
    return this.get('account');
  }

  createUser(user: User): Observable<User> {
    return this.post('users', user, 'http://localhost:3000/');
  }

  public setUserInStorage(user: User): void {
    window.localStorage.setItem('user', JSON.stringify(user));
  }

  public getUserFromStorage(): User {
    return JSON.parse(window.localStorage.getItem('user'));
  }
}
