import { Injectable } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';

import { DatabaseItem } from 'src/app/models/database-items/database-item';

import { HttpInternalService } from './http-internal.service';

@Injectable({
    providedIn: 'root',
})
export class LoadChangesService {
    protected unsubscribe$ = new Subject<void>();

    private readonly loadChangesRoutePrefix = '/api/changerecords';

    private readonly databaseItemsRoutePrefix = '/api/databaseitems';

    // eslint-disable-next-line no-empty-function
    constructor(private httpClient: HttpInternalService) { }

    public loadChangesRequest(guid: string) {
        // this.httpClient.postRequest<string>(`${this.loadChangesRoutePrefix}/${guid}`, null!)
        //     .pipe(takeUntil(this.unsubscribe$))
        //     .subscribe({
        //         next: (event) => {
        //             // eslint-disable-next-line no-console
        //             console.log(event);
        //         },
        //         error: (error) => {
        //             // eslint-disable-next-line no-console
        //             console.log(error);
        //         },
        //     });

        this.httpClient.getRequest<DatabaseItem[]>(`${this.databaseItemsRoutePrefix}/${guid}`)
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
