import { Injectable } from '@angular/core';
import { HttpInternalService } from '@core/services/http-internal.service';

import { DbConnectionRemote } from '../../models/console/db-connection-remote';

@Injectable({
    providedIn: 'root',
})
export class ConsoleConnectRemoteService {
    private readonly consoleConnectRoutePrefix = '/api/consoleConnect';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) { }

    public tryConnect(dbConnectionRemote: DbConnectionRemote) {
        return this.httpService.postRequest(`${this.consoleConnectRoutePrefix}/db-connect`, dbConnectionRemote);
    }
}
