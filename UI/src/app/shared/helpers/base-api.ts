import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

export class BaseNewApi {
  private _baseUrl = 'http://localhost:5000/api/';

  constructor(public http: HttpClient) {}

  public get(url: string, baseUrl: string = ''): Observable<any> {
    return this.http.get(this.getUrl(url, baseUrl));
  }

  public post(url: string, data: any, baseUrl: string = ''): Observable<any> {
    return this.http.post(this.getUrl(url, baseUrl), data);
  }

  public put(url: string, data: any, baseUrl: string = ''): Observable<any> {
    return this.http.put(this.getUrl(url, baseUrl), data);
  }

  //
  // private methods
  //

  private getUrl(url: string, baseUrl: string) {
    return baseUrl ? baseUrl + url : this._baseUrl + url;
  }
}
