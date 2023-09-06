import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '@core/base/base.component';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { finalize, takeUntil, tap } from 'rxjs';

import { ProjectDto } from '../../../models/projects/project-dto';
import { UpdateProjectDto } from '../../../models/projects/update-project-dto';

@Component({
    selector: 'app-general-settings-menu',
    templateUrl: './general-settings.component.html',
    styleUrls: ['./general-settings.component.sass'],
})
export class GeneralSettingsComponent extends BaseComponent implements OnInit {
    public projectForm: FormGroup;

    public projectId: string;

    public project: ProjectDto;

    constructor(
        private fb: FormBuilder,
        private spinner: SpinnerService,
        private projectService: ProjectService,
        private notificationService: NotificationService,
        private route: ActivatedRoute,
    ) {
        super();
    }

    ngOnInit() {
        this.createForm();
        const projectId = this.route.snapshot.paramMap.get('id');

        if (projectId != null) {
            this.projectId = projectId;
            this.projectService.getProject(projectId)
                .pipe(
                    takeUntil(this.unsubscribe$),
                    finalize(() => this.spinner.hide()),
                )
                .subscribe({
                    next: project => {
                        this.project = project;
                    },
                    error: err => {
                        this.notificationService.error(err.message);
                    },
                });
        }
    }

    public createForm() {
        this.projectForm = this.fb.group({
            projectName: [this.project.name, [Validators.required, Validators.maxLength(50)]],
            description: [this.project.description],
        });
    }

    onSaveClick(): void {
        this.spinner.show();

        const updatedProject: UpdateProjectDto = {
            name: this.projectForm.value.projectName,
            description: this.projectForm.value.description,
        };

        this.projectService
            .updateProject(this.projectId, updatedProject)
            .pipe(
                takeUntil(this.unsubscribe$),
                tap(() => this.spinner.hide()),
            )
            .subscribe(
                () => {
                    this.notificationService.info('Project updated successfully');
                },
                () => {
                    this.notificationService.error('Failed to update project');
                },
            );
    }
}
