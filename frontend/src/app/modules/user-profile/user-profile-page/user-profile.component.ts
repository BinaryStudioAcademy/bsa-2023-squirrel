import { Location } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from '@core/base/base.component';
import { AuthService } from '@core/services/auth.service';
import { NotificationService } from '@core/services/notification.service';
import { SpinnerService } from '@core/services/spinner.service';
import { UserService } from '@core/services/user.service';
import { faEye, faEyeSlash, faPen } from '@fortawesome/free-solid-svg-icons';
import { ValidationsFn } from '@shared/helpers/validations-fn';
import { finalize, takeUntil } from 'rxjs';

import { UpdateUserNamesDto } from 'src/app/models/user/update-userNames.dto';
import { UpdateUserNotificationsDto } from 'src/app/models/user/update-userNotifications.dto';
import { UpdateUserPasswordDto } from 'src/app/models/user/update-userPassword.dto';
import { UserDto } from 'src/app/models/user/user-dto';

@Component({
    selector: 'app-user-profile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.sass'],
})
export class UserProfileComponent extends BaseComponent implements OnInit, OnDestroy {
    public squirrelNotification: boolean;

    public emailNotification: boolean;

    public openEyeIcon = faEye;

    public penIcon = faPen;

    public closeEyeIcon = faEyeSlash;

    public user: UserDto;

    public userNamesForm: FormGroup = new FormGroup({});

    public passwordForm: FormGroup = new FormGroup({});

    public notificationsForm: FormGroup = new FormGroup({});

    constructor(
        private fb: FormBuilder,
        private location: Location,
        private userService: UserService,
        private notificationService: NotificationService,
        private authService: AuthService,
        private spinner: SpinnerService,
    ) {
        super();
    }

    public ngOnInit() {
        const loggedUserId = this.authService.getCurrentUser()?.id;

        if (!loggedUserId) {
            this.notificationService.error('Current User is null');

            return;
        }

        this.fetchUserById(loggedUserId);
    }

    private fetchUserById(userId: number) {
        this.spinner.show();

        this.userService
            .getUserById(userId)
            .pipe(
                takeUntil(this.unsubscribe$),
                finalize(() => this.spinner.hide()),
            )
            .subscribe(
                (userFromDb) => {
                    this.user = userFromDb;
                    this.initializeForms();
                },
                (error) => {
                    this.notificationService.error(`Failed to fetch user by ID: ${error.message}`);
                },
            );
    }

    public override ngOnDestroy() {
        super.ngOnDestroy();
    }

    private initializeForms() {
        this.initUserNamesForm();
        this.initChangePasswordForm();
        this.initNotificationsValue();
    }

    private initUserNamesForm() {
        this.userNamesForm = this.fb.group({
            userName: [
                this.user.userName,
                [Validators.required, Validators.minLength(2), Validators.maxLength(25), ValidationsFn.userNameMatch()],
            ],
            firstName: [
                this.user.firstName,
                [Validators.required, Validators.minLength(2), Validators.maxLength(25), ValidationsFn.nameMatch()],
            ],
            lastName: [
                this.user.lastName,
                [Validators.required, Validators.minLength(2), Validators.maxLength(25), ValidationsFn.nameMatch()],
            ],
        });
    }

    private initChangePasswordForm() {
        this.passwordForm = this.fb.group({
            currentPassword: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(25)]],
            newPassword: [
                '',
                [
                    Validators.required,
                    Validators.minLength(2),
                    Validators.maxLength(25),
                    ValidationsFn.lowerExist(),
                    ValidationsFn.upperExist(),
                ],
            ],
            repeatPassword: [
                '',
                [
                    Validators.required,
                    Validators.minLength(2),
                    Validators.maxLength(25),
                    ValidationsFn.lowerExist(),
                    ValidationsFn.upperExist(),
                ],
            ],
        });

        this.passwordForm.controls['newPassword'].valueChanges.subscribe(() => {
            this.passwordForm.controls['repeatPassword'].updateValueAndValidity();
        });
    }

    private initNotificationsValue() {
        this.squirrelNotification = this.user.squirrelNotification;
        this.emailNotification = this.user.emailNotification;
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
            id: this.user.id,
        };

        const userSubscription = this.userService.updateUserNames(userData);

        userSubscription.pipe(takeUntil(this.unsubscribe$)).subscribe(
            (user) => {
                this.user = user;
                this.authService.setCurrentUser(user);
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
            id: this.user.id,
        };

        const userSubscription = this.userService.updateUserPassword(userData);

        userSubscription.pipe(takeUntil(this.unsubscribe$)).subscribe(
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
            id: this.user.id,
        };

        const userSubscription = this.userService.updateUserNotifications(userData);

        userSubscription.pipe(takeUntil(this.unsubscribe$)).subscribe(
            (user) => {
                this.user = user;
                this.authService.setCurrentUser(user);
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
