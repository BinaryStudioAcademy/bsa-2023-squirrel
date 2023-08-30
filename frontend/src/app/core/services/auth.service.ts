import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';

import { AccessTokenDto } from 'src/app/models/auth/access-token-dto';
import { GoogleAuthDto } from 'src/app/models/auth/google-auth-dto';
import { UserAuthDto } from 'src/app/models/auth/user-auth-dto';
import { UserDto } from 'src/app/models/user/user-dto';
import { UserRegisterDto } from 'src/app/models/user/user-register-dto';

import { UserLoginDto } from '../../models/user/user-login-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private readonly authRoutePrefix = '/api/auth';

    private readonly accessTokenKey = 'accessToken';

    private readonly refreshTokenKey = 'refreshToken';

    private user: UserDto;

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService, private router: Router) {}

    public signOut = () => {
        localStorage.removeItem(this.accessTokenKey);
        localStorage.removeItem(this.refreshTokenKey);
        this.router.navigate(['/login']);
    };

    public validateGoogleAuth(token: string) {
        const auth: GoogleAuthDto = { idToken: token };

        return this.httpService.postRequest<UserAuthDto>(`${this.authRoutePrefix}/login/google`, auth).subscribe({
            next: (data: UserAuthDto) => {
                this.saveTokens(data.token);
                this.user = data.user;
                this.router.navigate(['/main']);
            },
            error: () => {
                this.signOut();
            },
        });
    }

    public register(userRegisterDto: UserRegisterDto): Observable<UserAuthDto> {
        return this.httpService.postRequest<UserAuthDto>(`${this.authRoutePrefix}/register`, userRegisterDto).pipe(
            tap((data) => {
                this.saveTokens(data.token);
                this.user = data.user;
            }),
        );
    }

    public login(userLoginDto: UserLoginDto): Observable<UserAuthDto> {
        return this.httpService.postRequest<UserAuthDto>(`${this.authRoutePrefix}/login`, userLoginDto).pipe(
            tap((data) => {
                this.saveTokens(data.token);
                this.user = data.user;
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

    public getUser() {
        return this.user;
    }
}
