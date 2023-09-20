import { Injectable } from '@angular/core';
import { finalize, Subject, takeUntil } from 'rxjs';

import { DatabaseItemsService } from './database-items.service';
import { HttpInternalService } from './http-internal.service';
import { SpinnerService } from './spinner.service';

@Injectable({
    providedIn: 'root',
})
export class LoadChangesService {
    protected unsubscribe$ = new Subject<void>();

    private readonly loadChangesRoutePrefix = '/api/changerecords';

    constructor(
        private httpClient: HttpInternalService,
        private databaseItemsService: DatabaseItemsService,
        private spinner: SpinnerService,
    ) {
        // eslint-disable-next-line no-empty-function
    }

    public loadChangesRequest(guid: string) {
        this.spinner.show();

        this.httpClient.postRequest<string>(`${this.loadChangesRoutePrefix}/${guid}`, null!)
            .pipe(
                takeUntil(this.unsubscribe$),
                finalize(() => this.spinner.hide()),
            )
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

        this.databaseItemsService.getAllItems(guid)
            .pipe(
                takeUntil(this.unsubscribe$),
            )
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
