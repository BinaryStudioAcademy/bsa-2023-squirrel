import { Component, OnDestroy, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from '@core/base/base.component';
import { BranchService } from '@core/services/branch.service';
import { LoadChangesService } from '@core/services/load-changes.service';
import { NotificationService } from '@core/services/notification.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { takeUntil } from 'rxjs';

import { BranchDto } from 'src/app/models/branch/branch-dto';

import { CreateBranchModalComponent } from '../../create-branch-modal/create-branch-modal.component';

@Component({
    selector: 'app-navbar-header',
    templateUrl: './navbar-header.component.html',
    styleUrls: ['./navbar-header.component.sass'],
})
export class NavbarHeaderComponent extends BaseComponent implements OnInit, OnDestroy {
    public branches: BranchDto[] = [];

    @ViewChild('modalContent') modalContent: TemplateRef<any>;

    public isSettingsEnabled: boolean = false;

    public currentProjectId: number;

    public selectedBranch: BranchDto;

    public navLinks: { path: string; displayName: string }[] = [
        { displayName: 'Changes', path: './changes' },
        { displayName: 'PRs', path: './pull-requests' },
        { displayName: 'Branches', path: './branches' },
        { displayName: 'Scripts', path: './scripts' },
        { displayName: 'Code', path: './code' },
        { displayName: 'Settings', path: './settings' },
    ];

    constructor(
        private branchService: BranchService,
        public dialog: MatDialog,
        private route: ActivatedRoute,
        private sharedProject: SharedProjectService,
        private changesService: LoadChangesService,
        private notificationService: NotificationService,
    ) {
        super();
    }

    ngOnInit(): void {
        this.route.params.subscribe((params) => { this.currentProjectId = params['id']; });
        this.branchService.getAllBranches(this.currentProjectId)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe((branches) => { this.branches = branches; });

        this.sharedProject.project$.pipe(
            takeUntil(this.unsubscribe$),
        ).subscribe({
            next: project => {
                this.isSettingsEnabled = project!.isAuthor;
            },
        });
    }

    public onBranchSelected(value: BranchDto) {
        this.selectedBranch = value;
        this.branchService.selectBranch(this.currentProjectId, value.id);
    }

    public openBranchModal() {
        const dialogRef = this.dialog.open(CreateBranchModalComponent, {
            width: '50%',
            data: { projectId: this.currentProjectId, branches: this.branches },
        });

        dialogRef.componentInstance.branchCreated.subscribe((branch) => {
            this.onBranchSelected(branch);
            this.branches.push(branch);
        });
    }

    public getCurrentBranch() {
        const currentBranchId = this.branchService.getCurrentBranch(this.currentProjectId);
        const currentBranch = this.branches.find(x => x.id === currentBranchId);

        return currentBranch ? this.branches.indexOf(currentBranch) : 0;
    }

    filterBranch(item: BranchDto, value: string) {
        return item.name.includes(value);
    }

    public loadChanges() {
        this.changesService.loadChangesRequest()
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: () => {
                    this.notificationService.info('Changes loaded successfully');
                },
                error: () => {
                    this.notificationService.error('An error occurred while attempting to load changes');
                },
            });
    }
}
