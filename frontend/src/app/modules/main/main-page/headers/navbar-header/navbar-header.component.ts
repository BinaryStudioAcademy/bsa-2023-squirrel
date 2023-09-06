import { ComponentType } from '@angular/cdk/portal';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { SafeHtml } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { BranchService } from '@core/services/branch.service';
import { NotificationService } from '@core/services/notification.service';
import { NewBranchAddedEventService } from '@modules/main/main-page/create-branch-modal/newBranchAddedEventService';
import { ArrayFunctions } from '@shared/helpers/array-functions';
import { SvgFileContentFetcher } from '@shared/helpers/svgFileContentFetcher';
import { Subscription } from 'rxjs';

import { BranchDto } from 'src/app/models/branch/branch-dto';

import { CreateBranchModalComponent } from '../../create-branch-modal/create-branch-modal.component';

@Component({
    selector: 'app-navbar-header',
    templateUrl: './navbar-header.component.html',
    styleUrls: ['./navbar-header.component.sass'],
})
export class NavbarHeaderComponent implements OnInit, OnDestroy {
    createBranchModalComponent: ComponentType<CreateBranchModalComponent> = CreateBranchModalComponent;

    public branches: BranchDto[] = [];

    public branchNames: string[] = [];

    public selectedBranch: string;

    public navLinks: { path: string; displayName: string }[] = [
        { displayName: 'Changes', path: './changes' },
        { displayName: 'PRs', path: './pull-requests' },
        { displayName: 'Branches', path: './branches' },
        { displayName: 'Scripts', path: './scripts' },
        { displayName: 'Code', path: './code' },
        { displayName: 'Settings', path: './settings' },
    ];

    private branchesIconPath = 'assets/git-branch.svg';

    public branchIcon: SafeHtml;

    private newBranchAddedEventSubscription: Subscription;

    constructor(
        private svgFileContentFetcher: SvgFileContentFetcher,
        private branchService: BranchService,
        private notificationService: NotificationService,
        private newBranchAddedEventService: NewBranchAddedEventService,
        private route: ActivatedRoute,
    ) {
        this.newBranchAddedEventSubscription = this.newBranchAddedEventService.newBranchAddedEventEmitter.subscribe(
            this.onNewBranch,
        );
    }

    ngOnInit(): void {
        this.getBranchIcon();
        this.LoadBranches();
    }

    private LoadBranches() {
        const currentProjectId = this.getCurrentProjectIdFromRoute();

        if (!currentProjectId) {
            return;
        }

        this.branchService.getProjectBranches(currentProjectId).subscribe(
            (branches) => {
                this.branches = branches;

                this.branchNames = branches.map((b) => b.name);
                this.branchNames.push('Add new branch');
            },
            () => {
                this.notificationService.error('Failed to load branches for current project');
            },
        );
    }

    private getBranchIcon() {
        this.svgFileContentFetcher.fetchSvgContent(this.branchesIconPath).subscribe((response) => {
            if (response !== null) {
                this.branchIcon = response;
            }
        });
    }

    private getCurrentProjectIdFromRoute() {
        const currentProjectId = this.route.snapshot.paramMap.get('id');

        return currentProjectId ? parseInt(currentProjectId, 10) : null;
    }

    private onNewBranch(branch: BranchDto) {
        this.branches.push(branch);

        ArrayFunctions.addElementToArrayAsSecondToLast(this.branchNames, branch.name);

        this.selectedBranch = branch.name;
    }

    ngOnDestroy() {
        if (this.newBranchAddedEventSubscription) {
            this.newBranchAddedEventSubscription.unsubscribe();
        }
    }
}
