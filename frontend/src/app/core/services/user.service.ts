import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { UpdateUserNamesDto } from 'src/app/models/user/update-userNames.dto';
import { UpdateUserNotificationsDto } from 'src/app/models/user/update-userNotifications.dto';
import { UpdateUserPasswordDto } from 'src/app/models/user/update-userPassword.dto';
import { UserDto } from 'src/app/models/user/user-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({ providedIn: 'root' })
export class UserService {
    public routePrefix = '/api/user';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) {}

    public updateUserNames(dto: UpdateUserNamesDto): Observable<UserDto> {
        return this.httpService.putRequest<UserDto>(`${this.routePrefix}/update-names`, dto);
    }

    public updateUserPassword(dto: UpdateUserPasswordDto): Observable<void> {
        return this.httpService.putRequest<void>(`${this.routePrefix}/update-password`, dto);
    }

    public updateUserNotifications(dto: UpdateUserNotificationsDto): Observable<UserDto> {
        return this.httpService.putRequest<UserDto>(`${this.routePrefix}/update-notifications`, dto);
    }

    public getUserFromToken(): Observable<UserDto> {
        return this.httpService.getRequest<UserDto>(`${this.routePrefix}/fromToken`);
    }
}
