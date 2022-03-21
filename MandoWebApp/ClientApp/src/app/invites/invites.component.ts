import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-invites',
  templateUrl: './invites.component.html'
})
export class InvitesComponent {
  public inviteEmail: string | undefined = undefined;
  public invites: Invite[] = [];

  constructor(private http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {

    this.loadInvites();
  }

  private loadInvites(): void {
    this.http.get<Invite[]>(this.baseUrl + 'invite').subscribe(result => {
      this.invites = result;
    }, error => console.error(error));
  }

  public createInvite(): void {
    this.http.post(this.baseUrl + 'invite', { email: this.inviteEmail })
      .subscribe(() => this.loadInvites()
        , error => console.error(error))
  }
}

interface Invite {
  inviteId: string;
  email: string;
  status: string;
  createdAt: string;
}
