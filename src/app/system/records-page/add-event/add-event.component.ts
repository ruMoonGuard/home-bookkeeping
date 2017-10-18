import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import * as moment from 'moment';

import { Category } from '../../../models/category.model';
import { EventChangeMoney } from '../../../models/event.model';
import { EventsService } from '../../shared/services/events.service';
import { BillService } from '../../shared/services/bill.service';
import { Subscription } from 'rxjs/Subscription';
import { Bill } from '../../../models/bill.model';
import { Message } from '../../../models/message.model';

@Component({
  selector: 'csw-add-event',
  templateUrl: './add-event.component.html',
  styleUrls: ['./add-event.component.scss']
})
export class AddEventComponent implements OnInit, OnDestroy {

  @Input() categories: Category[] = [];

  sub1: Subscription;

  message: Message;

  typesEvents = [
    { type: 'outcome', label: 'Расход' },
    { type: 'income', label: 'Доход' }
  ];

  constructor(
    private eventService: EventsService,
    private billService: BillService
  ) { }

  ngOnInit() {
    this.message = new Message('error', '');
  }

  ngOnDestroy() {
    if (this.sub1) { this.sub1.unsubscribe(); }
  }

  onSubmit(form: NgForm) {
    const { type, amount, category, description } = form.value;
    const date = moment().format('DD.MM.YY HH:mm:ss');

    const event = new EventChangeMoney(type, description, date, category, amount);

    this.sub1 = this.billService.getBill()
      .subscribe((bill: Bill) => {
        let value = 0;
        if (type !== 'outcome') {
          value += bill.value + amount;
        } else {
          value = bill.value - amount;
          if (value < 0) {
            this.showMessage(`Вам не хватает ${value * -1}`);
            return;
          }
        }
        this.billService.updateBill({value, currency: bill.currency})
          .mergeMap(() => this.eventService.addEventChangeMoney(event))
          .subscribe(() => {
            form.setValue({
              amount: 0,
              description: '',
              category: 1,
              type: 'outcome'
            });
            this.showMessage(`Событие успешно добавлено!`, 'success');
          });
      });
    console.log(event);
  }

  showMessage(text: string, type: string = 'danger') {
    this.message = new Message(type, text);
    window.setTimeout(() => {
      this.message.text = '';
    }, 5000);
  }
}
