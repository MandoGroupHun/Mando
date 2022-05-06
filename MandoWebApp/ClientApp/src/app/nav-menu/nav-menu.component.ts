import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Profile } from 'oidc-client';
import { firstValueFrom } from 'rxjs';
import { AuthorizeService, isInRole } from '../../api-authorization/authorize.service';
import { ProductService } from '../_services/product.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  pendingDonationCount: number | undefined = undefined;
  public user: Profile | null = null;

  constructor(public authorizeService: AuthorizeService,
    @Inject('BASE_URL') private baseUrl: string,
    private http: HttpClient,
    private productService: ProductService,
    public translateService: TranslateService) {
    this.productService.pendingProductCountEmitter.subscribe({
      next: () => {
        this.getPendingDonationCount();
      }
    });

    authorizeService.getUser().subscribe({
      next: user => {
        this.user = user;
        this.getPendingDonationCount();
      }
    });
  }

  private getPendingDonationCount(): void {
    if (!this.isPriviliged()) {
      return;
    }

    this.http.get<number>(this.baseUrl + 'product/pendingdonationcount').subscribe({
      next: result => this.pendingDonationCount = result,
      error: (error: HttpErrorResponse) => console.error(error)
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
