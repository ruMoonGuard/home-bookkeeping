import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';

import { Category } from '../../../models/category.model';
import { CategoriesService } from '../../shared/services/categories.service';
import { Message } from '../../../models/message.model';

@Component({
  selector: 'csw-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.scss']
})
export class EditCategoryComponent implements OnInit {
  @Input() categories: Category[] = [];
  @Output() onCategoryChanged = new EventEmitter<Category>();

  message: Message;

  categoryCurrentId = 1;
  categoryCurrent: Category;

  constructor(private categoriesService: CategoriesService) { }

  ngOnInit() {
    this.message = new Message('success', '');
    this.changeCategory();
  }

  onSubmit(form: NgForm) {
    const { name, capacity } = form.value;

    const updateCategory = new Category(name, capacity, +this.categoryCurrentId);

    this.categoriesService.updateCategory(updateCategory)
      .subscribe((category: Category) => {
        this.onCategoryChanged.emit(category);
        this.message.text = 'Категория успешно обновлена!';
        window.setTimeout(() => {
          this.message.text = '';
        }, 5000);
      });
  }

  changeCategory() {
    this.categoryCurrent = this.categories.find(c => c.id === +this.categoryCurrentId);
  }
}
