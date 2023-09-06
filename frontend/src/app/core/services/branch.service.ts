import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { BranchDto } from 'src/app/models/branch/branch-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({
    providedIn: 'root',
})
export class BranchService {
    private readonly branchesApiUrl = '/api/branch';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) {}

    public getProjectBranches(projectId: number): Observable<BranchDto[]> {
        return this.httpService.getRequest<BranchDto[]>(`${this.branchesApiUrl}/byProject/${projectId}`);
    }

    public addBranch(branch: BranchDto): Observable<BranchDto> {
        return this.httpService.postRequest<BranchDto>(this.branchesApiUrl, branch);
    }

    public deleteBranch(branchId: string): Observable<void> {
        const url = `${this.branchesApiUrl}/${branchId}`;

        return this.httpService.deleteRequest<void>(url);
    }
}
