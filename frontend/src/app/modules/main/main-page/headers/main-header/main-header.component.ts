import { Component, OnInit } from '@angular/core';
import { SharedProjectService } from '@core/services/shared-project.service';

import { ProjectResponseDto } from 'src/app/models/projects/project-response-dto';

@Component({
    selector: 'app-main-header',
    templateUrl: './main-header.component.html',
    styleUrls: ['./main-header.component.sass'],
})
export class MainHeaderComponent implements OnInit {
    public project: ProjectResponseDto;

    public selectedDbName: string;

    public dbNames: string[] = ['Dev DB', 'DB 2', 'Db 3', 'Db 4'];

    // eslint-disable-next-line no-empty-function
    constructor(private sharedProject: SharedProjectService) {
    }

    ngOnInit() {
        this.loadProject();
    }

    public onDatabaseSelected(value: string) {
        this.selectedDbName = value;
    }

    private loadProject() {
        this.sharedProject.project$.subscribe({
            next: project => {
                if (project) {
                    this.project = project;
                }
            },
        });
    }
}
