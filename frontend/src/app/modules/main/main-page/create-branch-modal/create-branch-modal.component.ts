import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { BranchService } from '@core/services/branch.service';
import { NotificationService } from '@core/services/notification.service';
import { SpinnerService } from '@core/services/spinner.service';
import { BranchNameFormatter } from '@shared/helpers/branch-name-formatter';
import { catchError, Observable, of, takeUntil, tap } from 'rxjs';

import { BranchDto } from 'src/app/models/branch/branch-dto';
import { CreateBranchDto } from 'src/app/models/branch/create-branch-dto';
import { ErrorDetailsDto } from 'src/app/models/error/error-details-dto';

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

    private readonly invalidTokenErrorType = 7;

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

    public ngOnInit() {
        this.createForm();
    }

    public createForm() {
        this.branchForm = this.fb.group({
            branchName: ['', [
                Validators.required,
                Validators.minLength(3),
                Validators.maxLength(200),
                Validators.pattern(/^[A-Za-z0-9- _@]*$/)]],
            selectedParent: ['', Validators.required],
        });
    }

    public createBranch(): void {
        if (!this.branchForm.valid) {
            return;
        }
        this.spinner.show();

        const branch: CreateBranchDto = {
            name: BranchNameFormatter.formatBranchName(this.branchForm.value.branchName),
            parentId: this.branchForm.value.selectedParent,
        };

        this.branchService
            .addBranch(this.projectId, branch)
            .pipe(
                catchError((error: ErrorDetailsDto): Observable<BranchDto | null> => {
                    if (error.errorType === this.invalidTokenErrorType) {
                        this.notificationService.error(error.message);
                    } else {
                        this.notificationService.error('Failed to create branch');
                    }

                    return of(null);
                }),
                takeUntil(this.unsubscribe$),
                tap(() => this.spinner.hide()),

            )
            .subscribe(
                (createdBranch: BranchDto | null) => {
                    if (createdBranch) {
                        this.dialogRef.close(createdBranch);
                        this.notificationService.info('Branch created successfully');
                        this.branchCreated.emit(createdBranch);
                    }
                },
            );
    }

    public close(): void {
        this.dialogRef.close();
    }
}
