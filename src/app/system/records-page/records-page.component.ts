import { Component, OnInit } from '@angular/core';
import { Category } from '../../models/category.model';

@Component({
  selector: 'csw-records-page',
  templateUrl: './records-page.component.html',
  styleUrls: ['./records-page.component.scss']
})
export class RecordsPageComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  newCategory(category: Category) {
    // to do
  }
}
