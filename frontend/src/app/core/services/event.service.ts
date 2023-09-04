import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

import { User } from 'src/app/models/user/user';

@Injectable({
    providedIn: 'root',
})
export class EventService {
    private onUserChanged = new Subject<User | undefined>();

    public userChangedEvent$ = this.onUserChanged.asObservable();

    public userChanged(user: User | undefined) {
        this.onUserChanged.next(user);
    }
}
