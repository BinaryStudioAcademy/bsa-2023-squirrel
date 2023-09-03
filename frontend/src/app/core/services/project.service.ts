import { Injectable } from '@angular/core';
import { Observable, switchMap } from 'rxjs';

import { ProjectDto } from 'src/app/models/projects/project-dto';

import { AuthService } from './auth.service';
import { HttpInternalService } from './http-internal.service';

@Injectable({
    providedIn: 'root',
})
export class ProjectService {
    private readonly projectsApiUrl = '/api/project';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService, private authService: AuthService) {}

    public addProject(project: ProjectDto): Observable<ProjectDto> {
        return this.httpService.postRequest<ProjectDto>(this.projectsApiUrl, project);
    }

    public updateProject(projectId: string, project: ProjectDto): Observable<ProjectDto> {
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
        return this.authService
            .getUserIdFromToken()
            .pipe(
                switchMap((id) => this.httpService.getRequest<ProjectDto[]>(`${this.projectsApiUrl}/createdby/${id}`)),
            );
    }
}
