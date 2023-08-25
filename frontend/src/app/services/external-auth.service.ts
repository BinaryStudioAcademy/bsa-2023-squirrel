import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { environment } from '../../environments/environment';
import { ExternalAuthDto } from '../models/auth/external-auth-dto';
import { UserAuthDto } from '../models/auth/user-auth-dto';

@Injectable({
    providedIn: 'root',
})
export class ExternalAuthService {
    private readonly APIUrl = environment.coreUrl;

    private readonly accessTokenKey = 'accessToken';

    private readonly refreshTokenKey = 'refreshToken';

    // eslint-disable-next-line no-empty-function
    constructor(private http: HttpClient, private router: Router) {}

    public signOut = () => {
        localStorage.removeItem(this.accessTokenKey);
        localStorage.removeItem(this.refreshTokenKey);
        this.router.navigate(['/login']);
    };

    public validateGoogleAuth(credentials: string) {
        const auth: ExternalAuthDto = { idToken: credentials };

        return this.http.post<UserAuthDto>(`${this.APIUrl}/api/auth/login/google`, auth).subscribe({
            next: (data: UserAuthDto) => {
                localStorage.setItem(this.accessTokenKey, JSON.stringify(data.token.accessToken));
                localStorage.setItem(this.refreshTokenKey, JSON.stringify(data.token.refreshToken));
                this.router.navigate(['/main']);
            },
            error: () => {
                this.signOut();
            },
        });
    }
}
