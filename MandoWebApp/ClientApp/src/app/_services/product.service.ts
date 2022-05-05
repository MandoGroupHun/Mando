import { EventEmitter, Inject, Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { AddPendingBuildingProduct } from "../models/api-inputs/add-pending-building-product";
import { Observable, tap } from 'rxjs';
import { AcceptPendingBuildingProduct } from '../models/api-inputs/accept-pending-building-product';
import { AddBuildingProduct } from '../models/api-inputs/add-building-product';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  public pendingProductCountEmitter = new EventEmitter();

  constructor(@Inject('BASE_URL') public baseUrl: string, private http: HttpClient) {

  }

  public addBuildingProduct(input: AddBuildingProduct): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'product/addbuildingproduct', input);
  }

  public addPendingBuildingProduct(input: AddPendingBuildingProduct): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'product/addpendingbuildingproduct', input)
      .pipe(tap(() => this.pendingProductCountEmitter.emit()));
  }

  public acceptPendingBuildingProduct(input: AcceptPendingBuildingProduct): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'product/acceptpendingbuildingproduct', input)
      .pipe(tap(() => this.pendingProductCountEmitter.emit()));
  }

  public deletePendingBuildingProduct(pendingBuildingProductId: number): Observable<any> {
    return this.http.delete<any>(this.baseUrl + `product/declinePendingBuildingProduct?pendingBuildingProductId=${pendingBuildingProductId}`)
      .pipe(tap(() => this.pendingProductCountEmitter.emit()));
  }
}
