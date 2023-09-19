import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';

import { CreateScriptDto } from 'src/app/models/scripts/create-script-dto';
import { RunScriptDto } from 'src/app/models/scripts/run-script-dto';
import { ScriptContentDto } from 'src/app/models/scripts/script-content-dto';
import { ScriptDto } from 'src/app/models/scripts/script-dto';
import { ScriptResultDto } from 'src/app/models/scripts/script-result-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({
    providedIn: 'root',
})
export class ScriptService {
    private readonly scriptRoutePrefix = '/api/script';

    private readonly sqlServiceUrl = environment.sqlServiceUrl;

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) {}

    public getAllScripts(projectId: number): Observable<ScriptDto[]> {
        return this.httpService.getRequest(`${this.scriptRoutePrefix}/${projectId}/all`);
    }

    public createScript(dto: CreateScriptDto): Observable<ScriptDto> {
        return this.httpService.postRequest(this.scriptRoutePrefix, dto);
    }

    public updateScript(dto: ScriptDto): Observable<ScriptDto> {
        return this.httpService.putRequest(this.scriptRoutePrefix, dto);
    }

    public formatScript(dto: RunScriptDto): Observable<ScriptContentDto> {
        return this.httpService.putRequest(`${this.sqlServiceUrl}${this.scriptRoutePrefix}/format`, dto);
    }

    public executeScript(dto: RunScriptDto): Observable<ScriptResultDto> {
        return this.httpService.postRequest(`${this.sqlServiceUrl}${this.scriptRoutePrefix}/execute`, dto);
    }
}
