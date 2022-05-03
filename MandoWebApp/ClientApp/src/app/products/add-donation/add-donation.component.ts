import { Component, Inject, OnDestroy, Optional } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Product, SizeType } from '../../models/product';
import { forkJoin, Subject, takeUntil } from 'rxjs';
import { Building } from '../../models/building';
import { extractFirstErrorMessage } from '../../utilities/error-util';
import { TranslateService } from '@ngx-translate/core';
import { LocalizedMessageService } from '../../_services/localized-message.service';
import { Unit } from '../../models/unit';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Category } from '../../models/category';

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
    public categories: Category[] = [];
    public selectedProduct: Product | undefined;
    public selectedBuilding: Building | undefined;
    public selectedUnit: Unit | undefined;
    public quantity = 1;
    public size: string | undefined;
    public newHuProductName: string | undefined;
    public newEnProductName: string | undefined;
    public saveInProgress = false;
    public isLoading = true;
    public isNewProduct = false;
    public sizeTypes: { name: string; id: SizeType }[] = [];
    public selectedSizeType: { name: string; id: SizeType } | undefined;
    public selectedCategory: Category | undefined;
    public isPendingDonation = false;
    public pendingDonationId: number | undefined;

    private ngUnsubscribe = new Subject;

    constructor(private http: HttpClient, @Inject('BASE_URL') public baseUrl: string,
        private messageService: LocalizedMessageService,
        private translateService: TranslateService,
        @Optional() public dialogRef: DynamicDialogRef,
        @Optional() public dialogConfig: DynamicDialogConfig) {
        this.isPendingDonation = !!dialogRef;
        this.loadData();
        this.translateService.onLangChange.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => {
            this.loadData();
            this.resetOptions();
        });
    }

    private resetOptions() {
        if (this.isPendingDonation) {
            return;
        }

        this.selectedProduct = undefined;
        this.selectedCategory = undefined;
        this.selectedUnit = undefined;
        this.size = undefined;
        this.isNewProduct = false;
        this.selectedSizeType = undefined;
        this.quantity = 1;
        this.newHuProductName = undefined;
        this.newEnProductName = undefined;
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
        const categories$ = this.http.get<Category[]>(this.baseUrl + 'product/categories');

        forkJoin([products$, buildings$, units$, categories$])
            .subscribe({
                next: ([products, buildings, units, categories]) => {
                    this.products = products;
                    this.buildings = buildings;
                    this.units = units;
                    this.categories = categories;
                    
                    const sizeTypeNumbered = this.translateService.get('ADDDONATION.SIZETYPE.NUMBERED');
                    const sizeTypeTShirt = this.translateService.get('ADDDONATION.SIZETYPE.CHARACTER');
                    const sizeTypeChild = this.translateService.get('ADDDONATION.SIZETYPE.CHILD');
                
                    forkJoin([sizeTypeNumbered, sizeTypeTShirt, sizeTypeChild]).subscribe({
                      next: ([numbered, tshirt, child]) => {
                        this.sizeTypes = [SizeType.Child, SizeType.Numbered, SizeType.TShirt].map(((x) => {
                            return { name: x === SizeType.Child ? child :
                                x === SizeType.Numbered ? numbered : tshirt, id: x };
                        }));

                        if (!!this.dialogConfig) {
                            this.isNewProduct = true;
                            this.selectedCategory = this.categories.find(x => x.categoryId === this.dialogConfig.data.categoryId);
                            this.productsByCategory = this.products.filter(x => x.category === this.selectedCategory?.name);
                            this.newEnProductName = this.dialogConfig.data.enProductName;
                            this.newHuProductName = this.dialogConfig.data.huProductName;
                            this.quantity = this.dialogConfig.data.quantity;
                            this.selectedUnit = this.units.find(x => x.unitId === this.dialogConfig.data.unitId);
                            this.selectedSizeType = this.sizeTypes.find(x => x.id === this.dialogConfig.data.sizeType);
                            this.size = this.dialogConfig.data.size;
                            this.pendingDonationId = this.dialogConfig.data.pendingDonationId;
                        }
                      }, error: error => console.error(error)
                    });

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

        if (this.isPendingDonation) {
            this.acceptPendingDonation();
        } else if (this.isNewProduct) {
            this.createNewPendingDonation();
        } else {
            this.createNewDonation();
        }
    }

    private createNewDonation(): void {
        this.http.post<boolean>(this.baseUrl + 'product/addbuildingproduct', {
            productId: this.selectedProduct!.productId,
            buildingId: this.selectedBuilding!.buildingId,
            quantity: this.quantity,
            size: !!this.selectedProduct!.sizeType ? this.size : null
        }).subscribe({
            next: () => {
                this.messageService.add({ severity: 'success', summary: 'MESSAGE.SUCCESS', detail: 'MESSAGE.ADDDONATION.SUCCESS_DETAIL' });
                this.saveInProgress = false;
                this.resetOptions();
            }, error: (error: HttpErrorResponse) => {
                this.messageService.add({ severity: 'error', summary: 'MESSAGE.ERROR', detail: 'MESSAGE.ADDDONATION.ERROR_DETAIL' }, extractFirstErrorMessage(error));
                this.saveInProgress = false;
            }
        });
    }

    private createNewPendingDonation(): void {
        this.http.post<boolean>(this.baseUrl + 'product/addpendingbuildingproduct', {
            categoryId: this.selectedCategory!.categoryId,
            huProductName: this.newHuProductName,
            enProductName: this.newEnProductName,
            buildingId: this.selectedBuilding!.buildingId,
            quantity: this.quantity,
            sizeType: this.selectedSizeType?.id,
            size: !!this.selectedSizeType ? this.size : null,
            unitId: this.selectedUnit!.unitId
        }).subscribe({
            next: () => {
                this.messageService.add({ severity: 'success', summary: 'MESSAGE.SUCCESS', detail: 'MESSAGE.ADDDONATION.SUCCESS_DETAIL' });
                this.saveInProgress = false;
                this.resetOptions();
            }, error: (error: HttpErrorResponse) => {
                this.messageService.add({ severity: 'error', summary: 'MESSAGE.ERROR', detail: 'MESSAGE.ADDDONATION.ERROR_DETAIL' }, extractFirstErrorMessage(error));
                this.saveInProgress = false;
            }
        });
    }

    private acceptPendingDonation(): void {
        this.http.post<boolean>(this.baseUrl + 'product/acceptpendingbuildingproduct', {
            pendingBuildingProductId: this.pendingDonationId,
            categoryId: this.selectedCategory!.categoryId,
            huProductName: this.newHuProductName,
            enProductName: this.newEnProductName,
            buildingId: this.selectedBuilding!.buildingId,
            quantity: this.quantity,
            sizeType: this.selectedSizeType?.id,
            size: !!this.selectedSizeType ? this.size : null,
            unitId: this.selectedUnit!.unitId,
            productId: !this.isNewProduct ? this.selectedProduct?.productId : null
        }).subscribe({
            next: () => {
                this.dialogRef.close(true);
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
        if (this.isPendingDonation) {
            return;
        }

        if (!e.checked) {
            this.newHuProductName = undefined;
            this.newEnProductName = undefined;
            this.selectedSizeType = undefined;
            this.selectedUnit = undefined;
        } else {
            this.selectedProduct = undefined;
        }
    }

    public shouldDisableSaveButton(): boolean {
        return this.saveInProgress || !this.selectedBuilding || !this.selectedCategory ||
            (!this.isNewProduct && (!this.selectedProduct || (!!this.selectedProduct.sizeType && (!this.size || this.size.trim() === '')))) ||
            (this.isNewProduct && (!this.selectedUnit || (!!this.selectedSizeType && (!this.size || this.size.trim() === '')) ||
                (!this.isPendingDonation && ((!this.newHuProductName || this.newHuProductName.trim() === '') && (!this.newEnProductName || this.newEnProductName.trim() === ''))) ||
                (this.isPendingDonation && ((!this.newHuProductName || this.newHuProductName.trim() === '') || (!this.newEnProductName || this.newEnProductName.trim() === '')))));
    }

    ngOnDestroy(): void {
        this.ngUnsubscribe.next(null);
        this.ngUnsubscribe.complete();
    }
}
