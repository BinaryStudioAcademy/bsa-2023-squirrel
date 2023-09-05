import { Component, OnDestroy, OnInit } from '@angular/core';
import { BaseComponent } from '@core/base/base.component';
import { AuthService } from '@core/services/auth.service';
import { EventService } from '@core/services/event.service';
import { takeUntil } from 'rxjs';

import { User } from 'src/app/models/user/user';

@Component({
    selector: 'app-profile-menu',
    templateUrl: './profile-menu.component.html',
    styleUrls: ['./profile-menu.component.sass'],
})
export class ProfileMenuComponent extends BaseComponent implements OnInit, OnDestroy {
    public currentUser: User | undefined;

    constructor(private authService: AuthService, private eventService: EventService) {
        super();
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
