import { Injectable } from '@angular/core';
import { HttpInternalService } from '@core/services/http-internal.service';

import { QueryParameters } from '../../models/sql-service/query-parameters';

@Injectable({
    providedIn: 'root',
})
export class TablesService {
    private readonly tableRoutePrefix = '/api/table';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) {}

    public getAllTablesNames(query: QueryParameters) {
        return this.httpService.postRequest(this.tableRoutePrefix, query);
    }
}
