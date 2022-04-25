import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Supply } from 'src/app/models/supply';
import { Table } from 'primeng/table';
import { extractFirstErrorMessage } from '../../utilities/error-util';
import { LocalizedMessageService } from 'src/app/_services/localized-message.service';

@Component({
  selector: 'app-supplies',
  templateUrl: './supplies.component.html'
})
export class SuppliesComponent {
  public supplies: Supply[] = [];
  public selectedSupply: Supply | undefined = undefined;
  public quantitySnapshot: number | undefined = undefined;

  constructor(private http: HttpClient, @Inject('BASE_URL') public baseUrl: string, public messageService: LocalizedMessageService) {
    this.loadSupplies();
  }

  public filterTable(dataTable: Table, $event: any) {
    dataTable.filterGlobal(($event.target as HTMLTextAreaElement).value, 'contains');
  }

  public selectForEdit(supply: Supply): void {
    this.selectedSupply = supply;
    this.quantitySnapshot = supply.quantity;
  }

  public save(supply: Supply): void {
    this.http.post<any>(this.baseUrl + 'product/supplyUpdate', supply).subscribe(() => {
      this.messageService.add({ severity: 'success', summary: 'MESSAGE.SUCCESS', detail: 'MESSAGE.SUPPLIES.SUCCESS_DETAIL' });
      this.selectedSupply = undefined;
    }, error => {
      this.messageService.add({ severity: 'error', summary: 'MESSAGE.ERROR', detail: 'MESSAGE.SUPPLIES.ERROR_DETAIL' }, extractFirstErrorMessage(error));
    });
  }

  public cancelEdit(supply: Supply): void {
    supply.quantity = this.quantitySnapshot!;
    this.selectedSupply = undefined;
    this.quantitySnapshot = undefined;
  }

  public isEditing(supply: Supply): boolean {
    return supply === this.selectedSupply;
  }

  private loadSupplies(): void {
    this.http.get<Supply[]>(this.baseUrl + 'product/supplies').subscribe(result => {
      this.supplies = result;
    }, error => console.error(error));
  }
}

