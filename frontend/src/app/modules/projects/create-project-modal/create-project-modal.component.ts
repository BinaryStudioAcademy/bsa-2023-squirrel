import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';

import { EngineEnum } from '../../../models/projects/engine-enum';
import { ProjectDto } from '../../../models/projects/project-dto';

@Component({
    selector: 'app-create-project-modal',
    templateUrl: './create-project-modal.component.html',
    styleUrls: ['./create-project-modal.component.sass'],
})
export class CreateProjectModalComponent implements OnInit {
    @Output() projectCreated = new EventEmitter<ProjectDto>();

    selectedEngine: EngineEnum = EngineEnum.SqlServer;

    projectForm: FormGroup;

    constructor(
        private fb: FormBuilder,
        private projectService: ProjectService,
        public dialogRef: MatDialogRef<CreateProjectModalComponent>,
        private notificationService: NotificationService,
        // eslint-disable-next-line no-empty-function
    ) {}

    ngOnInit() {
        this.createForm();
    }

    createForm() {
        this.projectForm = this.fb.group({
            projectName: ['', Validators.required],
            selectedEngine: [EngineEnum.SqlServer],
        });
    }

    createProject(): void {
        if (!this.projectForm.valid) {
            return;
        }

        const newProject: ProjectDto = {
            name: this.projectForm.value.projectName,
            engine: this.projectForm.value.selectedEngine,
        };

        this.projectService.addProject(newProject).subscribe(
            (createdProject: ProjectDto) => {
                this.dialogRef.close(createdProject);
                this.notificationService.info('Project created successfully');
                this.projectCreated.emit(newProject);
            },
            () => {
                this.notificationService.error('Failed to create project');
            },
        ).unsubscribe();
    }

    close(): void {
        this.dialogRef.close();
    }
}
