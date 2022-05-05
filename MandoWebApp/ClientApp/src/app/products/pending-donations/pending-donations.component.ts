import { Component, Inject, OnDestroy } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Table } from 'primeng/table';
import { TranslateService } from '@ngx-translate/core';
import { Subject, takeUntil } from 'rxjs';
import { PendingDonation } from '../../models/pendingdonation';
import { DialogService } from 'primeng/dynamicdialog';
import { AddDonationComponent } from '../add-donation/add-donation.component';
import { LocalizedMessageService } from '../../_services/localized-message.service';
import { ConfirmationService } from 'primeng/api';
import { extractFirstErrorMessage } from '../../utilities/error-util';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  selector: 'app-pending-donations',
  templateUrl: './pending-donations.component.html',
  providers: [DialogService]
})
export class PendingDonationsComponent implements OnDestroy {
  public pendingDonations: PendingDonation[] = [];

  private ngUnsubscribe = new Subject;

  constructor(private http: HttpClient, @Inject('BASE_URL') public baseUrl: string,
    private translateService: TranslateService,
    private messageService: LocalizedMessageService,
    public dialogService: DialogService,
    private productService: ProductService,
    private confirmationService: ConfirmationService) {
    this.loadPendingDonations();
    this.translateService.onLangChange.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => this.loadPendingDonations());
  }

  public filterTable(dataTable: Table, $event: any) {
    dataTable.filterGlobal(($event.target as HTMLTextAreaElement).value, 'contains');
  }

  private loadPendingDonations(): void {
    this.http.get<PendingDonation[]>(this.baseUrl + 'product/pendingdonations').subscribe({
      next: result => {

        result.forEach(x => {
          if (!!x.huProductName && !!x.enProductName) {
            x.productName = this.translateService.currentLang === 'hu' ? `${x.huProductName} (${x.enProductName})` : `${x.enProductName} (${x.huProductName})`;
          } else {
            x.productName = !!x.huProductName ? x.huProductName : x.enProductName;
          }
        });

        this.pendingDonations = result;
      }, error: (error: HttpErrorResponse) => console.error(error)
    });
  }

  editPendingDonation(pendingDonation: PendingDonation) {
    this.translateService.get('PENDING_DONATIONS.EDIT_DONATION').subscribe({
      next: (message) => {
        const ref = this.dialogService.open(AddDonationComponent, {
          data: {
            pendingDonationId: pendingDonation.pendingDonationId,
            categoryId: pendingDonation.categoryId,
            huProductName: pendingDonation.huProductName,
            enProductName: pendingDonation.enProductName,
            unitId: pendingDonation.unitId,
            quantity: pendingDonation.quantity,
            sizeType: pendingDonation.sizeType,
            size: pendingDonation.size,
            buildingId: pendingDonation.buildingId
          },
          header: message,
          width: '50%'
        });

        ref.onClose.subscribe((success: boolean) => {
          if (success) {
            this.messageService.add({ severity: 'success', summary: 'MESSAGE.SUCCESS', detail: 'MESSAGE.PENDING_DONATIONS.SUCCESS_DETAIL' });
            this.loadPendingDonations();
          }
        });
      }, error: error => console.error(error)
    });
  }

  deletePendingDonation(pendingDonation: PendingDonation) {
    this.translateService.get('MESSAGE.PENDING_DONATIONS.DELETE_QUESTION').subscribe({
      next: (message) => {
        this.confirmationService.confirm({
          message: message,
          accept: () => {
            this.productService.deletePendingBuildingProduct(pendingDonation.pendingDonationId).subscribe({
              next: () => {
                this.messageService.add({ severity: 'success', summary: 'MESSAGE.SUCCESS', detail: 'MESSAGE.PENDING_DONATIONS.SUCCESS_DELETE' });
                this.loadPendingDonations();
              }, error: (error: HttpErrorResponse) => {
                this.messageService.add({ severity: 'error', summary: 'MESSAGE.ERROR', detail: 'MESSAGE.PENDING_DONATIONS.ERROR_DELETE' }, extractFirstErrorMessage(error));
              }
            });
          }
        });
      }, error: error => console.error(error)
    });
  }

  acceptPendingDonation(pendingDonation: PendingDonation) {
    this.translateService.get('MESSAGE.PENDING_DONATIONS.ACCEPT_QUESTION').subscribe({
      next: (message) => {
        this.confirmationService.confirm({
          message: message,
          accept: () => {
            this.productService.acceptPendingBuildingProduct({
              pendingBuildingProductId: pendingDonation.pendingDonationId,
              categoryId: pendingDonation.categoryId,
              huProductName: pendingDonation.huProductName,
              enProductName: pendingDonation.enProductName,
              buildingId: pendingDonation.buildingId,
              quantity: pendingDonation.quantity,
              sizeType: pendingDonation.sizeType,
              size: pendingDonation.size,
              unitId: pendingDonation.unitId,
              productId: undefined
            }).subscribe({
              next: () => {
                this.messageService.add({ severity: 'success', summary: 'MESSAGE.SUCCESS', detail: 'MESSAGE.ADDDONATION.SUCCESS_DETAIL' });
                this.loadPendingDonations();
              }, error: (error: HttpErrorResponse) => {
                this.messageService.add({ severity: 'error', summary: 'MESSAGE.ERROR', detail: 'MESSAGE.ADDDONATION.ERROR_DETAIL' }, extractFirstErrorMessage(error));
              }
            });
          }
        });
      }, error: error => console.error(error)
    });
  }

  isAcceptDisabled(pendingDonation: PendingDonation) {
    return !pendingDonation.huProductName || pendingDonation.huProductName.trim() === '' ||
      !pendingDonation.enProductName || pendingDonation.enProductName.trim() === '';
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next(null);
    this.ngUnsubscribe.complete();
  }
}
