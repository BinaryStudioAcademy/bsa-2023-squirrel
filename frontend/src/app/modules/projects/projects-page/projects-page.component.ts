import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { CreateProjectModalComponent } from '@modules/projects/create-project-modal/create-project-modal.component';
import { takeUntil } from 'rxjs';

import { ProjectDto } from 'src/app/models/projects/project-dto';

@Component({
    selector: 'app-projects-page',
    templateUrl: './projects-page.component.html',
    styleUrls: ['./projects-page.component.sass'],
})
export class ProjectsPageComponent extends BaseComponent implements OnInit {
    public projects: ProjectDto[] = [];

    constructor(
        public dialog: MatDialog,
        private projectService: ProjectService,
        private notificationService: NotificationService,
    ) {
        super();
    }

    ngOnInit(): void {
        this.loadProjects();
    }

    public loadProjects(): void {
        this.projectService
            .getAllUserProjects()
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                (projects: ProjectDto[]) => {
                    this.projects = projects;
                },
                () => {
                    this.notificationService.error('Failed to load projects');
                },
            );
    }

    public openCreateModal(): void {
        const dialogRef = this.dialog.open(CreateProjectModalComponent, {
            width: '500px',
            height: '45%',
        });

        dialogRef.componentInstance.projectCreated.subscribe((createdProject: ProjectDto) => {
            this.projects.push(createdProject);
        });
    }
}
