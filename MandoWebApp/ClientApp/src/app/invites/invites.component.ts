import { Component, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { extractFirstErrorMessage } from '../utilities/error-util';
import { LocalizedMessageService } from '../_services/localized-message.service';

@Component({
  selector: 'app-invites',
  templateUrl: './invites.component.html'
})
export class InvitesComponent {
  public inviteEmail: string | undefined = undefined;
  public invites: Invite[] = [];

  constructor(private http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private messageService: LocalizedMessageService) {

    this.loadInvites();
  }

  private loadInvites(): void {
    this.http.get<Invite[]>(this.baseUrl + 'invite').subscribe(result => {
      this.invites = result;
    }, error => console.error(error));
  }

  public createInvite(): void {
    this.http.post<boolean>(this.baseUrl + 'invite', { email: this.inviteEmail })
      .subscribe(newAdded => {
        if (newAdded) {
          this.messageService.add({ severity: 'success', summary: 'MESSAGE.INVITE.SUCCESS', detail: 'MESSAGE.INVITE.SUCCESS_DETAIL' });
          this.loadInvites();
        }
        else {
          this.messageService.add({ severity: 'warn', summary: 'MESSAGE.INVITE.DUPLICATE', detail: 'MESSAGE.INVITE.DUPLICATE_DETAIL' });
        }
      }, (error: HttpErrorResponse) => {
        this.messageService.add({ severity: 'error', summary: 'MESSAGE.INVITE.ERROR', detail: 'MESSAGE.INVITE.ERROR_DETAIL' }, extractFirstErrorMessage(error));
      });
  }
}

interface Invite {
  inviteId: string;
  email: string;
  status: string;
  createdAt: string;
}
