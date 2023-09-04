import { Injectable, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { CredentialResponse } from 'google-one-tap';
import { map, Observable, of, tap } from 'rxjs';

import { AccessTokenDto } from 'src/app/models/auth/access-token-dto';
import { GoogleAuthDto } from 'src/app/models/auth/google-auth-dto';
import { UserAuthDto } from 'src/app/models/auth/user-auth-dto';
import { User } from 'src/app/models/user/user';
import { UserRegisterDto } from 'src/app/models/user/user-register-dto';

import { UserLoginDto } from '../../models/user/user-login-dto';

import { EventService } from './event.service';
import { HttpInternalService } from './http-internal.service';
import { SpinnerService } from './spinner.service';
import { UserService } from './user.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private readonly authRoutePrefix = '/api/auth';

    private readonly accessTokenKey = 'accessToken';

    private readonly refreshTokenKey = 'refreshToken';

    constructor(
        private httpService: HttpInternalService,
        private router: Router,
        private ngZone: NgZone,
        private spinner: SpinnerService,
        private userService: UserService,
        private eventService: EventService, // eslint-disable-next-line no-empty-function
    ) {}

    private currentUser: User | undefined;

    public getUser() {
        return this.currentUser
            ? of(this.currentUser)
            : this.userService.getUserFromToken().pipe(
                map((resp: any) => {
                    this.currentUser = resp;
                    this.eventService.userChanged(this.currentUser);

                    return this.currentUser;
                }),
            );
    }

    public signOut = () => {
        localStorage.removeItem(this.accessTokenKey);
        localStorage.removeItem(this.refreshTokenKey);
        this.currentUser = undefined;
        this.eventService.userChanged(undefined);
        this.router.navigate(['/login']);
    };

    public signInViaGoogle(googleCredentialsToken: CredentialResponse) {
        const credentials: GoogleAuthDto = { idToken: googleCredentialsToken.credential };

        this.ngZone.run(() => this.spinner.show());

        return this.httpService
            .postRequest<UserAuthDto>(`${this.authRoutePrefix}/login/google`, credentials)
            .subscribe({
                next: (response: UserAuthDto) => {
                    this.handleAuthResponse(response);
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

    public register(userRegisterDto: UserRegisterDto): Observable<UserAuthDto> {
        return this.httpService.postRequest<UserAuthDto>(`${this.authRoutePrefix}/register`, userRegisterDto).pipe(
            tap((resp) => {
                this.handleAuthResponse(resp);
            }),
        );
    }

    public login(userLoginDto: UserLoginDto): Observable<UserAuthDto> {
        return this.httpService.postRequest<UserAuthDto>(`${this.authRoutePrefix}/login`, userLoginDto).pipe(
            tap((resp) => {
                this.handleAuthResponse(resp);
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

    private handleAuthResponse(response: UserAuthDto) {
        this.saveTokens(response.token);
        this.currentUser = response.user;
        this.eventService.userChanged(this.currentUser);
    }
}
