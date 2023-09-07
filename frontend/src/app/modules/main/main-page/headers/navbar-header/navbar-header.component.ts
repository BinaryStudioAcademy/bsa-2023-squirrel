import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { BranchService } from '@core/services/branch.service';
import { takeUntil } from 'rxjs';

import { BranchDto } from 'src/app/models/branch/branch-dto';

import { CreateBranchModalComponent } from '../../create-branch-modal/create-branch-modal.component';

@Component({
    selector: 'app-navbar-header',
    templateUrl: './navbar-header.component.html',
    styleUrls: ['./navbar-header.component.sass'],
})
export class NavbarHeaderComponent extends BaseComponent implements OnInit, OnDestroy {
    public branches: BranchDto[];

    public currentProjectId: number;

    public selectedBranch: string;

    public navLinks: { path: string; displayName: string }[] = [
        { displayName: 'Changes', path: './changes' },
        { displayName: 'PRs', path: './pull-requests' },
        { displayName: 'Branches', path: './branches' },
        { displayName: 'Scripts', path: './scripts' },
        { displayName: 'Code', path: './code' },
        { displayName: 'Settings', path: './settings' },
    ];

    // eslint-disable-next-line no-empty-function
    constructor(private branchService: BranchService, public dialog: MatDialog) {
        super();
    }

    ngOnInit(): void {
        this.currentProjectId = 1;
        this.branchService.getAllBranches(this.currentProjectId)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe((branches) => { this.branches = branches; });
    }

    public onBranchSelected(value: string) {
        this.selectedBranch = value;
        this.branchService.selectBranch(this.currentProjectId, value);
    }

    public openBranchModal() {
        const dialogRef = this.dialog.open(CreateBranchModalComponent, {
            height: '45%',
        });

        dialogRef.componentInstance.branchCreated.subscribe((branch) => {
            this.branches.concat(branch);
            this.onBranchSelected(branch.name);
        });
    }

    public getBranchNames() {
        return this.branches.map(x => x.name);
    }

    public getCurrentBranch() {
        const currentBranch = this.branchService.getCurrentBranch(this.currentProjectId);

        return currentBranch ? this.getBranchNames().indexOf(currentBranch) : 0;
    }
}
