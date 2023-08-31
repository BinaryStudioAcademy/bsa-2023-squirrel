import { Location } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BaseComponent } from '@core/base/base.component';
import { AuthService } from '@core/services/auth.service';
import { NotificationService } from '@core/services/notification.service';
import { SpinnerService } from '@core/services/spinner.service';
import { UserService } from '@core/services/user.service';
import { takeUntil } from 'rxjs';

import { UpdateUserNamesDto } from 'src/app/models/user/update-userNames.dto';
import { UserDto } from 'src/app/models/user/user-dto';

@Component({
    selector: 'app-user-profile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.sass'],
})
export class UserProfileComponent extends BaseComponent implements OnInit, OnDestroy {
    public user: UserDto;

    public editProfileForm: FormGroup = new FormGroup({});

    constructor(
        private spinner: SpinnerService,
        private fb: FormBuilder,
        private location: Location,
        private userService: UserService,
        private notificationService: NotificationService,
        private authService: AuthService, // eslint-disable-next-line no-empty-function
    ) {
        super();
    }

    public ngOnInit() {
        const loggedUserId = this.authService.getCurrentUser()?.id;

        debugger;
        if (loggedUserId !== null && loggedUserId !== undefined) {
            this.userService
                .getUserById(loggedUserId)
                .pipe(takeUntil(this.unsubscribe$))
                .subscribe(
                    (userFromDb) => {
                        this.user = userFromDb;
                        this.initializeForm();
                    },
                    (error) => {
                        console.error('Error getting user by ID:', error);
                        this.notificationService.error('Failed to fetch user by ID');
                    },
                );
        } else {
            this.notificationService.error('Current User is null');
        }
    }

    public override ngOnDestroy() {
        super.ngOnDestroy();
    }

    private initializeForm() {
        this.editProfileForm = this.fb.group({
            username: [this.user.userName],
            firstName: [this.user.firstName],
            lastName: [this.user.lastName],
        });
    }

    public saveNewInfo() {
        this.spinner.show();

        const userData: UpdateUserNamesDto = {
            userName: this.editProfileForm.value.username,
            firstName: this.editProfileForm.value.firstName,
            lastName: this.editProfileForm.value.lastName,
            id: this.user.id,
        };

        const userSubscription = this.userService.updateUserNames(userData);

        userSubscription.pipe(takeUntil(this.unsubscribe$)).subscribe(
            (user) => {
                this.user = user;
                this.authService.setCurrentUser(user);
                this.spinner.hide();
                this.notificationService.info('Successfully updated');
            },
            (error) => {
                this.spinner.hide();
                this.notificationService.error(error.message);
            },
        );
    }

    getUserInitials(): string {
        if (this.user.firstName && this.user.lastName) {
            return `${this.user.firstName.charAt(0)}${this.user.lastName.charAt(0)}`;
        }

        return this.user.userName.substr(0, 2);
    }

    public goBack = () => this.location.back();
}
