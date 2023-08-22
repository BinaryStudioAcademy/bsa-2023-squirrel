import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NavigationCancel, NavigationEnd, NavigationError, NavigationStart, Router } from '@angular/router';
import { GoogleLoginProvider, SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { SpinnerService } from '@core/services/spinner.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.sass'],
})
export class AppComponent implements OnInit {
    loginForm!: FormGroup;

    socialUser!: SocialUser;

    isLoggedIn?: boolean;

    private accessToken = '';

    constructor(
        private router: Router,
        private spinner: SpinnerService,
        private formBuilder: FormBuilder,
        private socialAuthService: SocialAuthService,
    ) {
        this.listenRouter();
    }

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            email: ['', Validators.required],
            password: ['', Validators.required],
        });
        this.socialAuthService.authState.subscribe((user) => {
            this.socialUser = user;
            this.isLoggedIn = user != null;
            console.log(this.socialUser);
        });
    }

    loginWithGoogle(): void {
        this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID).then(() => this.router.navigate(['main']));
    }

    logOut(): void {
        this.socialAuthService.signOut();
    }

    getAccessToken(): void {
        this.socialAuthService
            .getAccessToken(GoogleLoginProvider.PROVIDER_ID)
            .then((accessToken) => (this.accessToken = accessToken));
    }

    private listenRouter() {
        this.router.events.subscribe((event) => {
            if (event instanceof NavigationStart) {
                this.spinner.show();
            }
            if (
                event instanceof NavigationEnd ||
                event instanceof NavigationCancel ||
                event instanceof NavigationError
            ) {
                this.spinner.hide();
            }
        });
    }
}
