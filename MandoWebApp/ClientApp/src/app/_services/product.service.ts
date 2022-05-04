import { EventEmitter, Inject, Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { AddPendingBuildingProduct } from "../models/api-inputs/add-pending-building-product";
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  public newPendingProductEmitter = new EventEmitter();

  constructor(@Inject('BASE_URL') public baseUrl: string, private http: HttpClient) {

  }

  public addPendingBuildingProduct(input: AddPendingBuildingProduct): Observable<boolean> {
    return this.http.post<any>(this.baseUrl + 'product/addpendingbuildingproduct', input)
      .pipe(tap(() => this.newPendingProductEmitter.emit()));
  }
}
