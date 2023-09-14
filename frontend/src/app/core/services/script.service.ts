import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';

import { CreateScriptDto } from 'src/app/models/scripts/create-script-dto';
import { ScriptContentDto } from 'src/app/models/scripts/script-content-dto';
import { ScriptDto } from 'src/app/models/scripts/script-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({
    providedIn: 'root',
})
export class ScriptService {
    private readonly scriptRoutePrefix = '/api/script';

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

    public executeScript(script: ScriptContentDto) {
        return this.httpService.postRequest(`${environment.consoleUrl}/script/execute`, script);
    }
}
