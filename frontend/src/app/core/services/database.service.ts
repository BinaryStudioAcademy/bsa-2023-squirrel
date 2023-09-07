import { Injectable } from '@angular/core';
import { HttpInternalService } from '@core/services/http-internal.service';
import { tap } from 'rxjs';

import { NewDatabaseDto } from '../../models/database/new-database-dto';
import { Record } from '../../models/database/record';

@Injectable({
    providedIn: 'root',
})
export class DatabaseService {
    private readonly databaseApiUrl = '/api/projectDatabase';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) { }

    public addDatabase(newDatabase: NewDatabaseDto) {
        return this.httpService.postRequest<Record>(this.databaseApiUrl, newDatabase)
            .pipe(tap((value) => console.log(value)));
    }

    public getAllDatabases() {
        return this.httpService.getRequest<string[]>(`${this.databaseApiUrl}/all`);
    }
}
