import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { InvitesComponent } from './invites/invites.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AddDonationComponent } from './products/add-donation.component';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { InputNumberModule } from 'primeng/inputnumber';
import { UserManagementComponent } from './user-management/user-management.component';
import { SuppliesComponent } from './products/supplies/supplies.component';
import { MessageService } from 'primeng/api';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { LocalizedMessageService } from './_services/localized-message.service';
import { LanguageInterceptor } from './_services/language.interceptor';

// AoT requires an exported function for factories
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/locales/');
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    InvitesComponent,
    UserManagementComponent,
    AddDonationComponent,
    SuppliesComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      },
      defaultLanguage: 'hu'
    }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    InputTextModule,
    ButtonModule,
    CheckboxModule,
    TableModule,
    ToastModule,
    AutoCompleteModule,
    DropdownModule,
    InputNumberModule,
    ProgressSpinnerModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'invites', component: InvitesComponent, canActivate: [AuthorizeGuard] },
      { path: 'usermanagement', component: UserManagementComponent, canActivate: [AuthorizeGuard] },
      { path: 'products', component: AddDonationComponent, canActivate: [AuthorizeGuard] },
      { path: 'supplies', component: SuppliesComponent, canActivate: [AuthorizeGuard] },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LanguageInterceptor, multi: true },
    MessageService,
    LocalizedMessageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
