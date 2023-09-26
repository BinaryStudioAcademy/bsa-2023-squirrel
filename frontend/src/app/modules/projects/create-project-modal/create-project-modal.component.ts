import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { BranchNameFormatter } from '@shared/helpers/branch-name-formatter';
import { ValidationsFn } from '@shared/helpers/validations-fn';
import { takeUntil, tap } from 'rxjs';

import { DbEngine } from 'src/app/models/projects/db-engine';
import { NewProjectDto } from 'src/app/models/projects/new-project-dto';
import { ProjectDto } from 'src/app/models/projects/project-dto';

@Component({
    selector: 'app-create-project-modal',
    templateUrl: './create-project-modal.component.html',
    styleUrls: ['./create-project-modal.component.sass'],
})
export class CreateProjectModalComponent extends BaseComponent implements OnInit {
    @Output() public projectCreated = new EventEmitter<ProjectDto>();

    public selectedEngine: DbEngine;

    public projectForm: FormGroup;

    constructor(
        public dialogRef: MatDialogRef<CreateProjectModalComponent>,
        private fb: FormBuilder,
        private projectService: ProjectService,
        private notificationService: NotificationService,
        private spinner: SpinnerService,
    ) {
        super();
    }

    ngOnInit() {
        this.createForm();
    }

    public createForm() {
        this.projectForm = this.fb.group({
            projectName: ['', [
                Validators.required,
                Validators.minLength(3),
                Validators.maxLength(50),
                ValidationsFn.noCyrillic(),
            ]],
            defaultBranchName: ['', [
                Validators.required,
                Validators.minLength(3),
                Validators.maxLength(200),
                ValidationsFn.branchNameMatch()]],
            selectedEngine: ['', Validators.required],
        });
    }

    public createProject(): void {
        this.spinner.show();

        const newProject: NewProjectDto = {
            project: {
                name: this.projectForm.value.projectName.trim(),
                description: null,
                dbEngine: parseInt(this.projectForm.value.selectedEngine, 10) as DbEngine,
            },
            defaultBranch: {
                name: BranchNameFormatter.formatBranchName(this.projectForm.value.defaultBranchName),
            },
        };

        this.projectService
            .addProject(newProject)
            .pipe(
                takeUntil(this.unsubscribe$),
                tap(() => this.spinner.hide()),
            )
            .subscribe(
                (createdProject: ProjectDto) => {
                    this.dialogRef.close(createdProject);
                    this.notificationService.info('Project created successfully');
                    this.projectCreated.emit(createdProject);
                },
                () => {
                    this.notificationService.error('Failed to create project');
                },
            );
    }

    public close(): void {
        this.dialogRef.close();
    }

    public replaceSpacesWithHyphens(event: Event) {
        const inputElement = event.target as HTMLInputElement;

        inputElement.value = BranchNameFormatter.formatBranchName(inputElement.value);
    }
}
