import { Component, Inject, OnDestroy } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Product, SizeType } from '../../models/product';
import { forkJoin, Subject, takeUntil } from 'rxjs';
import { Building } from '../../models/building';
import { extractFirstErrorMessage } from '../../utilities/error-util';
import { TranslateService } from '@ngx-translate/core';
import { LocalizedMessageService } from '../../_services/localized-message.service';
import { Unit } from '../../models/unit';

@Component({
    selector: 'app-add-donation',
    templateUrl: './add-donation.component.html',
    styleUrls: ['./add-donation.component.css']
})
export class AddDonationComponent implements OnDestroy {
    public products: Product[] = [];
    public buildings: Building[] = [];
    public units: Unit[] = [];
    public productsByCategory: Product[] = [];
    public filteredProducts: Product[] = [];
    public categories: { name: string; id: string }[] = [];
    public selectedProduct: Product | undefined;
    public selectedBuilding: Building | undefined;
    public selectedUnit: Unit | undefined;
    public quantity = 1;
    public size: string | undefined;
    public newProductName: string | undefined;
    public saveInProgress = false;
    public isLoading = true;
    public isNewProduct = false;
    public sizeTypes: { name: string; id: SizeType }[] = [];
    public selectedSizeType: { name: string; id: SizeType } | undefined;
    public selectedCategory: { name: string; id: string } | undefined;

    private ngUnsubscribe = new Subject;

    constructor(private http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private messageService: LocalizedMessageService,
        private translateService: TranslateService) {
        this.loadData();
        this.translateService.onLangChange.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => {
            this.loadData();
            this.selectedProduct = undefined;
            this.quantity = 1;
        });
    }

    onCategoryChange(event: any) {
        this.productsByCategory = this.products.filter(x => x.category === event.value.name);
        this.selectedProduct = undefined;
    }

    private loadData(): void {
        this.isLoading = true;
        const products$ = this.http.get<Product[]>(this.baseUrl + 'product/products');
        const units$ = this.http.get<Unit[]>(this.baseUrl + 'product/units');
        const buildings$ = this.http.get<Building[]>(this.baseUrl + 'building/buildings');

        forkJoin([products$, buildings$, units$])
            .subscribe({
                next: ([products, buildings, units]) => {
                    this.products = products;
                    this.buildings = buildings;
                    this.units = units;
                    this.categories = [... new Set(products.map(x => x.category))].map(((x) => {
                        return { name: x, id: x };
                    }));
                    this.sizeTypes = [SizeType.Child, SizeType.Numbered, SizeType.TShirt].map(((x) => {
                        return { name: this.getSizeTypeName(x), id: x };
                    }));
                    this.isLoading = false;
                }, error: (error: HttpErrorResponse) => {
                    console.error(error);
                    this.isLoading = false;
                }
            });
    }

    public getSuffix(): string {
        return this.isNewProduct ? ` ${this.selectedUnit?.name ?? ''}` : ` ${this.selectedProduct?.unitName ?? ''}`;
    }

    private getSizeTypeName(sizeType: SizeType): string {
        switch(sizeType) { 
            case SizeType.Numbered: { 
               return "Számozott";
            } 
            case SizeType.TShirt: { 
               return "Betűs";
            } 
            case SizeType.Child: { 
                return "Gyerek";
            }
        }
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
                this.messageService.add({ severity: 'success', summary: 'MESSAGE.SUCCESS', detail: 'MESSAGE.ADDDONATION.SUCCESS_DETAIL' });
                this.saveInProgress = false;
            }, error: (error: HttpErrorResponse) => {
                this.messageService.add({ severity: 'error', summary: 'MESSAGE.ERROR', detail: 'MESSAGE.ADDDONATION.ERROR_DETAIL' }, extractFirstErrorMessage(error));
                this.saveInProgress = false;
            }
        });
    }

    public getSizeToolTip(): string {
        switch(this.selectedProduct?.sizeType ?? this.selectedSizeType?.id) { 
            case SizeType.Numbered: { 
               return '32, 36, 44';
            } 
            case SizeType.TShirt: { 
               return 'S, M, L, XL';
            } 
            case SizeType.Child: { 
                return '126, 134';
            }
            default: {
                return '';
            }
        }
    }

    public newProductChanged(e: any) {
        if (!e.checked) {
            this.newProductName = undefined;
            this.selectedSizeType = undefined;
            this.selectedUnit = undefined;
        } else {
            this.selectedProduct = undefined;
        }
    }

    public shouldDisableSaveButton(): boolean {
        return this.saveInProgress || !this.selectedBuilding || !this.selectedCategory ||
            (!this.isNewProduct && (!this.selectedProduct || (!!this.selectedProduct.sizeType && (!this.size || this.size.trim() === '')))) ||
            (this.isNewProduct && ((!this.newProductName || this.newProductName.trim() === '') || (!!this.selectedSizeType && (!this.size || this.size.trim() === ''))));
    }

    ngOnDestroy(): void {
        this.ngUnsubscribe.next(null);
        this.ngUnsubscribe.complete();
    }
}
