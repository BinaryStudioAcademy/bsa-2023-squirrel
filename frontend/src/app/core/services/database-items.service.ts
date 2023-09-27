import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { DatabaseItem } from 'src/app/models/database-items/database-item';

import { HttpInternalService } from './http-internal.service';

@Injectable({
    providedIn: 'root',
})
export class DatabaseItemsService {
    private readonly databaseItemsRoutePrefix = '/api/databaseitems';

    constructor(private httpService: HttpInternalService) { }

    public getAllItems(): Observable<DatabaseItem[]> {
        return this.httpService.getRequest<DatabaseItem[]>(this.databaseItemsRoutePrefix);
    }
}
