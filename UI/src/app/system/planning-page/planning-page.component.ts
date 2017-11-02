import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Observable } from 'rxjs/Observable';

import { BillService } from '../shared/services/bill.service';
import { EventsService } from '../shared/services/events.service';
import { CategoriesService } from '../shared/services/categories.service';
import { Bill } from '../../models/bill.model';
import { Category } from '../../models/category.model';
import { EventChangeMoney } from '../../models/event.model';

@Component({
  selector: 'csw-planning-page',
  templateUrl: './planning-page.component.html',
  styleUrls: ['./planning-page.component.scss']
})
export class PlanningPageComponent implements OnInit, OnDestroy {

  private sub1: Subscription;

  isLoaded = false;
  bill: Bill;
  categories: Category[];
  events: EventChangeMoney[];

  constructor(
    private billService: BillService,
    private eventsService: EventsService,
    private categoriesService: CategoriesService
  ) { }

  ngOnInit() {
    this.sub1 = Observable.combineLatest(
      this.billService.getBill(),
      this.categoriesService.getCategories(),
      this.eventsService.getEvents()
    ).subscribe((data: [Bill, Category[], EventChangeMoney[]]) => {
      this.bill = data[0];
      this.categories = data[1];
      this.events = data[2];
      this.isLoaded = true;
    });
  }

  ngOnDestroy() {
    if (this.sub1) { this.sub1.unsubscribe(); }
  }

  getCategoryCostPercent(cat: Category): string {
    return `${this.getCategoryCost(cat)}%`;
  }

  getCategoryCost(cat: Category): number {
    const eventsByCat = this.events.filter(e => e.categoryId === cat.id && e.type === 'outcome');
    return eventsByCat.reduce((total, e) => {
      total += e.amount;
      return total;
      }, 0);
  }

  getCatPercent(cat: Category): string {
    return `${this.getPercent(cat)}%`;
  }

  getCatColor(cat: Category): string {
    const percent = this.getPercent(cat);
    return  percent <= 60 ? 'success' : percent >= 100 ? 'danger' : 'warning';
  }

  private getPercent(cat: Category): number {
    const percent = (this.getCategoryCost(cat) / cat.capacity) * 100;
    return percent > 100 ? 100 : percent;
  }
}
