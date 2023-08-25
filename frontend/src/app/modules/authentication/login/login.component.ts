import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { environment } from '@env/environment';
import { ValidationsFn } from '@shared/helpers/validations-fn';

import { ExternalAuthService } from 'src/app/services/external-auth.service';

import { UserLoginDto } from '../../../models/auth/user-login-dto';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.sass'],
})
export class LoginComponent implements OnInit {
    public loginForm: FormGroup = new FormGroup({});

    // eslint-disable-next-line no-empty-function
    constructor(private fb: FormBuilder, private externalAuthService: ExternalAuthService) {}

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
            callback: this.handleCredentialResponse.bind(this),
        });

        google.accounts.id.renderButton(
            this.elRef.nativeElement.querySelector('#buttonDiv'),
            { theme: 'outline', size: 'large' }, // customization attributes
        );

        google.accounts.id.prompt(); // also display the One Tap dialog
    }

    private handleCredentialResponse(response: any) {
        console.log(`Encoded JWT ID token: ${response.credential}`);
    }

    public login() {
        const user: UserLoginDto = this.loginForm.value;

        // temporary solution
        // eslint-disable-next-line no-console
        console.log(user);
    }
}
