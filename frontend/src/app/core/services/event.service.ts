import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

import { DatabaseItem } from 'src/app/models/database-items/database-item';
import { UserDto } from 'src/app/models/user/user-dto';

@Injectable({
    providedIn: 'root',
})
export class EventService {
    public userChangedEvent$: Observable<UserDto | undefined>;

    public changesLoadedEvent$: Observable<DatabaseItem[] | undefined>;

    public changesSavedEvent$: Observable<string | undefined>;

    public branchChangedEvent$: Observable<number | undefined>;

    private onChangesSaved = new Subject<string | undefined>();

    private onUserChanged = new Subject<UserDto | undefined>();

    private onChangesLoaded = new Subject<DatabaseItem[] | undefined>();

    private onBranchChanged = new Subject<number | undefined>();

    constructor() {
        this.userChangedEvent$ = this.onUserChanged.asObservable();
        this.changesLoadedEvent$ = this.onChangesLoaded.asObservable();
        this.changesSavedEvent$ = this.onChangesSaved.asObservable();
        this.branchChangedEvent$ = this.onBranchChanged.asObservable();
    }

    public changesLoaded(item: DatabaseItem[] | undefined) {
        this.onChangesLoaded.next(item);
    }

    public userChanged(user: UserDto | undefined) {
        this.onUserChanged.next(user);
    }

    public changesSaved(guid: string | undefined) {
        this.onChangesSaved.next(guid);
    }

    public branchChanged(branchId: number | undefined) {
        this.onBranchChanged.next(branchId);
    }
}
