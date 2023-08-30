import {
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '@core/services/auth.service';
import { Observable } from 'rxjs';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    // eslint-disable-next-line no-empty-function
    constructor(private authService: AuthService) {}

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        const { accessToken } = this.authService;

        if (accessToken) {
            // eslint-disable-next-line no-param-reassign
            request = request.clone({ setHeaders: { Authorization: `Bearer ${accessToken}` } });
        }

        return next.handle(request);
    }
}
