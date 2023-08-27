import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BaseComponent } from '@core/base/base.component';
import { AuthService } from '@core/services/auth.service';
import { NotificationService } from '@core/services/notification.service';
import { environment } from '@env/environment';
import { ValidationsFn } from '@shared/helpers/validations-fn';
import { takeUntil } from 'rxjs';

import { UserLoginDto } from 'src/app/models/user/user-login-dto';

declare const google: any;

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.sass'],
})
export class LoginComponent extends BaseComponent implements OnInit {
    public loginForm: FormGroup = new FormGroup({});

    // eslint-disable-next-line no-empty-function
    constructor(
        private fb: FormBuilder,
        private externalAuthService: AuthService,
        private notificationService: NotificationService,
        private router: Router,
    ) {
        super();
    }

    ngOnInit(): void {
        this.initializeForm();
        this.initializeGoogleSignIn();
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

    private initializeGoogleSignIn() {
        google.accounts.id.initialize({
            client_id: environment.googleClientId,
            context: 'signin',
            ux_mode: 'popup',
            callback: this.handleCredentialResponse.bind(this),
        });

        google.accounts.id.renderButton(
            document.getElementById('signInGoogle'),
            { theme: 'outline', size: 'large', width: '360px', text: 'signin_with', locale: 'en_US' }, // customization attributes
        );

        // also display the One Tap dialog
        google.accounts.id.prompt();
    }

    private handleCredentialResponse(response: any) {
        // eslint-disable-next-line no-console
        console.log(`Encoded JWT ID token: ${response.credential}`);
        this.externalAuthService.validateGoogleAuth(response.credential);
    }

    public login() {
        const user: UserLoginDto = this.loginForm.value;

        this.externalAuthService.login(user)
            .pipe(takeUntil(this.unsubscribe$)).subscribe({
                next: () => this.router.navigateByUrl('/main'),
                error: err => this.notificationService.error(err.message),
            });
    }
}
