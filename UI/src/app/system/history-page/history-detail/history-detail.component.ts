import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';

import { EventsService } from '../../shared/services/events.service';
import { CategoriesService } from '../../shared/services/categories.service';
import { EventChangeMoney } from '../../../models/event.model';
import { Category } from '../../../models/category.model';

@Component({
  selector: 'csw-history-detail',
  templateUrl: './history-detail.component.html',
  styleUrls: ['./history-detail.component.scss']
})
export class HistoryDetailComponent implements OnInit, OnDestroy {

  sub: Subscription;

  event: EventChangeMoney;
  category: Category;

  isLoaded = false;

  constructor(private route: ActivatedRoute,
              private eventsService: EventsService,
              private categoriesService: CategoriesService) { }

  ngOnInit() {
    this.route.params
      .mergeMap((params: Params) => this.eventsService.getEventById(params['id']))
      .mergeMap((event: EventChangeMoney) => {
        this.event = event;
        return this.categoriesService.getCategoryById(event.categoryId);
      })
      .subscribe((category: Category) => {
        this.category = category;
        this.event.catName = this.category.name;
        this.isLoaded = true;
      });
  }

  ngOnDestroy() {
    if (this.sub) { this.sub.unsubscribe(); }
  }
}
