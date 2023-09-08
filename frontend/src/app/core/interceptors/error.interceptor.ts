import { HttpErrorResponse, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@core/services/auth.service';
import { throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

import { AccessTokenDto } from 'src/app/models/auth/access-token-dto';
import { ErrorDetailsDto } from 'src/app/models/error/error-details-dto';
import { ErrorType } from 'src/app/models/error/error-type';

@Injectable({
    providedIn: 'root',
})
export class ErrorInterceptor implements HttpInterceptor {
    // eslint-disable-next-line no-empty-function
    constructor(private authService: AuthService, private router: Router) {}

    intercept(request: HttpRequest<unknown>, next: HttpHandler) {
        return next.handle(request).pipe(
            catchError((error: HttpErrorResponse) => {
                if (error.status === 401) {
                    console.log('entered 401 response');
                    console.log(error);
                    console.log(error.headers);
                    console.log(error.headers.get('Token-Expired'));
                    //this.handleUnauthorizedError(request, error, next);
                    if (error.headers.has('Token-Expired')) {
                        console.log('entered access token expired section');

                        return this.authService.refreshTokens().pipe(
                            switchMap((resp: AccessTokenDto) => {
                                console.log('copying tokens to header..');
                                request = request.clone({
                                    setHeaders: {
                                        Authorization: `Bearer ${resp.accessToken}`,
                                    },
                                });
                                console.log('got token and saved:');
                                console.log(resp.refreshToken);

                                return next.handle(request);
                            }),
                        );
                    }

                    const err = error.error as ErrorDetailsDto;

                    if (err && err.errorType === ErrorType.RefreshTokenExpired) {
                        console.log('entered expired refresh token section');
                        this.authService.signOut();

                        return throwError(err);
                    }
                }

                return throwError(() => error.error);
            }),
        );
    }

    // handleUnauthorizedError(request: HttpRequest<unknown>, error: HttpErrorResponse, next: HttpHandler) {

    // }
}
