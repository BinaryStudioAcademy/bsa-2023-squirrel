import { HttpErrorResponse, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '@core/services/auth.service';
import { throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root',
})
export class ErrorInterceptor implements HttpInterceptor {
    private readonly unauthorizedErrorStatusCode = 401;

    private readonly forbiddenErrorStatusCode = 403;

    constructor(private authService: AuthService) { }

    intercept(request: HttpRequest<unknown>, next: HttpHandler) {
        return next.handle(request).pipe(
            catchError((error: HttpErrorResponse) => {
                if (error.status === this.unauthorizedErrorStatusCode && error.headers.has('Token-Expired')) {
                    return this.authService.refreshTokens().pipe(switchMap(() => next.handle(request)));
                }

                if (error.status === this.forbiddenErrorStatusCode) {
                    this.authService.signOut();
                }

                return throwError(() => error.error);
            }),
        );
    }
}
