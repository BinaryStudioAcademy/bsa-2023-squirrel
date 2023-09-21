import { Injectable } from '@angular/core';
import { HttpInternalService } from '@core/services/http-internal.service';
import { Observable } from 'rxjs';

import { DatabaseItemContentCompare } from '../../models/database-items/database-item-content-compare';

@Injectable({
    providedIn: 'root',
})
export class CommitChangesService {
    private readonly commitChangesRoute = '/api/commitchanges';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) {}

    public getContentDiffs(commitId: number, tempBlobId: string): Observable<DatabaseItemContentCompare[]> {
        return this.httpService.getRequest<DatabaseItemContentCompare[]>(`${this.commitChangesRoute}/${commitId}/${tempBlobId}`);
    }
}
