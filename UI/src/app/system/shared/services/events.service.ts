import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { EventChangeMoney } from '../../../models/event.model';
import { BaseNewApi } from '../../../shared/helpers/base-api';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class EventsService extends BaseNewApi {
  constructor(public http: HttpClient) {
    super(http);
  }

  addEventChangeMoney(event: EventChangeMoney): Observable<EventChangeMoney> {
    return this.post('events', event);
  }

  getEvents(): Observable<EventChangeMoney[]> {
    return this.get('events');
  }

  getEventById(id: number): Observable<EventChangeMoney> {
    return this.get(`events/${id}`);
  }
}
