import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DatabaseService } from '@core/services/database.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { CreateDbModalComponent } from '@modules/main/create-db-modal/create-db-modal.component';

import { DatabaseDto } from 'src/app/models/database/database-dto';
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

    public databases: DatabaseDto[] = [];

    private currentDb: DatabaseDto;

    constructor(
        public dialog: MatDialog,
        private sharedProject: SharedProjectService,
        private databaseService: DatabaseService,
        // eslint-disable-next-line no-empty-function
    ) {}

    ngOnInit() {
        this.loadProject();
    }

    public onDatabaseSelected(value: string) {
        this.selectedDbName = value;
        this.currentDb = this.databases!.find((database) => database.dbName === this.selectedDbName)!;

        this.sharedProject.setCurrentDb(this.currentDb);
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

        dialogRef.componentInstance.addedDatabase.subscribe({
            next: (addedDatabase: DatabaseDto) => {
                this.databases.push(addedDatabase);
                this.dbNames.push(addedDatabase.dbName);
            },
        });
    }

    private loadProject() {
        this.sharedProject.project$.subscribe({
            next: (project) => {
                if (project) {
                    this.project = project;
                    this.loadDatabases();
                }
            },
        });
    }

    private loadDatabases() {
        this.databaseService.getAllDatabases(this.project.id).subscribe({
            next: (databases) => {
                this.databases = databases;
                this.dbNames = databases.map((database) => database.dbName);
                this.sharedProject.setCurrentDb(databases[0]);
            },
        });
    }
}
