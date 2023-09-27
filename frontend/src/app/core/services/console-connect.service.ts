import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';

import { DbConnection } from '../../models/console/db-connection';

@Injectable({
    providedIn: 'root',
})
export class ConsoleConnectService {
    public baseUrl: string = environment.consoleUrl;

    constructor(private http: HttpClient) { }

    public connect(dbConnection: DbConnection) {
        return this.http.post<string>(`${this.baseUrl}/setting/connect`, dbConnection);
    }
}
