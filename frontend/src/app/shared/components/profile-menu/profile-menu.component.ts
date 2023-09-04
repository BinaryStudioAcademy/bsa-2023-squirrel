import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '@core/services/auth.service';
import { EventService } from '@core/services/event.service';
import { Subject, takeUntil } from 'rxjs';

import { UserDto } from 'src/app/models/user/user-dto';

@Component({
    selector: 'app-profile-menu',
    templateUrl: './profile-menu.component.html',
    styleUrls: ['./profile-menu.component.sass'],
})
export class ProfileMenuComponent implements OnInit, OnDestroy {
    private unsubscribe$ = new Subject<void>();

    public currentUser: UserDto | undefined;

    // eslint-disable-next-line no-empty-function
    constructor(private authService: AuthService, private eventService: EventService) {}

    ngOnDestroy(): void {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();
    }

    signOut() {
        this.authService.signOut();
    }

    ngOnInit(): void {
        this.authService
            .getUser()
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe((user) => {
                this.currentUser = user;
            });
        this.eventService.userChangedEvent$.pipe(takeUntil(this.unsubscribe$)).subscribe((user) => {
            this.currentUser = user;
        });
    }
}
