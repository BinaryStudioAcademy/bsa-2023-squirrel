import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { CreateScriptDto } from 'src/app/models/scripts/create-script-dto';
import { ScriptDto } from 'src/app/models/scripts/script-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({
    providedIn: 'root',
})
export class ScriptService {
    private readonly scriptRoutePrefix = '/api/script';

    constructor(private httpService: HttpInternalService) {
        // Intentionally left empty for dependency injection purposes only
    }

    public getAllScripts(projectId: number): Observable<ScriptDto[]> {
        return this.httpService.getRequest(`${this.scriptRoutePrefix}/${projectId}/all`);
    }

    public createScript(dto: CreateScriptDto): Observable<ScriptDto> {
        return this.httpService.postRequest(this.scriptRoutePrefix, dto);
    }

    public updateScript(dto: ScriptDto): Observable<ScriptDto> {
        return this.httpService.putRequest(this.scriptRoutePrefix, dto);
    }
}
