import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { CreateProjectModalComponent } from '@modules/projects/create-project-modal/create-project-modal.component';
import { Subject, takeUntil } from 'rxjs';

import { ProjectDto } from '../../../models/projects/project-dto';

@Component({
    selector: 'app-projects-page',
    templateUrl: './projects-page.component.html',
    styleUrls: ['./projects-page.component.sass'],
})
export class ProjectsPageComponent implements OnInit {
    projects: ProjectDto[] = [];

    private unsubscribe$ = new Subject<void>();

    constructor(
        public dialog: MatDialog,
        private projectService: ProjectService,
        private notificationService: NotificationService,
        private router: Router,
        // eslint-disable-next-line no-empty-function
    ) {}

    ngOnInit(): void {
        this.loadProjects();
    }

    loadProjects(): void {
        this.projectService.getAllProjects().pipe(
            takeUntil(this.unsubscribe$),
        ).subscribe(
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
            width: '500px',
            height: '45%',
        });

        dialogRef.componentInstance.projectCreated.subscribe((createdProject: ProjectDto) => {
            this.projects.push(createdProject);
        });
    }

    public chooseProject(projectId: number) {
        this.router.navigateByUrl(`/main/${projectId}`);
    }
}
