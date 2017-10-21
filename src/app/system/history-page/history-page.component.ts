import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Observable } from 'rxjs/Observable';
import * as moment from 'moment';

import { EventsService } from '../shared/services/events.service';
import { CategoriesService } from '../shared/services/categories.service';
import { Category } from '../../models/category.model';
import { EventChangeMoney } from '../../models/event.model';

@Component({
  selector: 'csw-history-page',
  templateUrl: './history-page.component.html',
  styleUrls: ['./history-page.component.scss']
})
export class HistoryPageComponent implements OnInit, OnDestroy {

  private sub1: Subscription;

  isLoaded = false;
  categories: Category[];
  events: EventChangeMoney[];
  eventsFilter: EventChangeMoney[];

  chartData = [];

  isFilterVisible = false;

  constructor(private eventsService: EventsService,
              private categoriesService: CategoriesService) { }

  ngOnInit() {
    this.sub1 = Observable.combineLatest(
      this.categoriesService.getCategories(),
      this.eventsService.getEvents()
    ).subscribe((data: [Category[], EventChangeMoney[]]) => {
      this.categories = data[0];
      this.events = data[1];
      this.events.forEach(e => {
        e.catName = this.categories.find(c => c.id === e.category).name;
      });

      this.setOridginalEvents();
      this.calculateChartData();

      this.isLoaded = true;
    });
  }

  ngOnDestroy() {
    if (this.sub1) { this.sub1.unsubscribe(); }
  }

  showFilter() {
    this.changeFilterVisible(true);
  }

  filterApply(filter) {
    this.setOridginalEvents();
    this.changeFilterVisible(false);

    const startPeriod = moment().startOf(filter.period).startOf('d');
    const endPeriod = moment().endOf(filter.period).endOf('d');

    this.eventsFilter = this.eventsFilter
      .filter(e => filter.types.indexOf(e.type) !== -1)
      .filter(e => filter.categories.indexOf(e.category.toString()) !== -1)
      .filter(e => {
        const momentDate = moment(e.date, 'DD.MM.YYYY HH:mm:ss');
        return momentDate.isBetween(startPeriod, endPeriod);
      });

    console.log(this.eventsFilter);
    this.calculateChartData();
  }

  filterCancel() {
    this.changeFilterVisible(false);
    this.setOridginalEvents();
    this.calculateChartData();
  }

  private calculateChartData(): void {
    this.chartData = [];

    this.categories.forEach((cat) => {
      const eventsCat = this.eventsFilter.filter(e => e.category === cat.id && e.type === 'outcome');

      this.chartData.push({
        value: eventsCat.reduce((total, e) => total += e.amount, 0),
        name: cat.name
      });
    });
  }

  private changeFilterVisible(value: boolean) {
    this.isFilterVisible = value;
  }

  private setOridginalEvents() {
    this.eventsFilter = this.events.slice();
  }
}
