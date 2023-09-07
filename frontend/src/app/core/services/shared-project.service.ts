import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { ProjectResponseDto } from '../../models/projects/project-response-dto';

@Injectable({
    providedIn: 'root',
})
export class SharedProjectService {
    private project = new BehaviorSubject<ProjectResponseDto | null>(null);

    get project$() {
        return this.project.asObservable();
    }

    setProject(data: ProjectResponseDto | null) {
        this.project.next(data);
    }
}
