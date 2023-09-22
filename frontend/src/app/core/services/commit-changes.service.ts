import { Injectable } from '@angular/core';
import { HttpInternalService } from '@core/services/http-internal.service';
import { BehaviorSubject } from 'rxjs';

import { DatabaseItemContentCompare } from '../../models/database-items/database-item-content-compare';

@Injectable({
    providedIn: 'root',
})
export class CommitChangesService {
    private readonly commitChangesRoute = '/api/commitchanges';

    private contentChangesSubject = new BehaviorSubject<DatabaseItemContentCompare[]>([]);

    contentChanges$ = this.contentChangesSubject.asObservable();

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) {}

    public getContentDiffs(commitId: number, tempBlobId: string): void {
        this.httpService
            .getRequest<DatabaseItemContentCompare[]>(`${this.commitChangesRoute}/${commitId}/${tempBlobId}`)
            .subscribe(
                (contentChanges) => {
                    this.contentChangesSubject.next(contentChanges);
                },
                (error) => {
                    console.log(error);
                },
            );
    }
}