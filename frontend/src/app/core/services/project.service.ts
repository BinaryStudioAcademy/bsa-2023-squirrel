import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { NewProjectDto } from 'src/app/models/projects/new-project-dto';
import { ProjectDto } from 'src/app/models/projects/project-dto';
import { ProjectResponseDto } from 'src/app/models/projects/project-response-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({
    providedIn: 'root',
})
export class ProjectService {
    private readonly projectsApiUrl = '/api/project';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) {}

    public addProject(newProject: NewProjectDto): Observable<ProjectResponseDto> {
        return this.httpService.postRequest<ProjectResponseDto>(this.projectsApiUrl, newProject);
    }

    public updateProject(projectId: string, project: ProjectDto): Observable<ProjectResponseDto> {
        const url = `${this.projectsApiUrl}/${projectId}`;

        return this.httpService.putRequest<ProjectResponseDto>(url, project);
    }

    public deleteProject(projectId: string): Observable<void> {
        const url = `${this.projectsApiUrl}/${projectId}`;

        return this.httpService.deleteRequest<void>(url);
    }

    public getProject(projectId: string): Observable<ProjectResponseDto> {
        const url = `${this.projectsApiUrl}/${projectId}`;

        return this.httpService.getRequest<ProjectResponseDto>(url);
    }

    public getAllUserProjects(): Observable<ProjectResponseDto[]> {
        return this.httpService.getRequest<ProjectResponseDto[]>(`${this.projectsApiUrl}/all`);
    }
}
