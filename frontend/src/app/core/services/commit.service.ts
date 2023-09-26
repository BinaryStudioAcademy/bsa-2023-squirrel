import { Injectable } from '@angular/core';

import { CreateCommitDto } from 'src/app/models/commit/create-commit-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({
    providedIn: 'root',
})
export class CommitService {
    private routePrefix = '/api/commit';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) { }

    public commit(dto: CreateCommitDto) {
        return this.httpService.postFullRequest(`${this.routePrefix}`, dto);
    }
}
