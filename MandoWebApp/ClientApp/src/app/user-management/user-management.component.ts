
import { Component, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  providers: [MessageService]
})
export class UserManagementComponent {
  public users: UserManagementItem[] = [];

  constructor(private http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private messageService: MessageService) {
    this.loadInvites();
  }

  private loadInvites(): void {
    this.http.get<UserManagementItem[]>(this.baseUrl + 'usermanagement').subscribe(result => {
      this.users = result;
    }, error => console.error(error));
  }

  //public createInvite(): void {
  //  this.http.post<boolean>(this.baseUrl + 'invite', { email: this.inviteEmail })
  //    .subscribe(newAdded => {
  //      if (newAdded) {
  //        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Invite successfully sent!' });
  //        this.loadInvites();
  //      }
  //      else {
  //        this.messageService.add({ severity: 'warn', summary: 'Duplicate invite', detail: 'This email has already received an invite!' });
  //      }
  //    }, (error: HttpErrorResponse) => {
  //      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'We failed to send an invite to this email. Details: ' + extractFirstErrorMessage(error) });
  //    });
  //}
}

interface UserManagementItem {
  id: string;
  name: string;
  roles: string[];
}

function extractFirstErrorMessage(error: HttpErrorResponse) {
  return error.error.errors[Object.keys(error.error.errors)[0]];
}
