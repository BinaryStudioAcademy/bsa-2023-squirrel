import { Injectable } from '@angular/core';
import { HttpInternalService } from '@core/services/http-internal.service';

import { DatabaseInfoDto } from '../../models/database/database-info-dto';
import { NewDatabaseDto } from '../../models/database/new-database-dto';

@Injectable({
    providedIn: 'root',
})
export class DatabaseService {
    private readonly databaseApiUrl = '/api/projectDatabase';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) { }

    public addDatabase(newDatabase: NewDatabaseDto) {
        return this.httpService.postRequest<DatabaseInfoDto>(this.databaseApiUrl, newDatabase);
    }

    public getAllDatabases(projectId: number) {
        return this.httpService.getRequest<DatabaseInfoDto[]>(`${this.databaseApiUrl}/all/${projectId}`);
    }
}
