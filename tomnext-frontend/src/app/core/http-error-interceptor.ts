import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const HttpErrorInterceptor: HttpInterceptorFn = (req, next) => {
  
  return next(req).pipe(
      catchError((error: HttpErrorResponse) => {
        return throwError(() => error);
      })
    );
};
