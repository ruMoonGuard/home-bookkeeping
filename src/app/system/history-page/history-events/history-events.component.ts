import { Component, Input, OnInit } from '@angular/core';
import { Category } from '../../../models/category.model';
import { EventChangeMoney } from '../../../models/event.model';

@Component({
  selector: 'csw-history-events',
  templateUrl: './history-events.component.html',
  styleUrls: ['./history-events.component.scss']
})
export class HistoryEventsComponent implements OnInit {

  @Input() categories: Category[] = [];
  @Input() events: EventChangeMoney[] = [];

  constructor() { }

  ngOnInit() {
  }

  getCategoryName(e: EventChangeMoney): string {
    return this.categories.find(c => c.id === e.category).name;
  }
}
