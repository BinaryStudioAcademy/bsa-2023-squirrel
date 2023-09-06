import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from '@core/base/base.component';
import { NotificationService } from '@core/services/notification.service';
import { SpinnerService } from '@core/services/spinner.service';
import { UserService } from '@core/services/user.service';
import { faPen } from '@fortawesome/free-solid-svg-icons';
import { ValidationsFn } from '@shared/helpers/validations-fn';
import { finalize, takeUntil } from 'rxjs';

import { UpdateUserNamesDto } from 'src/app/models/user/update-user-names-dto';
import { UpdateUserNotificationsDto } from 'src/app/models/user/update-user-notifications-dto';
import { UpdateUserPasswordDto } from 'src/app/models/user/update-user-password-dto';
import { UserProfileDto } from 'src/app/models/user/user-profile-dto';

@Component({
    selector: 'app-user-profile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.sass'],
})
export class UserProfileComponent extends BaseComponent implements OnInit, OnDestroy {
    public squirrelNotification: boolean;

    public emailNotification: boolean;

    public penIcon = faPen;

    public currentUser: UserProfileDto;

    public userNamesForm: FormGroup = new FormGroup({});

    public passwordForm: FormGroup = new FormGroup({});

    constructor(
        private fb: FormBuilder,
        private userService: UserService,
        private notificationService: NotificationService,
        private spinner: SpinnerService,
    ) {
        super();
    }

    public ngOnInit() {
        this.fetchCurrentUser();
    }

    private fetchCurrentUser() {
        this.spinner.show();

        this.userService
            .getUserProfile()
            .pipe(
                takeUntil(this.unsubscribe$),
                finalize(() => this.spinner.hide()),
            )
            .subscribe(
                (userProfile) => {
                    this.currentUser = userProfile;
                    this.initializeForms();
                },
                () => {
                    this.notificationService.error('Failed to fetch current user');
                },
            );
    }

    private initializeForms() {
        this.initUserNamesForm();
        this.initChangePasswordForm();
        this.initNotificationsValue();
    }

    private initUserNamesForm() {
        this.userNamesForm = this.fb.group({
            userName: [
                this.currentUser.userName,
                [Validators.required, Validators.minLength(2), Validators.maxLength(25), ValidationsFn.userNameMatch()],
            ],
            firstName: [
                this.currentUser.firstName,
                [Validators.required, Validators.minLength(2), Validators.maxLength(25), ValidationsFn.nameMatch()],
            ],
            lastName: [
                this.currentUser.lastName,
                [Validators.required, Validators.minLength(2), Validators.maxLength(25), ValidationsFn.nameMatch()],
            ],
        });
    }

    private initChangePasswordForm() {
        this.passwordForm = this.fb.group({
            currentPassword: [
                '',
                [
                    Validators.required,
                    Validators.minLength(6),
                    Validators.maxLength(25),
                    ValidationsFn.lowerExist(),
                    ValidationsFn.upperExist(),
                ],
            ],
            newPassword: [
                '',
                [
                    Validators.required,
                    Validators.minLength(6),
                    Validators.maxLength(25),
                    ValidationsFn.lowerExist(),
                    ValidationsFn.upperExist(),
                ],
            ],
            repeatPassword: ['', [Validators.required, ValidationsFn.matchValues('newPassword')]],
        });

        this.passwordForm.controls['newPassword'].valueChanges.subscribe(() => {
            this.passwordForm.controls['repeatPassword'].updateValueAndValidity();
        });
    }

    private initNotificationsValue() {
        this.squirrelNotification = this.currentUser.squirrelNotification;
        this.emailNotification = this.currentUser.emailNotification;
    }

    public updateUserNames() {
        if (!this.userNamesForm.valid) {
            this.notificationService.error('Update Names Form is invalid');

            return;
        }

        this.spinner.show();
        const userData: UpdateUserNamesDto = {
            userName: this.userNamesForm.value.userName,
            firstName: this.userNamesForm.value.firstName,
            lastName: this.userNamesForm.value.lastName,
        };

        this.userService
            .updateUserNames(userData)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                (user) => {
                    this.currentUser = user;
                    this.spinner.hide();
                    this.notificationService.info('Names successfully updated');
                    this.initUserNamesForm();
                },
                (error) => {
                    this.spinner.hide();
                    this.notificationService.error(error.message);
                },
            );
    }

    public updateUserPassword() {
        if (
            !(this.passwordForm.valid && this.passwordForm.value.newPassword === this.passwordForm.value.repeatPassword)
        ) {
            this.notificationService.error('Update Names Form is invalid');

            return;
        }

        this.spinner.show();
        const userData: UpdateUserPasswordDto = {
            currentPassword: this.passwordForm.value.currentPassword,
            newPassword: this.passwordForm.value.newPassword,
        };

        this.userService
            .updateUserPassword(userData)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                () => {
                    this.spinner.hide();
                    this.notificationService.info('Password successfully updated');
                    this.initChangePasswordForm();
                },
                (error) => {
                    this.spinner.hide();
                    this.notificationService.error(error.message);
                },
            );
    }

    public updateUserNotifications() {
        this.spinner.show();
        const userData: UpdateUserNotificationsDto = {
            squirrelNotification: this.squirrelNotification,
            emailNotification: this.emailNotification,
        };

        this.userService
            .updateUserNotifications(userData)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                (user) => {
                    this.currentUser = user;
                    this.spinner.hide();
                    this.notificationService.info('Notifications successfully updated');
                    this.initNotificationsValue();
                },
                (error) => {
                    this.spinner.hide();
                    this.notificationService.error(error.message);
                },
            );
    }
}
