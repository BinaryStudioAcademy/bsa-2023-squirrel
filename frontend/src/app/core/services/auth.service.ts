import { Injectable, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { CredentialResponse } from 'google-one-tap';
import { Observable, tap } from 'rxjs';

import { AccessTokenDto } from 'src/app/models/auth/access-token-dto';
import { GoogleAuthDto } from 'src/app/models/auth/google-auth-dto';
import { UserAuthDto } from 'src/app/models/auth/user-auth-dto';
import { UserRegisterDto } from 'src/app/models/user/user-register-dto';

import { UserLoginDto } from '../../models/user/user-login-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private readonly authRoutePrefix = '/api/auth';

    private readonly accessTokenKey = 'accessToken';

    private readonly refreshTokenKey = 'refreshToken';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService, private router: Router, private ngZone: NgZone) {}

    public signOut = () => {
        localStorage.removeItem(this.accessTokenKey);
        localStorage.removeItem(this.refreshTokenKey);
        this.router.navigate(['/login']);
    };

    public signInViaGoogle(googleCredentialsToken: CredentialResponse) {
        const credentials: GoogleAuthDto = { idToken: googleCredentialsToken.credential };

        return this.httpService
            .postRequest<UserAuthDto>(`${this.authRoutePrefix}/login/google`, credentials)
            .subscribe({
                next: (response: UserAuthDto) => {
                    this.saveTokens(response.token);
                    console.log(`received UserAuthDto: ${response}`);
                    this.ngZone.run(() => this.router.navigateByUrl('/main'));
                },
                error: () => {
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
}
