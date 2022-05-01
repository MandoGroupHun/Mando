import { Component, Inject, OnDestroy } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Table } from 'primeng/table';
import { TranslateService } from '@ngx-translate/core';
import { Subject, takeUntil } from 'rxjs';
import { PendingDonation } from '../../models/pendingdonation';

@Component({
  selector: 'app-pending-donations',
  templateUrl: './pending-donations.component.html'
})
export class PendingDonationsComponent implements OnDestroy {
  public pendingDonations: PendingDonation[] = [];

  private ngUnsubscribe = new Subject;

  constructor(private http: HttpClient, @Inject('BASE_URL') public baseUrl: string,
    private translateService: TranslateService) {
    this.loadPendingDonations();
    this.translateService.onLangChange.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => this.loadPendingDonations());
  }

  public filterTable(dataTable: Table, $event: any) {
    dataTable.filterGlobal(($event.target as HTMLTextAreaElement).value, 'contains');
  }

  private loadPendingDonations(): void {
    this.http.get<PendingDonation[]>(this.baseUrl + 'product/pendingdonations').subscribe({
      next: result => {
        this.pendingDonations = result;
      }, error: (error: HttpErrorResponse) => console.error(error)
    });
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next(null);
    this.ngUnsubscribe.complete();
  }
}
