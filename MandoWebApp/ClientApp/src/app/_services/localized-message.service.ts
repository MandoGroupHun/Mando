import { TranslateService } from "@ngx-translate/core";
import { Message, MessageService } from "primeng/api";
import { forkJoin } from "rxjs";
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LocalizedMessageService {
  constructor(private messageService: MessageService, private translateService: TranslateService) {

  }

  public add(message: Message, appendRawDetail: string | undefined = undefined): void {
    const translatedSummary = this.translateService.get(message.summary!);
    const translatedDetail = this.translateService.get(message.detail!);

    forkJoin([translatedSummary, translatedDetail]).subscribe({
      next: ([summary, detail]) => {
        message.summary = summary;
        message.detail = detail + (appendRawDetail ?? '');

        this.messageService.add(message);
      }, error: error => console.error(error)
    });
  }
}
