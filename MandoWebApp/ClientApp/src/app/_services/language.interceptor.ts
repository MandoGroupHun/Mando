import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class LanguageInterceptor implements HttpInterceptor {

  // Injecting TranslateService itself would cause a circular dependency because TranslateService
  // depends on HttpClient for fetching translation files
  constructor(private readonly injector: Injector) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    try {
      const translateService = this.injector.get(TranslateService);

      const langReq = req.clone({
        headers: req.headers.set('X-Language', translateService.currentLang)
      });

      return next.handle(langReq);
    } catch {
      return next.handle(req);
    }
  }
}
