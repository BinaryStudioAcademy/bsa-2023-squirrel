import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { GoogleLoginProvider, SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';

import { environment } from '../../environments/environment';
import { ExternalAuthDto } from '../models/auth/external-auth-dto';
import { UserAuthDto } from '../models/auth/user-auth-dto';

@Injectable({
    providedIn: 'root',
})
export class ExternalAuthService {
    private readonly APIUrl = environment.coreUrl;

    // eslint-disable-next-line no-empty-function
    constructor(private http: HttpClient, private googleAuthService: SocialAuthService, private router: Router) {}

    public signInWithGoogle() {
        debugger;

        return this.googleAuthService.signIn(GoogleLoginProvider.PROVIDER_ID).then((res) => {
            const user: SocialUser = { ...res };

            debugger;
            this.validateExternalAuth(user);
        });
    }

    public signOutGoogle = () => {
        this.googleAuthService.signOut();
        this.router.navigate(['/login']);
    };

    private validateExternalAuth(user: SocialUser) {
        const auth: ExternalAuthDto = { idToken: user.idToken };

        return this.http.post<UserAuthDto>(`${this.APIUrl}/api/auth/login/${user.provider}`, auth).subscribe({
            next: (data: UserAuthDto) => {
                localStorage.setItem('accessToken', JSON.stringify(data.token.accessToken));
                localStorage.setItem('refreshToken', JSON.stringify(data.token.refreshToken));
                this.router.navigate(['/main']);
            },
            error: () => {
                this.signOutGoogle();
            },
        });
    }

    public validateExternalAuth2(credentials: string) {
        const auth: ExternalAuthDto = { idToken: credentials };

        debugger;

        return this.http.post<UserAuthDto>(`${this.APIUrl}/api/auth/login/google`, auth).subscribe({
            next: (data: UserAuthDto) => {
                localStorage.setItem('accessToken', JSON.stringify(data.token.accessToken));
                localStorage.setItem('refreshToken', JSON.stringify(data.token.refreshToken));
                this.router.navigate(['/main']);
            },
            error: () => {
                this.signOutGoogle();
            },
        });
    }
}
