import { Component, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { Product } from '../models/product';

@Component({
    selector: 'app-add-product-building',
    templateUrl: './add-product-building.component.html',
    providers: [MessageService]
})
export class AddProductBuildingComponent {
    public products: Product[] = [];
    public productsByCategory: Product[] = [];
    public filteredProducts: Product[] = [];
    public categories: any[] = [];
    public selectedProduct: Product | undefined;
    public quantity = 1;
    public size: string | undefined;
    public saveInProgress = false;

    constructor(private http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private messageService: MessageService) {
        this.loadProducts();
    }

    onCategoryChange(event: any) {
        this.productsByCategory = this.products.filter(x => x.category === event.value.name);
    }

    private loadProducts(): void {
        this.http.get<Product[]>(this.baseUrl + 'product').subscribe(result => {
            this.products = result;
            this.categories = [... new Set(result.map(x => x.category))].map(((x) => {
                return { name: x, id: x };
            }));
            }, error => console.error(error));
    }

    public getSuffix(): string {
        return ` ${this.selectedProduct?.unitName ?? ''}`;
    }

    public filterProduct(event: any): void {
        let filtered : any[] = [];
        let query = event.query;

        for (let i = 0; i < this.productsByCategory.length; i++) {
            let product = this.productsByCategory[i];
            if (product.name.toLowerCase().indexOf(query.toLowerCase()) == 0) {
                filtered.push(product);
            }
        }
        this.filteredProducts = filtered;
    }

    public createBuildingProduct(): void {
        this.saveInProgress = true;
        this.http.post<boolean>(this.baseUrl + 'product', {
            productId: this.selectedProduct!.productId,
            quantity: this.quantity,
            size: !!this.selectedProduct!.sizeType ? this.size : null
        })
          .subscribe(_ => {
            this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Building product added!' });
            this.saveInProgress = false;
          }, (error: HttpErrorResponse) => {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'We failed to save the building product.' });
            this.saveInProgress = false;
          });
      }
}

