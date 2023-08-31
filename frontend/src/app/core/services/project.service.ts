import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ProjectDto } from '../../models/projects/project-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({
    providedIn: 'root',
})
export class ProjectService {
    private readonly projectsApiUrl = '/api/project';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) {}

    addProject(project: ProjectDto): Observable<ProjectDto> {
        return this.httpService.postRequest<ProjectDto>(this.projectsApiUrl, project);
    }

    updateProject(projectId: string, project: ProjectDto): Observable<ProjectDto> {
        const url = `${this.projectsApiUrl}/${projectId}`;

        return this.httpService.putRequest<ProjectDto>(url, project);
    }

    deleteProject(projectId: string): Observable<void> {
        const url = `${this.projectsApiUrl}/${projectId}`;

        return this.httpService.deleteRequest<void>(url);
    }

    getProject(projectId: string): Observable<ProjectDto> {
        const url = `${this.projectsApiUrl}/${projectId}`;

        return this.httpService.getRequest<ProjectDto>(url);
    }

    getAllProjects(): Observable<ProjectDto[]> {
        return this.httpService.getRequest<ProjectDto[]>(this.projectsApiUrl);
    }
}
