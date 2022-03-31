import { Component } from '@angular/core';
import { Profile } from 'oidc-client';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthorizeService, isInRole } from '../../api-authorization/authorize.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  public user: Profile | null = null;

  constructor(public authorizeService: AuthorizeService) {

    authorizeService.getUser().subscribe(user => {
      this.user = user;
    });
  }

  public showInvites(): boolean {
    return isInRole(this.user, 'Administrator') || isInRole(this.user, 'Manager');
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
