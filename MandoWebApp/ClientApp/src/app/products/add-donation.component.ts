import { Component, Inject, OnDestroy } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { Product } from '../models/product';
import { forkJoin, Subject, takeUntil } from 'rxjs';
import { Building } from '../models/building';
import { extractFirstErrorMessage } from '../utilities/error-util';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-add-donation',
    templateUrl: './add-donation.component.html',
    styleUrls: ['./add-donation.component.css']
})
export class AddDonationComponent implements OnDestroy {
    public products: Product[] = [];
    public buildings: Building[] = [];
    public productsByCategory: Product[] = [];
    public filteredProducts: Product[] = [];
    public categories: any[] = [];
    public selectedProduct: Product | undefined;
    public selectedBuilding: Building | undefined;
    public quantity = 1;
    public size: string | undefined;
    public saveInProgress = false;
    public isLoading = true;

    private ngUnsubscribe = new Subject;

    constructor(private http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private messageService: MessageService,
        private translateService: TranslateService) {
        this.loadData();
        this.translateService.onLangChange.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => this.loadData());
    }

    onCategoryChange(event: any) {
        this.productsByCategory = this.products.filter(x => x.category === event.value.name);
    }

    private loadData(): void {
        this.isLoading = true;
        const products$ = this.http.get<Product[]>(this.baseUrl + 'product/products');
        const buildings$ = this.http.get<Building[]>(this.baseUrl + 'building/buildings');

        forkJoin([products$, buildings$])
            .subscribe({
                next: ([products, buildings]) => {
                    this.products = products;
                    this.buildings = buildings;
                    this.categories = [... new Set(products.map(x => x.category))].map(((x) => {
                        return { name: x, id: x };
                    }));
                    this.isLoading = false;
                }, error: (error: HttpErrorResponse) => {
                    console.error(error);
                    this.isLoading = false;
                }
            });
    }

    public getSuffix(): string {
        return ` ${this.selectedProduct?.unitName ?? ''}`;
    }

    public filterProduct(event: any): void {
        let filtered: any[] = [];
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
        this.http.post<boolean>(this.baseUrl + 'product/add', {
            productId: this.selectedProduct!.productId,
            buildingId: this.selectedBuilding!.buildingId,
            quantity: this.quantity,
            size: !!this.selectedProduct!.sizeType ? this.size : null
        }).subscribe({
            next: () => {
                this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Building product added!' });
                this.saveInProgress = false;
            }, error: (error: HttpErrorResponse) => {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'We failed to save the building product. Details: ' + extractFirstErrorMessage(error) });
                this.saveInProgress = false;
            }
        });
    }

    ngOnDestroy(): void {
        this.ngUnsubscribe.next(null);
        this.ngUnsubscribe.complete();
    }
}
