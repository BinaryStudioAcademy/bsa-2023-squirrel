import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

import { UpdateUserDto } from 'src/app/models/user/update-user.dto';
import { UserDto } from 'src/app/models/user/user-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({ providedIn: 'root' })
export class UserService {
    public routePrefix = '/api/user';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) {}

    public getUserById(id: number): Observable<UserDto> {
        return this.httpService.getRequest<UserDto>(`${this.routePrefix}/${id}`);
    }

    public updateUser(user: UpdateUserDto): Observable<UserDto> {
        return this.httpService.putRequest<UserDto>(`${this.routePrefix}/update`, user);
    }
}
