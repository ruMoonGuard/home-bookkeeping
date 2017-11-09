import { TokenObject } from '../../models/token-object.model';

export class AuthService {

  private isAuthenticated = false;

  constructor() {}

  public login(token: TokenObject): void {
    this.isAuthenticated = true;
    window.localStorage.setItem('token', token.token);
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
