import { Injectable } from '@angular/core';

import { BranchDetailsDto } from 'src/app/models/branch/branch-details-dto';
import { BranchDto } from 'src/app/models/branch/branch-dto';
import { CreateBranchDto } from 'src/app/models/branch/create-branch-dto';
import { MergeBranchDto } from 'src/app/models/branch/merge-branch-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({
    providedIn: 'root',
})
export class BranchService {
    private routePrefix = '/api/branch';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) { }

    public getAllBranches(projectId: number) {
        return this.httpService.getRequest<BranchDto[]>(`${this.routePrefix}/${projectId}`);
    }

    public getAllBranchDetails(projectId: number, selectedBranchId: number) {
        return this.httpService.getRequest<BranchDetailsDto[]>(`${this.routePrefix}/${projectId}/${selectedBranchId}`);
    }

    public getLastCommitId(branchId: number) {
        return this.httpService.getRequest<number>(`${this.routePrefix}/lastcommit/${branchId}`);
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

    public mergeBranch(dto: MergeBranchDto) {
        return this.httpService.postRequest<BranchDto>(`${this.routePrefix}/merge`, dto);
    }
}
