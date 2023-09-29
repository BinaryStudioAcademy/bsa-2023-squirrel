import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

import { HttpInternalService } from './http-internal.service';

@Injectable({
    providedIn: 'root',
})
export class LoadChangesService {
    protected unsubscribe$ = new Subject<void>();

    private readonly loadChangesRoutePrefix = '/api/changerecords';

    constructor(private httpClient: HttpInternalService) { }

    public loadChangesRequest(guid: string): Observable<string> {
        return this.httpClient.postRequest<string>(`${this.loadChangesRoutePrefix}/${guid}`, null!);
    }
}
