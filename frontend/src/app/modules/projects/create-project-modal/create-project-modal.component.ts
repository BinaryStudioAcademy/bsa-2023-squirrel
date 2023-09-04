import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { takeUntil } from 'rxjs';

import { DbEngine } from 'src/app/models/projects/db-engine';
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
    ) {
        super();
    }

    ngOnInit() {
        this.createForm();
    }

    public createForm() {
        this.projectForm = this.fb.group({
            projectName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
            defaultBranchName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(200)]],
            selectedEngine: ['', Validators.required],
        });
    }

    public createProject(): void {
        if (!this.projectForm.valid) {
            return;
        }

        const newProject: ProjectDto = {
            name: this.projectForm.value.projectName,
            defaultBranchName: this.projectForm.value.defaultBranchName,
            dbEngine: parseInt(this.projectForm.value.selectedEngine, 10) as DbEngine,
        };

        this.projectService
            .addProject(newProject)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                (createdProject: ProjectDto) => {
                    this.dialogRef.close(createdProject);
                    this.notificationService.info('Project created successfully');
                    this.projectCreated.emit(newProject);
                },
                () => {
                    this.notificationService.error('Failed to create project');
                },
            );
    }

    public close(): void {
        this.dialogRef.close();
    }
}
