import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';

import { routes } from './app.routes';
import { provideHighcharts } from 'highcharts-angular';
import { provideToastr } from 'ngx-toastr';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { HttpErrorInterceptor } from './core/http-error-interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideHighcharts({
      instance: () => import('highcharts'), // dynamically load Highcharts
    }),
    provideAnimations(),
    provideToastr(),
    provideHttpClient(
      withInterceptors([HttpErrorInterceptor])
    )
  ]
};
