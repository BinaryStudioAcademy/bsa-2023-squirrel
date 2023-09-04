import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { Subject, takeUntil } from 'rxjs';

import { DbEngine } from '../../../models/projects/db-engine';
import { ProjectDto } from '../../../models/projects/project-dto';

@Component({
    selector: 'app-create-project-modal',
    templateUrl: './create-project-modal.component.html',
    styleUrls: ['./create-project-modal.component.sass'],
})
export class CreateProjectModalComponent implements OnInit {
    @Output() projectCreated = new EventEmitter<ProjectDto>();

    private unsubscribe$ = new Subject<void>();

    selectedEngine: DbEngine = DbEngine.SqlServer;

    projectForm: FormGroup;

    constructor(
        public dialogRef: MatDialogRef<CreateProjectModalComponent>,
        private fb: FormBuilder,
        private projectService: ProjectService,
        private notificationService: NotificationService,
    ) {
        // intentionaly left empty
    }

    ngOnInit() {
        this.createForm();
    }

    createForm() {
        this.projectForm = this.fb.group({
            projectName: ['', Validators.required],
            selectedEngine: [DbEngine.SqlServer],
            projectDescription: [''],
        });
    }

    createProject(): void {
        if (!this.projectForm.valid) {
            return;
        }

        const newProject: ProjectDto = {
            name: this.projectForm.value.projectName,
            engine: this.projectForm.value.selectedEngine,
            description: this.projectForm.value.projectDescription,
            date: new Date(),
        };

        // TODO: make it through the BranchService
        // created default branch for this project

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

    close(): void {
        this.dialogRef.close();
    }
}
