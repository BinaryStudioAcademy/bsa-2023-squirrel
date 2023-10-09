import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '@core/services/auth.service';
import { Observable } from 'rxjs';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService) { }

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        const accessToken = this.authService.getAccessToken();

        if (accessToken) {
            const clonedRequest = request.clone({ setHeaders: { Authorization: `Bearer ${accessToken}` } });

            return next.handle(clonedRequest);
        }

        return next.handle(request);
    }
}
