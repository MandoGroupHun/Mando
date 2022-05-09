import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { LocalizedMessageService } from '../_services/localized-message.service';
import { UnitService } from '../_services/unit.service';

@Component({
  selector: 'app-units',
  templateUrl: './units.component.html',
  styleUrls: ['./units.component.css']
})
export class UnitsComponent {

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
