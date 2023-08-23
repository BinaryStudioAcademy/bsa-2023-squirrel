import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

import { AccessTokenDto } from 'src/app/models/auth/access-token-dto';
import { AuthFailedInfo } from 'src/app/models/auth/auth-failed-info';
import { UserRegisterDto } from 'src/app/models/auth/user-register-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private authRoutePrefix = '/api/auth';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) {}

    public register(userRegisterDto: UserRegisterDto): Observable<AccessTokenDto | AuthFailedInfo> {
        return this.httpService
            .postFullRequest<AccessTokenDto>(`${this.authRoutePrefix}/register`, userRegisterDto)
            .pipe(
                map((response) => {
                    if (response.ok && response.body) {
                        this.saveTokens(response.body);

                        return response.body;
                    }

                    return { statusCode: response.status, errorMessage: response.statusText };
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
