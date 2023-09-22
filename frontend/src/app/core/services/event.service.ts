import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

import { DatabaseItem } from 'src/app/models/database-items/database-item';
import { UserDto } from 'src/app/models/user/user-dto';

@Injectable({
    providedIn: 'root',
})
export class EventService {
    private onUserChanged = new Subject<UserDto | undefined>();

    public userChangedEvent$ = this.onUserChanged.asObservable();

    public userChanged(user: UserDto | undefined) {
        this.onUserChanged.next(user);
    }

    private onChangesLoaded = new Subject<DatabaseItem[] | undefined>();

    public changesLoadedEvent$ = this.onChangesLoaded.asObservable();

    public changesLoaded(item: DatabaseItem[] | undefined) {
        this.onChangesLoaded.next(item);
    }

    private onChangesSaved = new Subject<string | undefined>();

    public changesSavedEvent$ = this.onChangesSaved.asObservable();

    public changesSaved(guid: string | undefined) {
        this.onChangesSaved.next(guid);
    }
}
