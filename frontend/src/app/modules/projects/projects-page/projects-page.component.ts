import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { BaseComponent } from '@core/base/base.component';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { CreateProjectModalComponent } from '@modules/projects/create-project-modal/create-project-modal.component';
import { finalize, takeUntil } from 'rxjs';

import { ProjectResponseDto } from 'src/app/models/projects/project-response-dto';

@Component({
    selector: 'app-projects-page',
    templateUrl: './projects-page.component.html',
    styleUrls: ['./projects-page.component.sass'],
})
export class ProjectsPageComponent extends BaseComponent implements OnInit {
    public projects: ProjectResponseDto[] = [];

    public isLoading = false;

    constructor(
        public dialog: MatDialog,
        private projectService: ProjectService,
        private notificationService: NotificationService,
        private router: Router,
        private sharedProject: SharedProjectService,
        private spinner: SpinnerService,
    ) {
        super();
    }

    ngOnInit(): void {
        this.loadProjects();
    }

    public loadProjects(): void {
        this.spinner.show();
        this.isLoading = true;
        this.projectService
            .getAllUserProjects()
            .pipe(
                takeUntil(this.unsubscribe$),
                finalize(() => {
                    this.spinner.hide();
                    this.isLoading = false;
                }),
            )
            .subscribe(
                (projects: ProjectResponseDto[]) => {
                    this.projects = projects;
                },
                () => {
                    this.notificationService.error('Failed to load projects');
                },
            );
    }

    public openCreateModal(): void {
        const dialogRef = this.dialog.open(CreateProjectModalComponent, {
            width: '450px',
        });

        dialogRef.componentInstance.projectCreated.subscribe(() => this.loadProjects());
    }

    chooseProject(project: ProjectResponseDto) {
        this.router.navigateByUrl(`projects/${project.id}/changes`);
    }
}
