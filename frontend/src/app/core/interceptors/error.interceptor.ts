import { HttpErrorResponse, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
    providedIn: 'root',
})
export class ErrorInterceptor implements HttpInterceptor {
    handleError(error: HttpErrorResponse) {
        return throwError(() => new Error(error.message ?? 'Something went wrong'));
    }

    intercept(req: HttpRequest<unknown>, next: HttpHandler) {
        return next.handle(req).pipe(catchError(this.handleError));
    }
}
