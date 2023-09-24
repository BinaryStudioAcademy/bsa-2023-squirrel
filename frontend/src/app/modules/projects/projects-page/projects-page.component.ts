import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { BaseComponent } from '@core/base/base.component';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { CreateProjectModalComponent } from '@modules/projects/create-project-modal/create-project-modal.component';
import { takeUntil } from 'rxjs';

import { ProjectResponseDto } from 'src/app/models/projects/project-response-dto';

@Component({
    selector: 'app-projects-page',
    templateUrl: './projects-page.component.html',
    styleUrls: ['./projects-page.component.sass'],
})
export class ProjectsPageComponent extends BaseComponent implements OnInit {
    public projects: ProjectResponseDto[] = [];

    constructor(
        public dialog: MatDialog,
        private projectService: ProjectService,
        private notificationService: NotificationService,
        private router: Router,
        private sharedProject: SharedProjectService,
    ) {
        super();
    }

    public ngOnInit(): void {
        this.loadProjects();
    }

    public loadProjects(): void {
        this.projectService
            .getAllUserProjects()
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: (projects: ProjectResponseDto[]) => {
                    this.projects = projects;
                },
                error: () => {
                    this.notificationService.error('Failed to load projects');
                },
            });
    }

    public openCreateModal(): void {
        const dialogRef = this.dialog.open(CreateProjectModalComponent, {
            width: '450px',
            height: '400px',
        });

        dialogRef.componentInstance.projectCreated.subscribe(() => this.loadProjects());
    }

    public chooseProject(project: ProjectResponseDto) {
        this.sharedProject.setProject(project);
        this.router.navigateByUrl(`main/${project.id}`);
    }
}
