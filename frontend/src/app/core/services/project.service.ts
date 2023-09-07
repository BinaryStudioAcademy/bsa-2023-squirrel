import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { NewProjectDto } from 'src/app/models/projects/new-project-dto';
import { ProjectResponseDto } from 'src/app/models/projects/project-response-dto';

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

    public addProject(newProject: NewProjectDto): Observable<ProjectResponseDto> {
        return this.httpService.postRequest<ProjectResponseDto>(this.projectsApiUrl, newProject);
    }

    public addUsersToProject(projectId: number, users: UserDto[]): Observable<ProjectResponseDto> {
        return this.httpService.putRequest<ProjectResponseDto>(`${this.projectsApiUrl}/add-users/${projectId}`, users);
    }

    public updateProject(projectId: number, project: UpdateProjectDto): Observable<ProjectResponseDto> {
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

    public getProjectUsers(projectId: number): Observable<UserDto[]> {
        return this.httpService.getRequest<UserDto[]>(`${this.projectsApiUrl}/team/${projectId}`);
    }
}
