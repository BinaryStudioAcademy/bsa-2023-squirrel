import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { DatabaseItem } from 'src/app/models/database-items/database-item';
import { UserDto } from 'src/app/models/user/user-dto';

@Injectable({
    providedIn: 'root',
})
export class EventService {
    private onChangesSaved = new BehaviorSubject<string | undefined>(undefined);

    private onUserChanged = new BehaviorSubject<UserDto | undefined>(undefined);

    private onChangesLoaded = new BehaviorSubject<DatabaseItem[] | undefined>(undefined);

    get userChangedEvent$() {
        return this.onUserChanged.asObservable();
    }

    get changesLoadedEvent$() {
        return this.onChangesLoaded.asObservable();
    }

    get changesSavedEvent$() {
        return this.onChangesSaved.asObservable();
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
}
