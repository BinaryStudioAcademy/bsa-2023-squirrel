/* eslint-disable no-empty-function */
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BaseComponent } from '@core/base/base.component';
import { AuthService } from '@core/services/auth.service';
import { NotificationService } from '@core/services/notification.service';
import { SpinnerService } from '@core/services/spinner.service';
import { ValidationsFn } from '@shared/helpers/validations-fn';
import { takeUntil } from 'rxjs';

import { ErrorDetailsDto } from 'src/app/models/error/error-details-dto';

import { UserRegisterDto } from '../../../models/auth/user-register-dto';

@Component({
    selector: 'app-registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.sass'],
})
export class RegistrationComponent extends BaseComponent implements OnInit {
    public registerForm: FormGroup = new FormGroup({});

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private spinner: SpinnerService,
        private authService: AuthService,
        private notificationService: NotificationService,
    ) {
        super();
    }

    public ngOnInit() {
        this.initializeForm();
    }

    private initializeForm() {
        this.registerForm = this.fb.group({
            username: [
                '',
                [Validators.required, Validators.minLength(2), Validators.maxLength(25), ValidationsFn.userNameMatch()],
            ],
            email: [
                '',
                [Validators.required, Validators.minLength(3), Validators.maxLength(50), ValidationsFn.emailMatch()],
            ],
            firstName: [
                '',
                [Validators.required, Validators.minLength(2), Validators.maxLength(25), ValidationsFn.nameMatch()],
            ],
            lastName: [
                '',
                [Validators.required, Validators.minLength(2), Validators.maxLength(25), ValidationsFn.nameMatch()],
            ],
            password: [
                '',
                [
                    Validators.required,
                    Validators.minLength(2),
                    Validators.maxLength(25),
                    ValidationsFn.wrongCharacters(),
                    ValidationsFn.lowerExist(),
                    ValidationsFn.upperExist(),
                ],
            ],
            confirmPassword: ['', [Validators.required, ValidationsFn.matchValues('password')]],
        });
        this.registerForm.controls['password'].valueChanges.subscribe({
            next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity(),
        });
    }

    public register() {
        this.spinner.show();

        const userRegistrationData: UserRegisterDto = {
            username: this.registerForm.value.username,
            email: this.registerForm.value.email,
            firstName: this.registerForm.value.firstName,
            lastName: this.registerForm.value.lastName,
            password: this.registerForm.value.password,
        };

        this.authService
            .register(userRegistrationData)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                () => {
                    this.router.navigateByUrl('/main');
                },
                (err: ErrorDetailsDto) => {
                    this.spinner.hide();
                    this.notificationService.error(err.message);
                },
            );
    }
}
