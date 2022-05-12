import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddUnit } from '../models/api-inputs/add-unit';

@Injectable({
  providedIn: 'root'
})
export class UnitService {

  constructor(@Inject('BASE_URL') public baseUrl: string, private http: HttpClient) {}

  public addUnit(input: AddUnit): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'unit/add', input);
  }
}
