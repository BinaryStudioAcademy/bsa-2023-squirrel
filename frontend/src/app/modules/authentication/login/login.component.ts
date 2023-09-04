import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BaseComponent } from '@core/base/base.component';
import { AuthService } from '@core/services/auth.service';
import { NotificationService } from '@core/services/notification.service';
import { SpinnerService } from '@core/services/spinner.service';
import { ValidationsFn } from '@shared/helpers/validations-fn';
import { takeUntil } from 'rxjs';

import { UserLoginDto } from 'src/app/models/user/user-login-dto';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.sass'],
})
export class LoginComponent extends BaseComponent implements OnInit {
    public loginForm: FormGroup = new FormGroup({});

    constructor(
        private fb: FormBuilder,
        private authService: AuthService,
        private notificationService: NotificationService,
        private router: Router,
        private spinner: SpinnerService,
    ) {
        super();
    }

    ngOnInit(): void {
        this.initializeForm();
    }

    public login() {
        this.spinner.show();
        const user: UserLoginDto = this.loginForm.value;

        this.authService
            .login(user)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: () => this.router.navigateByUrl('/projects'),
                error: (err) => {
                    this.spinner.hide();
                    this.notificationService.error(err.message);
                },
            });
    }

    private initializeForm() {
        this.loginForm = this.fb.group({
            email: [
                '',
                [Validators.required, Validators.minLength(3), Validators.maxLength(50), ValidationsFn.emailMatch()],
            ],
            password: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(25)]],
        });
    }
}
