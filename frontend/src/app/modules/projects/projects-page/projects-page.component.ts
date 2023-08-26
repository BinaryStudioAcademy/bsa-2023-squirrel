import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { CreateProjectModalComponent } from '@modules/projects/create-project-modal/create-project-modal.component';

import { ProjectDto } from '../../../models/projects/project-dto';

@Component({
    selector: 'app-projects-page',
    templateUrl: './projects-page.component.html',
    styleUrls: ['./projects-page.component.sass'],
})
export class ProjectsPageComponent implements OnInit {
    projects: ProjectDto[] = [];

    constructor(
        private projectService: ProjectService,
        public dialog: MatDialog,
        private notificationService: NotificationService,
        // eslint-disable-next-line no-empty-function
    ) {}

    ngOnInit(): void {
        this.loadProjects();
    }

    loadProjects(): void {
        this.projectService.getAllProjects().subscribe(
            (projects: ProjectDto[]) => {
                this.projects = projects;
            },
            () => {
                this.notificationService.error('Failed to load projects');
            },
        );
    }

    openCreateModal(): void {
        const dialogRef = this.dialog.open(CreateProjectModalComponent, {
            width: '35%',
            height: '45%',
        });

        dialogRef.componentInstance.projectCreated.subscribe((createdProject: ProjectDto) => {
            this.projects.push(createdProject);
        });
    }
}
