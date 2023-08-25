import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, switchMap } from 'rxjs';

import { AccessTokenDto } from 'src/app/models/auth/access-token-dto';
import { UserRegisterDto } from 'src/app/models/auth/user-register-dto';
import { ErrorDetailsDto } from 'src/app/models/error/error-details-dto';
import { WebApiResponse } from 'src/app/models/http/web-api-response';

import { HttpInternalService } from './http-internal.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private readonly authRoutePrefix = '/api/auth';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) {}

    public register(userRegisterDto: UserRegisterDto): Observable<WebApiResponse<null>> {
        return this.httpService
            .postRequest<AccessTokenDto | ErrorDetailsDto>(`${this.authRoutePrefix}/register`, userRegisterDto)
            .pipe(
                catchError((error: HttpErrorResponse) => of(error.error)),
                switchMap((response) => {
                    if (this.trySaveTokens(response as AccessTokenDto)) {
                        return of({
                            success: true,
                            data: null,
                            error: null,
                        });
                    }

                    return of({
                        success: false,
                        data: null,
                        error: response as ErrorDetailsDto,
                    });
                }),
            );
    }

    private trySaveTokens(tokens: AccessTokenDto): boolean {
        if (tokens.accessToken && tokens.refreshToken) {
            localStorage.setItem('accessToken', JSON.stringify(tokens.accessToken));
            localStorage.setItem('refreshToken', JSON.stringify(tokens.refreshToken));

            return true;
        }

        return false;
    }
}
