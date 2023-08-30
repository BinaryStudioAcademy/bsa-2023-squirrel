import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

import { UserDto } from 'src/app/models/user/user-dto';

// tslint:disable:member-ordering
@Injectable({ providedIn: 'root' })
export class EventService {
    private onUserChanged = new Subject<UserDto>();

    public userChangedEvent$ = this.onUserChanged.asObservable();

    public userChanged(user: UserDto) {
        this.onUserChanged.next(user);
    }
}
