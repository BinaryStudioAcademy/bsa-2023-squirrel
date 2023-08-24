import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
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
        );
    }

    openCreateModal(): void {
        const dialogRef = this.dialog.open(CreateProjectModalComponent, {
            width: '35%',
            height: '45%',
        });

        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.loadProjects();
            }
        });
    }
}
