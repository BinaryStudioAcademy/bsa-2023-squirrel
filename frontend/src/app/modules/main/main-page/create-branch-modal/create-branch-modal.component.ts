import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from '@core/base/base.component';
import { BranchService } from '@core/services/branch.service';
import { NotificationService } from '@core/services/notification.service';
import { takeUntil } from 'rxjs';

import { BranchDto } from 'src/app/models/branch/branch-dto';

import { NewBranchAddedEventService } from './newBranchAddedEventService';

@Component({
    selector: 'app-create-branch-modal',
    templateUrl: './create-branch-modal.component.html',
    styleUrls: ['./create-branch-modal.component.sass'],
})
export class CreateBranchModalComponent extends BaseComponent implements OnInit {
    public branches: BranchDto[] = [];

    public branchNames: string[] = [];

    public branchForm: FormGroup;

    public isBranchNameDistinct: boolean;

    constructor(
        private branchService: BranchService,
        private notificationService: NotificationService,
        private fb: FormBuilder,
        private route: ActivatedRoute,
        public dialogRef: MatDialogRef<CreateBranchModalComponent>,
        private newBranchAddedEventService: NewBranchAddedEventService,
    ) {
        super();
    }

    ngOnInit(): void {
        this.createForm();
        this.loadBranches();
    }

    public createForm() {
        this.branchForm = this.fb.group({
            branchName: ['', [Validators.required, Validators.minLength(3)]],
            sourceBranch: ['', Validators.required],
        });
    }

    public loadBranches(): void {
        const currentProjectId = this.getCurrentProjectIdFromRoute();

        if (!currentProjectId) {
            return;
        }

        this.branchService
            .getProjectBranches(currentProjectId)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                (branches: BranchDto[]) => {
                    this.branches = branches;
                    this.branchNames = this.branches.map((b) => b.name);
                },
                () => {
                    this.notificationService.error('Failed to load branches for current project');
                },
            );
    }

    public validateBranchNameInput(event: Event) {
        const inputElement = event.target as HTMLInputElement;

        const inputValue = inputElement.value;

        this.isBranchNameDistinct = !this.branchNames.includes(inputValue);
    }

    public createBranch() {
        const projectId = this.getCurrentProjectIdFromRoute();

        if (!projectId) {
            return;
        }

        const newBranch: BranchDto = {
            id: 0,
            isActive: true,
            projectId,
            name: this.branchForm.value.branchName,
        };

        this.branchService
            .addBranch(newBranch)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                (createdBranch: BranchDto) => {
                    this.notificationService.info('Branch created successfully');

                    this.newBranchAddedEventService.emitNewBranchAddedEvent(createdBranch);
                },
                () => {
                    this.notificationService.error('Failed to create branch');
                },
            );

        this.dialogRef.close();
    }

    private getCurrentProjectIdFromRoute() {
        const currentProjectId = this.route.snapshot.paramMap.get('id');

        return currentProjectId ? parseInt(currentProjectId, 10) : null;
    }
}
