import { Injectable } from '@angular/core';
import { HttpInternalService } from '@core/services/http-internal.service';

import { DatabaseDto } from '../../models/database/database-dto';
import { DatabaseInfoDto } from '../../models/database/database-info-dto';
import { NewDatabaseDto } from '../../models/database/new-database-dto';

@Injectable({
    providedIn: 'root',
})
export class DatabaseService {
    private readonly databaseApiUrl = '/api/projectDatabase';

    constructor(private httpService: HttpInternalService) { }

    public addDatabase(newDatabase: NewDatabaseDto) {
        return this.httpService.postRequest<DatabaseInfoDto>(this.databaseApiUrl, newDatabase);
    }

    public getAllDatabases(projectId: number) {
        return this.httpService.getRequest<DatabaseDto[]>(`${this.databaseApiUrl}/all/${projectId}`);
    }
}
