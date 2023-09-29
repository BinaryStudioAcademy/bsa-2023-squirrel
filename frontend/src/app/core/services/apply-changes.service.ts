import { Injectable } from '@angular/core';
import { HttpInternalService } from '@core/services/http-internal.service';

import { ApplyChangesDto } from '../../models/apply-changes/apply-changes-dto';
import { DatabaseDto } from '../../models/database/database-dto';

@Injectable({
    providedIn: 'root',
})
export class ApplyChangesService {
    private readonly applyChangesApiUrl = '/api/applychanges';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) { }

    public applyChanges(applyChangesDto: ApplyChangesDto, branchId: number) {
        console.log(applyChangesDto);
        console.log(branchId);

        return this.httpService.postRequest<DatabaseDto[]>(`${this.applyChangesApiUrl}/${branchId}`, applyChangesDto);
    }
}
