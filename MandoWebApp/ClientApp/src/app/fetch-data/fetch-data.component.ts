import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class InviteComponent {
  public invites: Invite[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Invite[]>(baseUrl + 'invites').subscribe(result => {
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
