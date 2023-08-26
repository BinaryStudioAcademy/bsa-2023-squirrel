import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';

import { AccessTokenDto } from 'src/app/models/auth/access-token-dto';
import { UserRegisterDto } from 'src/app/models/user/user-register-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private readonly authRoutePrefix = '/api/auth';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) {}

    public register(userRegisterDto: UserRegisterDto): Observable<AccessTokenDto> {
        return this.httpService.postRequest<AccessTokenDto>(`${this.authRoutePrefix}/register`, userRegisterDto).pipe(
            tap((tokens) => {
                this.saveTokens(tokens);
            }),
        );
    }

    private saveTokens(tokens: AccessTokenDto) {
        if (tokens.accessToken && tokens.refreshToken) {
            localStorage.setItem('accessToken', JSON.stringify(tokens.accessToken));
            localStorage.setItem('refreshToken', JSON.stringify(tokens.refreshToken));
        }
    }
}
