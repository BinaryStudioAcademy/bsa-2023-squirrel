import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from '@core/base/base.component';
import { AuthService } from '@core/services/auth.service';
import { EventService } from '@core/services/event.service';
import { NotificationService } from '@core/services/notification.service';
import { SpinnerService } from '@core/services/spinner.service';
import { UserService } from '@core/services/user.service';
import { faPen, faTrash } from '@fortawesome/free-solid-svg-icons';
import { ValidationsFn } from '@shared/helpers/validations-fn';
import { finalize, takeUntil } from 'rxjs';

import { UpdateUserNamesDto } from 'src/app/models/user/update-user-names-dto';
import { UpdateUserPasswordDto } from 'src/app/models/user/update-user-password-dto';
import { UserProfileDto } from 'src/app/models/user/user-profile-dto';

import { UserDto } from '../../../models/user/user-dto';

@Component({
    selector: 'app-user-profile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.sass'],
})
export class UserProfileComponent extends BaseComponent implements OnInit, OnDestroy {
    public squirrelNotification: boolean;

    public emailNotification: boolean;

    public penIcon = faPen;

    public trashIcon = faTrash;

    public currentUser: UserProfileDto;

    public userForUpdateService: UserDto | undefined;

    public userNamesForm: FormGroup = new FormGroup({});

    public passwordForm: FormGroup = new FormGroup({});

    private readonly maxFileLength = 5 * 1024 * 1024;

    private readonly allowedTypes = ['image/png', 'image/jpeg'];

    constructor(
        private fb: FormBuilder,
        private userService: UserService,
        private notificationService: NotificationService,
        private spinner: SpinnerService,
        private eventService: EventService,
        public authService: AuthService,
    ) {
        super();
    }

    public ngOnInit() {
        this.fetchCurrentUser();
    }

    private fetchCurrentUser() {
        this.spinner.show();

        this.authService
            .getUser()
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe((user) => {
                this.userForUpdateService = user;
            });

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
            currentPassword: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(25)]],
            newPassword: [
                '',
                [
                    Validators.required,
                    Validators.minLength(6),
                    Validators.maxLength(25),
                    ValidationsFn.wrongCharacters(),
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
                    this.updateServiceInfo(userData);
                    this.initUserNamesForm();
                },
                (error) => {
                    this.spinner.hide();
                    this.notificationService.error(error.message);
                },
            );
    }

    public updateServiceInfo(user: UpdateUserNamesDto) {
        const userUpdateService: UserDto = {
            userName: user.userName,
            firstName: user.firstName,
            lastName: user.lastName,
            avatarUrl: this.currentUser?.avatarUrl || '',
            id: this.userForUpdateService!.id,
            email: this.userForUpdateService!.email,
        };

        this.authService.setCurrentUser(userUpdateService);
        this.eventService.userChanged(userUpdateService);
    }

    public updateUserPassword() {
        if (
            !(this.passwordForm.valid && this.passwordForm.value.newPassword === this.passwordForm.value.repeatPassword)
        ) {
            this.notificationService.error('Update Password Form is invalid');

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

    public onFileChange(event: Event) {
        const inputElement = event.target as HTMLInputElement;

        if (!inputElement?.files?.length) {
            return;
        }
        const file = inputElement.files[0];

        if (!this.fileValidate(file)) {
            return;
        }

        this.spinner.show();
        this.userService
            .uploadAvatar(file)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: () => {
                    window.location.reload();
                },
                error: (error) => {
                    this.spinner.hide();
                    this.notificationService.error(error.message);
                },
            });
    }

    public fileValidate(file: File) {
        if (file.size > this.maxFileLength) {
            this.notificationService.error(`The file size should not exceed ${this.maxFileLength / (1024 * 1024)}MB`);

            return false;
        }

        if (!this.allowedTypes.includes(file.type)) {
            this.notificationService.error(`Invalid file type, need ${this.allowedTypes.join(', ')}`);

            return false;
        }

        return true;
    }

    public deleteAvatar() {
        this.spinner.show();

        this.userService.deleteAvatar()
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: () => {
                    window.location.reload();
                },
                error: (error) => {
                    this.spinner.hide();
                    this.notificationService.error(error.message);
                },
            });
    }
}
