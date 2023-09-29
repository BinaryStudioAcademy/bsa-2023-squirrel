import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { BranchService } from '@core/services/branch.service';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { finalize, takeUntil } from 'rxjs';

import { BranchDetailsDto } from 'src/app/models/branch/branch-details-dto';
import { BranchDto } from 'src/app/models/branch/branch-dto';

import { BranchMergeModalComponent } from '../branch-merge-modal/branch-merge-modal.component';

@Component({
    selector: 'app-branch-list',
    templateUrl: './branch-list.component.html',
    styleUrls: ['./branch-list.component.sass'],
})
export class BranchListComponent extends BaseComponent implements OnInit {
    public dropdownItems: string[];

    public branches: BranchDetailsDto[];

    public filteredBranches: BranchDetailsDto[];

    public searchForm: FormGroup = new FormGroup({});

    private branchList: BranchDto[] = [];

    private previousSelection: string[] = [];

    constructor(
        private fb: FormBuilder,
        private dialog: MatDialog,
        private branchService: BranchService,
        private projectService: ProjectService,
        private spinner: SpinnerService,
        private notificationService: NotificationService,
    ) {
        super();
        this.dropdownItems = this.getBranchTypes();
        this.searchForm = this.fb.group({
            search: ['', []],
        });
    }

    public ngOnInit(): void {
        this.getBranchesList();
        this.getBranches();
    }

    public getBranchTypes() {
        return ['All', 'Active'];
    }

    public onBranchTypeSelectionChange($event: string[]) {
        this.previousSelection = $event;
        if ($event.some(x => x === 'All')) {
            this.filteredBranches = this.branches;
        }

        if ($event.some(x => x === 'Active')) {
            this.filteredBranches = this.branches.filter((x) => x.isActive);
        }
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
        this.spinner.show();
        const projectId = this.projectService.currentProjectId;

        this.branchService
            .getAllBranchDetails(projectId, this.branchService.getCurrentBranch(projectId))
            .pipe(
                takeUntil(this.unsubscribe$),
                finalize(() => {
                    this.spinner.hide();
                }),
            )
            .subscribe(
                (branches: BranchDetailsDto[]) => {
                    this.branches = branches;
                    this.onBranchTypeSelectionChange(this.previousSelection);
                },
                () => {
                    this.notificationService.error('Failed to load branches');
                },
            );
    }
}
