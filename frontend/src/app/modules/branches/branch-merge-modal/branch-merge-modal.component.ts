import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { BranchService } from '@core/services/branch.service';
import { NotificationService } from '@core/services/notification.service';
import { SpinnerService } from '@core/services/spinner.service';
import { catchError, Observable, of, takeUntil, tap } from 'rxjs';

import { BranchDto } from 'src/app/models/branch/branch-dto';
import { MergeBranchDto } from 'src/app/models/branch/merge-branch-dto';

@Component({
    selector: 'app-branch-merge-modal',
    templateUrl: './branch-merge-modal.component.html',
    styleUrls: ['./branch-merge-modal.component.sass'],
})
export class BranchMergeModalComponent extends BaseComponent implements OnInit {
    @Input() public projectId: number;

    @Output() public branchMerged = new EventEmitter<BranchDto>();

    public branches: BranchDto[] = [];

    public branchForm: FormGroup;

    constructor(
        public dialogRef: MatDialogRef<BranchMergeModalComponent>,
        private fb: FormBuilder,
        private branchService: BranchService,
        private notificationService: NotificationService,
        private spinner: SpinnerService,
        @Inject(MAT_DIALOG_DATA) public data: { projectId: number, branches: BranchDto[] },
    ) {
        super();
        this.projectId = data.projectId;
        this.branches = data.branches;
    }

    public mergeBranch() {
        this.spinner.show();
        const dto = {
            destinationId: this.branchForm.value.destination,
            sourceId: this.branchForm.value.source,
        } as MergeBranchDto;

        this.branchService
            .mergeBranch(dto)
            .pipe(
                catchError((): Observable<BranchDto | null> => {
                    this.notificationService.error('Failed to merge branch');

                    return of(null);
                }),
                takeUntil(this.unsubscribe$),
                tap(() => this.spinner.hide()),

            )
            .subscribe(
                (mergedBranch: BranchDto | null) => {
                    if (mergedBranch) {
                        this.dialogRef.close(mergedBranch);
                        this.notificationService.info('Branch merged successfully');
                        this.branchMerged.emit(mergedBranch);
                    }
                },
            );
    }

    public validate() {
        return this.branchForm.valid &&
          (this.branchForm.value.source !== this.branchForm.value.destination);
    }

    public createForm() {
        this.branchForm = this.fb.group({
            source: ['', Validators.required],
            destination: ['', Validators.required],
        });
    }

    public close() {
        this.dialogRef.close();
    }

    public ngOnInit(): void {
        this.createForm();
    }
}
