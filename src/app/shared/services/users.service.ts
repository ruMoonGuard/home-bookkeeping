import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { User } from '../../models/user.model';

@Injectable()
export class UsersService {

  constructor(private http: Http) {}

  getUserByEmail(email: string): Observable<User> {
    return this.http.get(`http://localhost:3000/users?email=${email}`)
      .map((response: Response) => response.json())
      //.take(1);
      //.first();
      .map((user: User[]) => user[0] ? user[0] : undefined);
  }
}
