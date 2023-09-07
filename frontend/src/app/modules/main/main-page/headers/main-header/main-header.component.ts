import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SharedProjectService } from '@core/services/shared-project.service';
import { CreateDbModalComponent } from '@modules/main/create-db-modal/create-db-modal.component';

import { ProjectResponseDto } from 'src/app/models/projects/project-response-dto';

import { DbEngine } from '../../../../../models/projects/db-engine';

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
    constructor(private sharedProject: SharedProjectService, public dialog: MatDialog) {
    }

    ngOnInit() {
        this.loadProject();
    }

    public onDatabaseSelected(value: string) {
        this.selectedDbName = value;
    }

    public openCreateModal(): void {
        this.dialog.open(CreateDbModalComponent, {
            width: '700px',
            data: { dbEngine: DbEngine.PostgreSQL },
        });
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

