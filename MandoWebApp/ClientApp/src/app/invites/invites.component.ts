import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-invites',
  templateUrl: './invites.component.html'
})
export class InvitesComponent {
  public invites: Invite[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Invite[]>(baseUrl + 'invite').subscribe(result => {
      this.invites = result;
    }, error => console.error(error));
  }
}

interface Invite {
  inviteId: string;
  email: string;
  status: string;
  createdAt: string;
}
