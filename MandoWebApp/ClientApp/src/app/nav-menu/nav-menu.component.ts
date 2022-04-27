import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Profile } from 'oidc-client';
import { firstValueFrom } from 'rxjs';
import { AuthorizeService, isInRole } from '../../api-authorization/authorize.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  public user: Profile | null = null;

  constructor(public authorizeService: AuthorizeService, public translateService: TranslateService) {
    authorizeService.getUser().subscribe({
      next: user => {
        this.user = user;
      }
    });
  }

  public isPriviliged(): boolean {
    return isInRole(this.user, 'Administrator') || isInRole(this.user, 'Manager');
  }

  public async setLanguage(lang: string) {
    await firstValueFrom(this.translateService.use(lang));
  }

  public getCurrentLanguage(): string {
    return this.translateService.currentLang;
  }

  public getLanguages(): string[] {
    return this.translateService.langs;
  }

  collapse(): void {
    this.isExpanded = false;
  }

  toggle(): void {
    this.isExpanded = !this.isExpanded;
  }
}
