import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DatabaseService } from '@core/services/database.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { CreateDbModalComponent } from '@modules/main/create-db-modal/create-db-modal.component';

import { ProjectResponseDto } from 'src/app/models/projects/project-response-dto';

@Component({
    selector: 'app-main-header',
    templateUrl: './main-header.component.html',
    styleUrls: ['./main-header.component.sass'],
})
export class MainHeaderComponent implements OnInit {
    public project: ProjectResponseDto;

    public selectedDbName: string;

    public dbNames: string[] = [];

    constructor(
        private sharedProject: SharedProjectService,
        public dialog: MatDialog,
        private databaseService: DatabaseService,
    ) {
        // Intentionally left empty for dependency injection purposes only
    }

    public ngOnInit() {
        this.loadProject();
    }

    public onDatabaseSelected(value: string) {
        this.selectedDbName = value;
    }

    public openCreateModal(): void {
        const dialogRef = this.dialog.open(CreateDbModalComponent, {
            width: '700px',
            data: {
                dbEngine: this.project.dbEngine,
                projectId: this.project.id,
            },
            autoFocus: false,
        });

        dialogRef.componentInstance.dbName.subscribe({
            next: (dbName: string) => {
                this.dbNames.push(dbName);
            },
        });
    }

    private loadProject() {
        this.sharedProject.project$.subscribe({
            next: project => {
                if (project) {
                    this.project = project;
                    this.loadDatabases();
                }
            },
        });
    }

    private loadDatabases() {
        this.databaseService.getAllDatabases(this.project.id).subscribe({
            next: databases => {
                this.dbNames = databases.map(database => database.dbName);
            },
        });
    }
}
