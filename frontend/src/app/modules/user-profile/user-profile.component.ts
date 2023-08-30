import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BaseComponent } from '@core/base/base.component';
import { AuthService } from '@core/services/auth.service';
import { NotificationService } from '@core/services/notification.service';
import { UserService } from '@core/services/user.service';
import { takeUntil } from 'rxjs/operators';

import { UserDto } from 'src/app/models/user/user-dto';

@Component({
    selector: 'app-user-profile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.sass'],
})
export class UserProfileComponent extends BaseComponent implements OnInit {
    public user = {} as UserDto;

    public editProfileForm: FormGroup = new FormGroup({});

    public loading = false;

    constructor(
        private location: Location,
        private userService: UserService,
        private notificationService: NotificationService,
        private authService: AuthService, // eslint-disable-next-line no-empty-function
    ) {
        super();
    }

    public ngOnInit() {
        const userId = this.authService.getUser().id;
        const userSubscription = this.userService.getUserById(userId);

        userSubscription.pipe(takeUntil(this.unsubscribe$)).subscribe(
            (user) => {
                this.user = user;
            },
            (error) => this.notificationService.error(error.message),
        );
    }

    public saveNewInfo() {
        const userSubscription = this.userService.updateUser(this.user);

        this.loading = true;

        userSubscription.pipe(takeUntil(this.unsubscribe$)).subscribe(
            (user) => {
                this.user = user;
                this.notificationService.info('Successfully updated');
                this.loading = false;
            },
            (error) => this.notificationService.error(error.message),
        );
    }

    public goBack = () => this.location.back();
}
