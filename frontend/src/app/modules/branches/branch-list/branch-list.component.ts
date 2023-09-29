import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { BranchService } from '@core/services/branch.service';
import { ProjectService } from '@core/services/project.service';
import { takeUntil } from 'rxjs';

import { BranchDetailsDto } from 'src/app/models/branch/branch-details-dto';
import { BranchDto } from 'src/app/models/branch/branch-dto';
import { UserDto } from 'src/app/models/user/user-dto';

import { BranchMergeModalComponent } from '../branch-merge-modal/branch-merge-modal.component';

@Component({
    selector: 'app-branch-list',
    templateUrl: './branch-list.component.html',
    styleUrls: ['./branch-list.component.sass'],
})
export class BranchListComponent extends BaseComponent implements OnInit {
    public dropdownItems: string[];

    public branches: BranchDetailsDto[];

    public searchForm: FormGroup = new FormGroup({});

    private branchList: BranchDto[] = [];

    constructor(
        private fb: FormBuilder,
        private dialog: MatDialog,
        private branchService: BranchService,
        private projectService: ProjectService,
    ) {
        super();
        this.dropdownItems = this.getBranchTypes();
        this.searchForm = this.fb.group({
            search: ['', []],
        });

        this.branches = this.getBranches();
    }

    public ngOnInit(): void {
        this.getBranchesList();
    }

    public getBranchTypes() {
        // TODO: fetch data from server, remove placeholder data
        return ['All', 'Open', 'Merged'];
    }

    public onBranchTypeSelectionChange($event: any) {
        // TODO: add filter logic
        // eslint-disable-next-line no-console
        console.log($event);
    }

    public openMergeDialog() {
        const dialogRef = this.dialog.open(BranchMergeModalComponent, {
            width: '50%',
            data: {
                projectId: this.projectService.currentProjectId,
                branches: this.branchList },
        });

        dialogRef.componentInstance.branchMerged.subscribe((branch) => {
            this.branchService.selectBranch(this.projectService.currentProjectId, branch.id);
        });
    }

    public getBranchesList() {
        this.branchService
            .getAllBranches(this.projectService.currentProjectId)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe((branches) => {
                this.branchList = branches;
            });
    }

    public getBranches() {
        const user = {
            id: 1,
            avatarUrl: 'https://picsum.photos/200',
            email: 'test@test.test',
            firstName: 'John',
            lastName: 'Smith',
            userName: 'Johnny',
        } as UserDto;
        const date = new Date(1478708162000);
        const branches = [];

        for (let i = 0; i < 15; i++) {
            const branch = {
                name: 'task/fix-empty-list-generation',
                isActive: true,
                lastUpdatedBy: user,
                ahead: Number((Math.random() * 5).toFixed(0)),
                behind: Number((Math.random() * 5).toFixed(0)),
                createdAt: date,
                updatedAt: date,
            } as BranchDetailsDto;

            branches.push(branch);
        }

        return branches;
    }
}
