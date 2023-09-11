import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { UpdateUserNamesDto } from 'src/app/models/user/update-user-names-dto';
import { UpdateUserNotificationsDto } from 'src/app/models/user/update-user-notifications-dto';
import { UpdateUserPasswordDto } from 'src/app/models/user/update-user-password-dto';
import { UserDto } from 'src/app/models/user/user-dto';
import { UserProfileDto } from 'src/app/models/user/user-profile-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({ providedIn: 'root' })
export class UserService {
    private readonly routePrefix = '/api/user';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) {}

    public updateUserNames(dto: UpdateUserNamesDto): Observable<UserProfileDto> {
        return this.httpService.putRequest<UserProfileDto>(`${this.routePrefix}/update-names`, dto);
    }

    public updateUserPassword(dto: UpdateUserPasswordDto): Observable<void> {
        return this.httpService.putRequest<void>(`${this.routePrefix}/update-password`, dto);
    }

    public updateUserNotifications(dto: UpdateUserNotificationsDto): Observable<UserProfileDto> {
        return this.httpService.putRequest<UserProfileDto>(`${this.routePrefix}/update-notifications`, dto);
    }

    public getUserFromToken(): Observable<UserDto> {
        return this.httpService.getRequest<UserDto>(`${this.routePrefix}/fromToken`);
    }

    public getUserProfile(): Observable<UserProfileDto> {
        return this.httpService.getRequest<UserProfileDto>(`${this.routePrefix}/user-profile`);
    }

    public getAllUsers(): Observable<UserDto[]> {
        return this.httpService.getRequest<UserDto[]>(`${this.routePrefix}/all`);
    }
}
