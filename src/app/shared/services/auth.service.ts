import { User } from '../../models/user.model';

export class AuthService {

  private isAuthenticated = false;

  public login(user: User): void {
    this.isAuthenticated = true;
    window.localStorage.setItem('user', JSON.stringify(user));
  }

  public logout(): void {
    this.isAuthenticated = false;
    window.localStorage.clear();
  }

  public isLoggedIn(): boolean {
    return this.isAuthenticated;
  }
}
