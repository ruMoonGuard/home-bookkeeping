import { Component, OnDestroy, OnInit } from '@angular/core';
import { EventsService } from '../shared/services/events.service';
import { CategoriesService } from '../shared/services/categories.service';
import { Subscription } from 'rxjs/Subscription';
import { Observable } from 'rxjs/Observable';
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

  chartData = [];

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

      this.calculateChartData();

      this.isLoaded = true;
    });
  }

  ngOnDestroy() {
    if (this.sub1) { this.sub1.unsubscribe(); }
  }

  private calculateChartData(): void {
    this.chartData = [];

    this.categories.forEach((cat) => {
      const eventsCat = this.events.filter(e => e.category === cat.id && e.type === 'outcome');

      this.chartData.push({
        value: eventsCat.reduce((total, e) => total += e.amount, 0),
        name: cat.name
      });
    });
  }

}
