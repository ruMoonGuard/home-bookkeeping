import { Http, Response } from '@angular/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { Bill } from '../../../models/bill.model';

@Injectable()
export class BillService {
  constructor(private http: Http) {}

  getBill(): Observable<Bill> {
    return this.http.get('http://localhost:3000/bill')
      .map((response: Response) => response.json());
  }

  getCurrency(currency: string = 'RUB'): Observable<any> {
    return this.http.get(`http://api.fixer.io/latest?base=${currency}`)
      .map((response: Response) => response.json());
  }
}
