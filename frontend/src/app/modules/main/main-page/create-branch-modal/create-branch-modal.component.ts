import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { BranchService } from '@core/services/branch.service';
import { NotificationService } from '@core/services/notification.service';
import { SpinnerService } from '@core/services/spinner.service';
import { takeUntil, tap } from 'rxjs';

import { BranchDto } from 'src/app/models/branch/branch-dto';
import { CreateBranchDto } from 'src/app/models/branch/create-branch-dto';

@Component({
    selector: 'app-create-branch-modal',
    templateUrl: './create-branch-modal.component.html',
    styleUrls: ['./create-branch-modal.component.sass'],
})
export class CreateBranchModalComponent extends BaseComponent implements OnInit {
    @Input() public projectId: number;

    @Input() public branches: BranchDto[];

    @Output() public branchCreated = new EventEmitter<BranchDto>();

    public branchForm: FormGroup;

    constructor(
        public dialogRef: MatDialogRef<CreateBranchModalComponent>,
        private fb: FormBuilder,
        private branchService: BranchService,
        private notificationService: NotificationService,
        private spinner: SpinnerService,
        @Inject(MAT_DIALOG_DATA) public data: { projectId: number, branches: BranchDto[] },
    ) {
        super();
        this.branches = data.branches;
        this.projectId = data.projectId;
    }

    ngOnInit() {
        this.createForm();
    }

    public createForm() {
        this.branchForm = this.fb.group({
            branchName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
            selectedParent: ['', Validators.required],
        });
    }

    public createBranch(): void {
        if (!this.branchForm.valid) {
            return;
        }
        this.spinner.show();

        const branch: CreateBranchDto = {
            name: this.branchForm.value.branchName,
            parentId: this.branchForm.value.selectedParent,
        };

        this.branchService
            .addBranch(this.projectId, branch)
            .pipe(
                takeUntil(this.unsubscribe$),
                tap(() => this.spinner.hide()),
            )
            .subscribe(
                (createdBranch: BranchDto) => {
                    this.dialogRef.close(createdBranch);
                    this.notificationService.info('Branch created successfully');
                    this.branchCreated.emit(createdBranch);
                },
                () => {
                    this.notificationService.error('Failed to create branch');
                },
            );
    }

    public close(): void {
        this.dialogRef.close();
    }
}
