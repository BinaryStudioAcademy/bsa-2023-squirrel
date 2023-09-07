import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { NewProjectDto } from 'src/app/models/projects/new-project-dto';
import { ProjectDto } from 'src/app/models/projects/project-dto';

import { UpdateProjectDto } from '../../models/projects/update-project-dto';
import { UserDto } from '../../models/user/user-dto';

import { HttpInternalService } from './http-internal.service';

@Injectable({
    providedIn: 'root',
})
export class ProjectService {
    private readonly projectsApiUrl = '/api/project';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) {}

    public addProject(newProject: NewProjectDto): Observable<ProjectDto> {
        return this.httpService.postRequest<ProjectDto>(this.projectsApiUrl, newProject);
    }

    public updateProject(projectId: string, project: UpdateProjectDto): Observable<ProjectDto> {
        const url = `${this.projectsApiUrl}/${projectId}`;

        return this.httpService.putRequest<ProjectDto>(url, project);
    }

    public deleteProject(projectId: string): Observable<void> {
        const url = `${this.projectsApiUrl}/${projectId}`;

        return this.httpService.deleteRequest<void>(url);
    }

    public getProject(projectId: string): Observable<ProjectDto> {
        const url = `${this.projectsApiUrl}/${projectId}`;

        return this.httpService.getRequest<ProjectDto>(url);
    }

    public getAllUserProjects(): Observable<ProjectDto[]> {
        return this.httpService.getRequest<ProjectDto[]>(`${this.projectsApiUrl}/all`);
    }

    public getProjectUsers(projectId: string): Observable<UserDto[]> {
        return this.httpService.getRequest<UserDto[]>(`${this.projectsApiUrl}/team/${projectId}`);
    }
}
