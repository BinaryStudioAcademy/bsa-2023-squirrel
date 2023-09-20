import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { DatabaseDto } from '../../models/database/database-dto';
import { ProjectResponseDto } from '../../models/projects/project-response-dto';

@Injectable({
    providedIn: 'root',
})
export class SharedProjectService {
    private project = new BehaviorSubject<ProjectResponseDto | null>(null);

    private currentDb = new BehaviorSubject<DatabaseDto | null>(null);

    get project$() {
        return this.project.asObservable();
    }

    setProject(data: ProjectResponseDto | null) {
        this.project.next(data);
    }

    get currentDb$() {
        return this.currentDb.asObservable();
    }

    setCurrentDb(data: DatabaseDto | null) {
        this.currentDb.next(data);
    }
}
