import { Http, Response } from '@angular/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { Bill } from '../../../models/bill.model';
import { BaseAPI } from '../../../shared/helpers/BaseAPI';

@Injectable()
export class BillService extends BaseAPI {
  constructor(public http: Http) {
    super(http);
  }

  getBill(): Observable<Bill> {
    return this.get('bill');
  }

  getCurrency(currency: string = 'RUB'): Observable<any> {
    // return this.http.get(`http://api.fixer.io/latest?base=${currency}`)
    //   .map((response: Response) => response.json());
    return this.get(`latest?base=${currency}`, 'http://api.fixer.io/');
  }

  updateBill(bill: Bill): Observable<Bill> {
    return this.put(`bill`, bill);
  }
}
