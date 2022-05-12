import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddCategory } from '../models/api-inputs/add-category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(@Inject('BASE_URL') public baseUrl: string, private http: HttpClient) {}

  public addCategory(input: AddCategory): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'category/add', input);
  }
}
