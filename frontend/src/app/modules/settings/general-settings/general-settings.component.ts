import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from '@core/base/base.component';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { takeUntil, tap } from 'rxjs';

import { ProjectResponseDto } from '../../../models/projects/project-response-dto';
import { UpdateProjectDto } from '../../../models/projects/update-project-dto';

@Component({
    selector: 'app-general-settings-menu',
    templateUrl: './general-settings.component.html',
    styleUrls: ['./general-settings.component.sass'],
})
export class GeneralSettingsComponent extends BaseComponent implements OnInit {
    public projectForm: FormGroup;

    public project: ProjectResponseDto;

    constructor(
        private sharedProjectService: SharedProjectService,
        private fb: FormBuilder,
        private spinner: SpinnerService,
        private projectService: ProjectService,
        private notificationService: NotificationService,
    ) {
        super();
    }

    ngOnInit() {
        this.sharedProjectService.project$.subscribe({
            next: project => {
                if (project) {
                    this.project = project;
                    this.createForm();
                }
            },
        });
    }

    public createForm() {
        this.projectForm = this.fb.group({
            projectName: [this.project.name, [
                Validators.required,
                Validators.minLength(3),
                Validators.maxLength(50),
                Validators.pattern(/^(?![\u0410-\u044F\u0400-\u04FF]).*$/)]],
            description: [this.project.description, [Validators.maxLength(1000)]],
        });
    }

    onSaveClick(): void {
        if (!this.projectForm.valid) {
            return;
        }

        this.spinner.show();

        this.project.name = this.projectForm.value.projectName;
        this.project.description = this.projectForm.value.description;

        this.sharedProjectService.setProject(this.project);

        const updatedProject: UpdateProjectDto = {
            name: this.projectForm.value.projectName,
            description: this.projectForm.value.description,
        };

        this.projectService
            .updateProject(this.project.id, updatedProject)
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

        this.spinner.hide();
    }
}
