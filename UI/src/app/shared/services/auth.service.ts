import { TokenObject } from '../../models/token-object.model';

export class AuthService {

  private isAuthenticated = false;

  public login(token: TokenObject): void {
    this.isAuthenticated = true;
    window.localStorage.setItem('token', token.token);
    window.localStorage.setItem('name', token.name);
  }

  public logout(): void {
    this.isAuthenticated = false;
    window.localStorage.clear();
  }

  public isLoggedIn(): boolean {
    return this.isAuthenticated;
  }

  public getToken(): string {
    return window.localStorage.getItem('token');
  }
}
