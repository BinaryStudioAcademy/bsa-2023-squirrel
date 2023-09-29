import { Component, OnDestroy, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from '@core/base/base.component';
import { BranchService } from '@core/services/branch.service';
import { CommitChangesService } from '@core/services/commit-changes.service';
import { DatabaseItemsService } from '@core/services/database-items.service';
import { EventService } from '@core/services/event.service';
import { LoadChangesService } from '@core/services/load-changes.service';
import { NotificationService } from '@core/services/notification.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { finalize, takeUntil } from 'rxjs';

import { BranchDto } from 'src/app/models/branch/branch-dto';
import { DatabaseDto } from 'src/app/models/database/database-dto';

import { CreateBranchModalComponent } from '../../create-branch-modal/create-branch-modal.component';

@Component({
    selector: 'app-navbar-header',
    templateUrl: './navbar-header.component.html',
    styleUrls: ['./navbar-header.component.sass'],
})
export class NavbarHeaderComponent extends BaseComponent implements OnInit, OnDestroy {
    public branches: BranchDto[] = [];

    @ViewChild('modalContent') modalContent: TemplateRef<any>;

    public currentChangesGuId: string;

    public currentBranchId: number;

    public isSettingsEnabled: boolean = false;

    public currentProjectId: number;

    public lastCommitId: number;

    public selectedBranch: BranchDto;

    public selectedDatabase: DatabaseDto;

    public navLinks: { path: string; displayName: string }[] = [
        { displayName: 'Changes', path: './changes' },
        { displayName: 'PRs', path: './pull-requests' },
        { displayName: 'Branches', path: './branches' },
        { displayName: 'Scripts', path: './scripts' },
        { displayName: 'Code', path: './code' },
        { displayName: 'Settings', path: './settings' },
    ];

    constructor(
        public dialog: MatDialog,
        private branchService: BranchService,
        private route: ActivatedRoute,
        private sharedProject: SharedProjectService,
        private changesService: LoadChangesService,
        private notificationService: NotificationService,
        private databaseItemsService: DatabaseItemsService,
        private commitChangesService: CommitChangesService,
        private spinner: SpinnerService,
        private eventService: EventService,
    ) {
        super();
    }

    public ngOnInit(): void {
        this.route.params.subscribe((params) => {
            this.currentProjectId = params['id'];
        });
        this.branchService
            .getAllBranches(this.currentProjectId)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe((branches) => {
                this.branches = branches;
            });

        this.sharedProject.project$.pipe(takeUntil(this.unsubscribe$)).subscribe({
            next: (project) => {
                if (project) {
                    this.isSettingsEnabled = project.isAuthor;
                }
            },
        });
    }

    public onBranchSelected(value: any) {
        this.selectedBranch = value;
        this.branchService.selectBranch(this.currentProjectId, value.id);
    }

    public openBranchModal() {
        const dialogRef = this.dialog.open(CreateBranchModalComponent, {
            width: '500px',
            data: { projectId: this.currentProjectId, branches: this.branches },
        });

        dialogRef.componentInstance.branchCreated.subscribe((branch) => {
            this.onBranchSelected(branch);
            this.branches.push(branch);
        });
    }

    public getCurrentBranch() {
        this.currentBranchId = this.branchService.getCurrentBranch(this.currentProjectId);
        const currentBranch = this.branches.find((x) => x.id === this.currentBranchId);

        return currentBranch ? this.branches.indexOf(currentBranch) : 0;
    }

    public filterBranch(item: BranchDto, value: string) {
        return item.name.includes(value);
    }

    public getCurrentDatabase() {
        this.sharedProject.currentDb$.pipe(takeUntil(this.unsubscribe$)).subscribe({
            next: (currentDb) => {
                this.selectedDatabase = currentDb!;
            },
        });
    }

    public loadChanges() {
        this.getCurrentDatabase();

        if (!this.selectedDatabase) {
            this.notificationService.error('No database currently selected');

            return;
        }

        this.spinner.show();

        this.changesService
            .loadChangesRequest(this.selectedDatabase.guid)
            .pipe(
                takeUntil(this.unsubscribe$),
                finalize(() => this.spinner.hide()),
            )
            .subscribe({
                next: (changeGuid) => {
                    this.eventService.changesSaved(changeGuid);
                    this.currentChangesGuId = changeGuid;
                    this.loadCommitChanges();
                },
                error: (error) => {
                    // eslint-disable-next-line no-console
                    console.log(error);

                    this.notificationService.error('An error occurred while attempting to load changes');
                },
            });

        this.branchService
            .getLastCommitId(this.currentBranchId)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: (lastCommitId) => {
                    this.lastCommitId = lastCommitId;
                },
                error: () => {
                    this.notificationService.error('An error occurred while attempting to load last commit');
                },
            });

        this.databaseItemsService
            .getAllItems(this.selectedDatabase.guid)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: (event) => {
                    this.eventService.changesLoaded(event);
                    // eslint-disable-next-line no-console
                    console.log(event);
                },
                error: (error) => {
                    // eslint-disable-next-line no-console
                    console.log(error);
                    this.notificationService.error('An error occurred while attempting to load list of db items');
                },
            });
    }

    public loadCommitChanges() {
        this.spinner.show();
        this.commitChangesService.getContentDiffs(this.lastCommitId, this.currentChangesGuId);
    }
}
