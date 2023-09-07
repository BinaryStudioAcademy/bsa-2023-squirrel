import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

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
}
