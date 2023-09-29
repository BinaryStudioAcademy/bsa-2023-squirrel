import { Injectable } from '@angular/core';
import { HttpInternalService } from '@core/services/http-internal.service';
import { NotificationService } from '@core/services/notification.service';
import { BehaviorSubject } from 'rxjs';

import { DatabaseItemContentCompare } from '../../models/database-items/database-item-content-compare';

@Injectable({
    providedIn: 'root',
})
export class CommitChangesService {
    private readonly commitChangesRoute = '/api/commitchanges';

    private contentChangesSubject = new BehaviorSubject<DatabaseItemContentCompare[]>([]);

    public contentChanges$ = this.contentChangesSubject.asObservable();

    constructor(
        private httpService: HttpInternalService,
        private notificationService: NotificationService,
        // eslint-disable-next-line no-empty-function
    ) {}

    public getContentDiffs(commitId: number, tempBlobId: string): void {
        this.httpService
            .getRequest<DatabaseItemContentCompare[]>(`${this.commitChangesRoute}/${commitId}/${tempBlobId}`)
            .subscribe(
                (contentChanges) => {
                    this.contentChangesSubject.next(contentChanges);
                },
                (error) => {
                    this.notificationService.error(error.message);
                },
            );
    }

    public clearContentChanges() {
        this.contentChangesSubject.next([]);
    }
}
