import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthorizeService, IUser } from '../../api-authorization/authorize.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  public user: Observable<IUser | null>;

  constructor(public userManager: AuthorizeService) {
    this.user = userManager.getUser();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
