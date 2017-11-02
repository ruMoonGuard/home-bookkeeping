import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import { BaseAPI } from '../../../shared/helpers/BaseAPI';
import { Category } from '../../../models/category.model';
import { BaseNewApi } from '../../../shared/helpers/base-api';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class CategoriesService extends BaseNewApi {
  constructor(public http: HttpClient) {
    super(http);
  }

  getCategories(): Observable<Category[]> {
    return this.get('categories', 'http://localhost:5000/api/');
  }

  getCategoryById(id: number): Observable<Category> {
    return this.get(`categories/${id}`);
  }

  createCategory(category: Category): Observable<Category> {
    return this.post('categories', category);
  }

  updateCategory(category: Category): Observable<Category> {
    return this.put(`categories/${category.id}`, category);
  }
}
