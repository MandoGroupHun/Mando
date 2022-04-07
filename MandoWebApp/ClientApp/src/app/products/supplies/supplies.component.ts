import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Supply } from 'src/app/models/supply';

@Component({
  selector: 'app-supplies',
  templateUrl: './supplies.component.html'
})
export class SuppliesComponent {
  public supplies: Supply[] = [];

  constructor(private http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    this.loadProducts();
  }

  private loadProducts(): void {
    this.http.get<Supply[]>(this.baseUrl + 'product/supplies').subscribe(result => {
      this.supplies = result;
    }, error => console.error(error));
  }
}

