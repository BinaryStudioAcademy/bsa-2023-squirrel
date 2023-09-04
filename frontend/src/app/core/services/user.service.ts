import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { User } from 'src/app/models/user/user';

import { HttpInternalService } from './http-internal.service';

@Injectable({
    providedIn: 'root',
})
export class UserService {
    private routePrefix = '/api/user';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) {}

    public getUserFromToken(): Observable<User> {
        return this.httpService.getRequest<User>(`${this.routePrefix}/fromToken`);
    }
}
