import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';

import { CategoriesService } from '../../shared/services/categories.service';
import { Category } from '../../../models/category.model';

@Component({
  selector: 'csw-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.scss']
})
export class AddCategoryComponent implements OnInit {

  @Output() onCategoryAdded = new EventEmitter<Category>();

  constructor(private categoriesService: CategoriesService) { }

  ngOnInit() {
  }

  onSubmit(form: NgForm) {
    let { capacity } = form.value;
    const { name } = form.value;

    if (capacity < 0) {
      capacity *= -1;
    }

    const newCategory = new Category(name, capacity);

    this.categoriesService.createCategory(newCategory)
      .subscribe((category: Category) => {
        form.reset({'capacity': 1});
        this.onCategoryAdded.emit(category);
      });
  }
}
