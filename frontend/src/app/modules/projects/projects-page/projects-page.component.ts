import { Component, OnInit } from '@angular/core';
import { ProjectDto } from '../../../models/project-dto';

@Component({
    selector: 'app-projects-page',
    templateUrl: './projects-page.component.html',
    styleUrls: ['./projects-page.component.sass'],
})
export class ProjectsPageComponent implements OnInit {
    // eslint-disable-next-line no-undef
    projects: ProjectDto[] = [];

    constructor() { }

    ngOnInit(): void {
        this.loadProjects();
    }

    loadProjects(): void {
        const project1: ProjectDto = { name: 'Project 1' };
        const project2: ProjectDto = { name: 'Project 2' };
        const project3: ProjectDto = { name: 'Project 3' };

        this.projects = [project1, project2, project3];
    }
}
