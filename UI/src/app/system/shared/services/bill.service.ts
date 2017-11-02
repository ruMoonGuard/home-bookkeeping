import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { Bill } from '../../../models/bill.model';
import { BaseNewApi } from '../../../shared/helpers/base-api';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class BillService extends BaseNewApi {
  constructor(public http: HttpClient) {
    super(http);
  }

  getBill(): Observable<Bill> {
    return this.get('bills');
  }

  getCurrency(currency: string = 'RUB'): Observable<any> {
    return this.get(`latest?base=${currency}`, 'http://api.fixer.io/');
  }

  updateBill(bill: Bill): Observable<Bill> {
    return this.put(`bills`, bill);
  }
}
