import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Supply } from 'src/app/models/supply';
import { Table } from 'primeng/table';
import { MessageService } from 'primeng/api';
import { extractFirstErrorMessage } from '../../utilities/error-util';

@Component({
  selector: 'app-supplies',
  templateUrl: './supplies.component.html'
})
export class SuppliesComponent {
  public supplies: Supply[] = [];
  public selectedSupply: Supply | undefined = undefined;

  constructor(private http: HttpClient, @Inject('BASE_URL') public baseUrl: string, public messageService: MessageService) {
    this.loadSupplies();
  }

  public filterTable(dataTable: Table, $event: any) {
    dataTable.filterGlobal(($event.target as HTMLTextAreaElement).value, 'contains');
  }

  public selectForEdit(supply: Supply): void {
    this.selectedSupply = supply;
  }

  public save(supply: Supply): void {
    this.http.post<any>(this.baseUrl + 'product/supplyUpdate', supply).subscribe(() => {
      this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Successfully updated supply quantity' });
      this.selectedSupply = undefined;
    }, error => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'We failed to update supply quantity. Details: ' + extractFirstErrorMessage(error) });
    });
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

