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
    private authRoutePrefix = '/api/auth';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) {}

    public register(userRegisterDto: UserRegisterDto): Observable<WebApiResponse<null>> {
        return this.httpService
            .postFullRequest<AccessTokenDto | ErrorDetailsDto>(`${this.authRoutePrefix}/register`, userRegisterDto)
            .pipe(
                catchError((error: HttpErrorResponse) => of(error.error)),
                switchMap((response) => {
                    if (response.ok && response.body) {
                        this.saveTokens(response.body as AccessTokenDto);

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

    private saveTokens(tokens: AccessTokenDto) {
        if (tokens.accessToken && tokens.refreshToken) {
            localStorage.setItem('accessToken', JSON.stringify(tokens.accessToken));
            localStorage.setItem('refreshToken', JSON.stringify(tokens.refreshToken));
        }
    }
}
