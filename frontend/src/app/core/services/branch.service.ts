import { Injectable } from '@angular/core';

import { BranchDto } from 'src/app/models/branch/branch-dto';
import { CreateBranchDto } from 'src/app/models/branch/create-branch-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({
    providedIn: 'root',
})
export class BranchService {
    private routePrefix = '/api/branch';

    constructor(private httpService: HttpInternalService) { }

    public getAllBranches(projectId: number) {
        return this.httpService.getRequest<BranchDto[]>(`${this.routePrefix}/${projectId}`);
    }

    public addBranch(projectId: number, dto: CreateBranchDto) {
        return this.httpService.postRequest<BranchDto>(`${this.routePrefix}/${projectId}`, dto);
    }

    public selectBranch(projectId: number, branchId: number) {
        localStorage.setItem(`currentBranch_${projectId}`, branchId.toString());
    }

    public getCurrentBranch(projectId: number) {
        return Number(localStorage.getItem(`currentBranch_${projectId}`));
    }
}
