import { Injectable, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { CredentialResponse } from 'google-one-tap';
import { Observable, tap } from 'rxjs';

import { AccessTokenDto } from 'src/app/models/auth/access-token-dto';
import { GoogleAuthDto } from 'src/app/models/auth/google-auth-dto';
import { UserAuthDto } from 'src/app/models/auth/user-auth-dto';
import { UserDto } from 'src/app/models/user/user-dto';
import { UserRegisterDto } from 'src/app/models/user/user-register-dto';

import { UserLoginDto } from '../../models/user/user-login-dto';

import { HttpInternalService } from './http-internal.service';
import { SpinnerService } from './spinner.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private readonly authRoutePrefix = '/api/auth';

    private readonly accessTokenKey = 'accessToken';

    private readonly refreshTokenKey = 'refreshToken';

    private readonly currentUserKey = 'currentUser';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService,
        private router: Router,
        private ngZone: NgZone,
        private spinner: SpinnerService,) {}

    public signOut = () => {
        localStorage.removeItem(this.accessTokenKey);
        localStorage.removeItem(this.refreshTokenKey);
        localStorage.removeItem(this.currentUserKey);
        this.router.navigate(['/login']);
    };

    public signInViaGoogle(googleCredentialsToken: CredentialResponse) {
        const credentials: GoogleAuthDto = { idToken: googleCredentialsToken.credential };

        this.ngZone.run(() => this.spinner.show());

        return this.httpService.postRequest<UserAuthDto>(`${this.authRoutePrefix}/login/google`, credentials).subscribe({
            next: (data: UserAuthDto) => {
                this.saveTokens(data.token);
                this.setCurrentUser(data.user);
                this.ngZone.run(() => {
                    this.spinner.hide();
                    this.router.navigateByUrl('/main');
                });
            },
            error: () => {
                this.ngZone.run(() => this.spinner.hide());
                this.signOut();
            },
        });
    }

    public register(userRegisterDto: UserRegisterDto): Observable<UserAuthDto> {
        return this.httpService.postRequest<UserAuthDto>(`${this.authRoutePrefix}/register`, userRegisterDto).pipe(
            tap((data: UserAuthDto) => {
                this.saveTokens(data.token);
                this.setCurrentUser(data.user);
            }),
        );
    }

    public login(userLoginDto: UserLoginDto): Observable<UserAuthDto> {
        return this.httpService.postRequest<UserAuthDto>(`${this.authRoutePrefix}/login`, userLoginDto).pipe(
            tap((data: UserAuthDto) => {
                this.saveTokens(data.token);
                this.setCurrentUser(data.user);
            }),
        );
    }

    public tokenExist() {
        return localStorage.getItem('accessToken') && localStorage.getItem('refreshToken');
    }

    public get accessToken(): string | null {
        const localJwt = localStorage.getItem('accessToken');

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

    public setCurrentUser(user: UserDto) {
        localStorage.setItem(this.currentUserKey, JSON.stringify(user));
    }

    public getCurrentUser(): UserDto | null {
        const currentUser = localStorage.getItem(this.currentUserKey);

        if (!currentUser) {
            return null;
        }

        return JSON.parse(currentUser);
    }
}
