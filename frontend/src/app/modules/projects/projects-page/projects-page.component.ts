import { Component, OnInit } from '@angular/core';
import { ProjectService } from '@core/services/project.service';

import { ProjectDto } from '../../../models/project-dto';

@Component({
    selector: 'app-projects-page',
    templateUrl: './projects-page.component.html',
    styleUrls: ['./projects-page.component.sass'],
})
export class ProjectsPageComponent implements OnInit {
    projects: ProjectDto[] = [];

    // eslint-disable-next-line no-empty-function
    constructor(private projectService: ProjectService) { }

    ngOnInit(): void {
        this.loadProjects();
    }

    loadProjects(): void {
        this.projectService.getAllProjects().subscribe(
            (projects: ProjectDto[]) => {
                this.projects = projects;
            },
            (error) => {
                console.error('Error loading projects:', error);
            },
        );
    }
}
