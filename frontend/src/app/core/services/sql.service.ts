import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';

import { QueryParameters } from '../../models/sql-service/query-parameters';

@Injectable({
    providedIn: 'root',
})
export class SqlService {
    public baseUrl: string = `${environment.sqlService}/api/ConsoleAppHub`;

    // eslint-disable-next-line no-empty-function
    constructor(private http: HttpClient) { }

    public getAllTablesNames(query: QueryParameters) {
        return this.http.post(`${this.baseUrl}/getAllTablesNames`, query);
    }
}
