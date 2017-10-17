import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import { BaseAPI } from '../../../shared/helpers/BaseAPI';
import { EventChangeMoney } from '../../../models/event.model';

@Injectable()
export class EventsService extends BaseAPI {
  constructor(public http: Http) {
    super(http);
  }

  addEventChangeMoney(event: EventChangeMoney): Observable<EventChangeMoney> {
    return this.post('events', event);
  }
}
