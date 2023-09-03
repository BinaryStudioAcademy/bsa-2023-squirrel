import { Injectable, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { CredentialResponse } from 'google-one-tap';
import { Observable, tap } from 'rxjs';

import { AccessTokenDto } from 'src/app/models/auth/access-token-dto';
import { GoogleAuthDto } from 'src/app/models/auth/google-auth-dto';
import { UserAuthDto } from 'src/app/models/auth/user-auth-dto';
import { UserLoginDto } from 'src/app/models/user/user-login-dto';
import { UserRegisterDto } from 'src/app/models/user/user-register-dto';

import { HttpInternalService } from './http-internal.service';
import { SpinnerService } from './spinner.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private readonly authRoutePrefix = '/api/auth';

    private readonly accessTokenKey = 'accessToken';

    private readonly refreshTokenKey = 'refreshToken';

    constructor(
        private httpService: HttpInternalService,
        private router: Router,
        private ngZone: NgZone,
        private spinner: SpinnerService, // eslint-disable-next-line no-empty-function
    ) {}

    public signOut = () => {
        localStorage.removeItem(this.accessTokenKey);
        localStorage.removeItem(this.refreshTokenKey);
        this.router.navigate(['/login']);
    };

    public signInViaGoogle(googleCredentialsToken: CredentialResponse) {
        const credentials: GoogleAuthDto = { idToken: googleCredentialsToken.credential };

        this.ngZone.run(() => this.spinner.show());

        return this.httpService
            .postRequest<UserAuthDto>(`${this.authRoutePrefix}/login/google`, credentials)
            .subscribe({
                next: (response: UserAuthDto) => {
                    this.saveTokens(response.token);
                    this.ngZone.run(() => {
                        this.spinner.hide();
                        this.router.navigateByUrl('/projects');
                    });
                },
                error: () => {
                    this.ngZone.run(() => this.spinner.hide());
                    this.signOut();
                },
            });
    }

    public register(userRegisterDto: UserRegisterDto): Observable<AccessTokenDto> {
        return this.httpService.postRequest<AccessTokenDto>(`${this.authRoutePrefix}/register`, userRegisterDto).pipe(
            tap((tokens) => {
                this.saveTokens(tokens);
            }),
        );
    }

    public login(userLoginDto: UserLoginDto): Observable<AccessTokenDto> {
        return this.httpService.postRequest<AccessTokenDto>(`${this.authRoutePrefix}/login`, userLoginDto).pipe(
            tap((tokens) => {
                this.saveTokens(tokens);
            }),
        );
    }

    public tokenExist() {
        return localStorage.getItem(this.accessTokenKey) && localStorage.getItem(this.refreshTokenKey);
    }

    public get accessToken(): string | null {
        const localJwt = localStorage.getItem(this.accessTokenKey);

        if (!localJwt) {
            return null;
        }

        return JSON.parse(localJwt);
    }

    private saveTokens(tokens: AccessTokenDto) {
        if (tokens.accessToken && tokens.refreshToken) {
            localStorage.setItem(this.accessTokenKey, JSON.stringify(tokens.accessToken));
            localStorage.setItem(this.refreshTokenKey, JSON.stringify(tokens.refreshToken));
        }
    }
}
