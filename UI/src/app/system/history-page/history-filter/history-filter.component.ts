import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Category } from '../../../models/category.model';

@Component({
  selector: 'csw-history-filter',
  templateUrl: './history-filter.component.html',
  styleUrls: ['./history-filter.component.scss']
})
export class HistoryFilterComponent implements OnInit {

  @Output() onFilterApply = new EventEmitter<any>();
  @Output() onFilterCancel = new EventEmitter<any>();

  @Input() categories: Category[] = [];

  periods = [
    {type: 'd', label: 'День'},
    {type: 'w', label: 'Неделя'},
    {type: 'M', label: 'Месяц'},
  ];

  types = [
    {type: 'income', label: 'Доход'},
    {type: 'outcome', label: 'Расход'}
  ];

  selectedPeriod = 'd';
  selectedCategories = [];
  selectedTypeEvents = [];

  constructor() { }

  ngOnInit() {
  }

  closeFilter() {
    this.selectedPeriod = 'd';
    this.selectedCategories = [];
    this.selectedTypeEvents = [];
    this.onFilterCancel.emit();
  }

  applyFilter() {
    this.onFilterApply.emit({
      types: this.selectedTypeEvents,
      categories: this.selectedCategories,
      period: this.selectedPeriod
    });
  }

  onChangedTypeEvent({checked, value}) {
    this.calculateProperty('selectedTypeEvents', checked, value);
  }

  onChangedCategory({checked, value}) {
    this.calculateProperty('selectedCategories', checked, value);
  }

  private calculateProperty(field: string, checked: boolean, value: string) {
    if (checked) {
      this[field].indexOf(value) === -1 ? this[field].push(value) : null;
    } else {
      this[field] = this[field].filter(i => i !== value);
    }
  }
}
