import { HttpErrorResponse, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '@core/services/auth.service';
import { throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root',
})
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService) {
        // Intentionally left empty for dependency injection purposes only
    }

    intercept(request: HttpRequest<unknown>, next: HttpHandler) {
        return next.handle(request).pipe(
            catchError((error: HttpErrorResponse) => {
                if (error.status === 401 && error.headers.has('Token-Expired')) {
                    return this.authService.refreshTokens().pipe(switchMap(() => next.handle(request)));
                }

                if (error.status === 403) {
                    this.authService.signOut();
                }

                return throwError(() => error.error);
            }),
        );
    }
}
