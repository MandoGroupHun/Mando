import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { LocalizedMessageService } from '../../_services/localized-message.service';
import { UnitService } from '../../_services/unit.service';

@Component({
  selector: 'app-add-unit',
  templateUrl: './add-unit.component.html',
  styleUrls: ['./add-unit.component.css']
})
export class AddUnitComponent {

  public newUnitHuName: string | undefined;
  public newUnitEnName: string | undefined;

  constructor(
  @Inject('BASE_URL') public baseUrl: string,
  private messageService: LocalizedMessageService,
  private unitService: UnitService) {};

  createUnit() {
    this.unitService.addUnit({
      ENName: this.newUnitEnName?.toLocaleLowerCase(),
      HUName: this.newUnitHuName?.toLocaleLowerCase()
    }).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'MESSAGE.SUCCESS', detail: 'MESSAGE.UNITS.SUCCESS_DETAIL' });
        this.resetFields();
      },
      error: (error: HttpErrorResponse) => {
        this.messageService.add({ severity: 'error', summary: 'MESSAGE.ERROR', detail: 'MESSAGE.UNITS.ERROR_DETAIL' });
      }
    })
  }

  resetFields() {
    this.newUnitHuName = undefined
    this.newUnitEnName = undefined
  }
}
