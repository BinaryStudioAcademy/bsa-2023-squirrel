import {
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        const localJwt = localStorage.getItem('accessToken');

        if (!localJwt) {
            return next.handle(request);
        }

        const accessToken = JSON.parse(localJwt);

        if (accessToken) {
            // eslint-disable-next-line no-param-reassign
            request = request.clone({ setHeaders: { Authorization: `Bearer ${accessToken}` } });
        }

        return next.handle(request);
    }
}
