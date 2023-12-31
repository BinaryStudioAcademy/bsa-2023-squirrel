import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { UpdateUserNamesDto } from 'src/app/models/user/update-user-names-dto';
import { UpdateUserPasswordDto } from 'src/app/models/user/update-user-password-dto';
import { UserDto } from 'src/app/models/user/user-dto';
import { UserProfileDto } from 'src/app/models/user/user-profile-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({ providedIn: 'root' })
export class UserService {
    private readonly routePrefix = '/api/user';

    constructor(private httpService: HttpInternalService) { }

    public updateUserNames(dto: UpdateUserNamesDto): Observable<UserProfileDto> {
        return this.httpService.putRequest<UserProfileDto>(`${this.routePrefix}/update-names`, dto);
    }

    public updateUserPassword(dto: UpdateUserPasswordDto): Observable<void> {
        return this.httpService.putRequest<void>(`${this.routePrefix}/update-password`, dto);
    }

    public getUserFromToken(): Observable<UserDto> {
        return this.httpService.getRequest<UserDto>(`${this.routePrefix}/fromToken`);
    }

    public getUserProfile(): Observable<UserProfileDto> {
        return this.httpService.getRequest<UserProfileDto>(`${this.routePrefix}/user-profile`);
    }

    public uploadAvatar(avatar: File) {
        const formData = new FormData();

        formData.append('avatar', avatar);

        return this.httpService.postRequest(`${this.routePrefix}/add-avatar`, formData);
    }

    public deleteAvatar() {
        return this.httpService.deleteRequest(`${this.routePrefix}/delete-avatar`);
    }

    public getAllUsers(): Observable<UserDto[]> {
        return this.httpService.getRequest<UserDto[]>(`${this.routePrefix}/all`);
    }
}
