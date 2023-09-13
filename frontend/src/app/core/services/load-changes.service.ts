import { Injectable } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';

import { HttpInternalService } from './http-internal.service';

@Injectable({
    providedIn: 'root',
})
export class LoadChangesService {
    protected unsubscribe$ = new Subject<void>();

    private readonly loadChangesRoutePrefix = '/api/changerecords';

    // eslint-disable-next-line no-empty-function
    constructor(private httpClient: HttpInternalService) { }

    public loadChangesRequest() {
        this.httpClient.postRequest<string>(`${this.loadChangesRoutePrefix}`, null!)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: (event) => {
                    // eslint-disable-next-line no-console
                    console.log(event);
                },
                error: (error) => {
                    // eslint-disable-next-line no-console
                    console.log(error);
                },
            });
    }
}
